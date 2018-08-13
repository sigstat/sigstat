using SigStat.Common.Helpers;
using SigStat.Common.PipelineItems.Classifiers;
using SigStat.Common.Transforms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SigStat.Common.Model
{
    public class Verifier : ILogger, IProgress
    {
        private ITransformation _tp;
        public ITransformation TransformPipeline { get => _tp; set { _tp = value; _tp.Logger = Logger; } }

        private IClassification _cp;
        public IClassification ClassifierPipeline { get => _cp; set { _cp = value; _cp.Logger = Logger; } }

        private double limit;
        private List<Signature> genuines;

        private Logger _log;//TODO: ezzel kezdeni valamit
        public Logger Logger { get => _log;
            set {
                _log = value;
                if(_tp != null)
                    _tp.Logger = _log;
                if(_cp != null)
                    _cp.Logger = _log;
            }
        }
        protected void Log(LogLevel level, string message)
        {
            if (_log != null)
                _log.AddEntry(level, this, message);
        }

        private int _progress;
        public int Progress { get => _progress; set { _progress = value; ProgressChanged?.Invoke(this, value); } }
        public event EventHandler<int> ProgressChanged;

        public Verifier()
        {
            //TransformPipeline = new Pipeline.SequentialTransformPipeline();
        }

        public void Train(Signer signer)
        {
            Train(signer.Signatures.FindAll((s) => s.Origin == Origin.Genuine));
        }

        public void Train(List<Signature> sigs)
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

        public bool Test(Signature sig)
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
