using SigStat.Common.Helpers;
using SigStat.Common.PipelineItems.Classifiers;
using SigStat.Common.Transforms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SigStat.Common.Model
{
    /// <summary>
    /// Uses pipelines to transform, train on, and classify <see cref="Signature"/> objects.
    /// </summary>
    public class Verifier : ILogger, IProgress
    {
        private ITransformation _tp;
        /// <summary> Gets or sets the transform pipeline. Hands over the Logger object. </summary>
        public ITransformation TransformPipeline { get => _tp; set { _tp = value; _tp.Logger = Logger; } }

        private IClassification _cp;
        /// <summary>  Gets or sets the classifier pipeline. Hands over the Logger object. </summary>
        public IClassification ClassifierPipeline { get => _cp; set { _cp = value; _cp.Logger = Logger; } }

        private double limit;
        private List<Signature> genuines;

        private Logger _log;//TODO: ezzel kezdeni valamit: mivel ez nem pipeline item, ezert nem akartam a PipelineBase-bol szarmaztatni, ami implementalja az ILogger-t.
        /// <summary> Gets or sets the attached <see cref="Helpers.Logger"/> object used to log messages. Hands it over to the pipelines. </summary>
        public Logger Logger { get => _log;
            set {
                _log = value;
                if(_tp != null)
                    _tp.Logger = _log;
                if(_cp != null)
                    _cp.Logger = _log;
            }
        }
        /// <summary>
        /// Enqueues a new log entry to be consumed by the attached <see cref="Helpers.Logger"/>. Use this when developing new pipeline items.
        /// </summary>
        /// <param name="level">Specifies the level of the entry. Higher levels than the <see cref="Helpers.Logger"/>'s filter level will be ignored.</param>
        /// <param name="message">The main content of the log entry.</param>
        protected void Log(LogLevel level, string message)
        {
            if (_log != null)
                _log.EnqueueEntry(level, this, message);
        }

        private int _progress;
        /// <inheritdoc/>
        public int Progress { get => _progress; set { _progress = value; ProgressChanged?.Invoke(this, value); } }
        /// <inheritdoc/>
        public event EventHandler<int> ProgressChanged;

        /// <summary>
        /// Initializes a new instance of the <see cref="Verifier"/> class, with empty pipelines and no <see cref="SigStat.Common.Helpers.Logger"/>.
        /// </summary>
        public Verifier()
        {
            TransformPipeline = new Pipeline.SequentialTransformPipeline();
            ClassifierPipeline = new WeightedClassifier();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Verifier"/> class with the properties of <paramref name="v"/>.
        /// </summary>
        /// <param name="v">An instance of the <see cref="Verifier"/> class.</param>
        public Verifier(Verifier v)
        {
            this.Logger = v.Logger;
            this.TransformPipeline = v.TransformPipeline;
            this.ClassifierPipeline = v.ClassifierPipeline;
        }

        /// <summary>
        /// Trains the verifier with <see cref="Signer.Signatures"/> having <see cref="Origin.Genuine"/> property.
        /// </summary>
        /// <param name="signer">The signer with genuine signatures.</param>
        public void Train(Signer signer)
        {
            Train(signer.Signatures.FindAll((s) => s.Origin == Origin.Genuine));
        }

        /// <summary>
        /// Trains the verifier with a list of signatures. Uses the <see cref="TransformPipeline"/> to extract features,
        /// and <see cref="ClassifierPipeline"/> to find an optimized limit.
        /// </summary>
        /// <param name="sigs">The list of signatures to train on.</param>
        public virtual void Train(List<Signature> sigs)
        {
            Log(LogLevel.Info, $"Training started with {sigs.Count} signatures.");
            genuines = new List<Signature>(sigs);

            //constraint hogy csak Genuine-okkal tudjunk trainelni
            genuines.ForEach((sig) =>
            {
                if (sig.Origin != Origin.Genuine)
                    Log(LogLevel.Warn, $"Training with a non-genuine signature. ID: {sig.ID}");
            });

            genuines.ForEach((sig) =>
            {
                TransformPipeline.Transform(sig);
            });
            Log(LogLevel.Debug, "Signatures transformed.");

            //optimize limit
            limit = new ApproximateLimit(ClassifierPipeline).Calculate(genuines);

            //TODO: egyeb optimizalasi lehetosegek

            Log(LogLevel.Debug, $"Limit approximation: {limit}");
            Log(LogLevel.Info, "Training finished.");

        }

        /// <summary>
        /// Verifies the genuinity of <paramref name="sig"/>.
        /// </summary>
        /// <param name="sig"></param>
        /// <returns>True if <paramref name="sig"/> passes the verification test.</returns>
        public virtual bool Test(Signature sig)
        {
            Log(LogLevel.Info, $"Verification SignatureID {sig.ID} in progress.");

            TransformPipeline.Transform(sig);
            Log(LogLevel.Debug, "Signature transformed. Classifying..");

            double[] vals = new double[genuines.Count];
            for (int i = 0; i < genuines.Count; i++)
            {
                vals[i] = ClassifierPipeline.Pair(sig, genuines[i]);
                //if(i%10==0)
                    Progress = (int)(i / (double)(genuines.Count - 1) * 100.0);
            }
            double avg = vals.Average();
            Log(LogLevel.Debug, $"Verification SignatureID {sig.ID} result: { (avg < limit ? Origin.Genuine : Origin.Forged) }");
            Log(LogLevel.Info, $"Verification SignatureID {sig.ID}  finished.");
            return avg < limit;
        }

        /// <summary>
        /// Basic <see cref="Verifier"/> model with DTW classification of tangent features.
        /// </summary>
        public static Verifier BasicVerifier
        {
            get
            {
                return new Verifier()
                {
                    TransformPipeline = new TangentExtraction(),
                    ClassifierPipeline = new DTWClassifier() { FeatureDescriptor<List<double>>.Descriptor("Tangent") }
                };
            }
        }
    }
}
