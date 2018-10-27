using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using SigStat.Common.Loaders;
using System.Linq;
using SigStat.Common.Helpers;

namespace SigStat.Common.Model
{
    /// <summary>Contains the benchmark results of a single <see cref="Common.Signer"/></summary>
    public class Result
    {
        /// <summary>Identifier of the <see cref="Signer"/></summary>
        public readonly string Signer;
        /// <summary>False Rejection Rate</summary>
        public readonly double Frr;
        /// <summary>False Acceptance Rate</summary>
        public readonly double Far;
        /// <summary>Average Error Rate</summary>
        public readonly double Aer;

        //ez internal, mert csak a Benchmark keszithet uj Resultokat
        internal Result(string signer, double frr, double far, double aer)
        {
            Signer = signer;
            Frr = frr;
            Far = far;
            Aer = aer;
        }
    }

    //TODO: document ThresholdResult
    public class ThresholdResult: Result
    {
        public readonly double Threshold;

        public ThresholdResult(string signer, double frr, double far, double aer, double threshold)
            : base(signer, frr, far, aer)
        {
            Threshold = threshold;
        }

    }

    /// <summary>Contains the benchmark results of every <see cref="Common.Signer"/> and the summarized final results.</summary>
    public struct BenchmarkResults
    {
        /// <summary>List that contains the <see cref="Result"/>s for each <see cref="Signer"/></summary>
        public readonly List<Result> SignerResults;
        /// <summary>Summarized, final result of the benchmark execution.</summary>
        public readonly Result FinalResult;

        //ez internal, mert csak a Benchmark keszithet uj BenchmarkResults-t
        internal BenchmarkResults(List<Result> signerResults, Result finalResult)
        {
            SignerResults = signerResults;
            FinalResult = finalResult;
        }
    }

    /// <summary> Benchmarking class to test error rates of a <see cref="Model.Verifier"/> </summary>
    public class VerifierBenchmark : ILogger, IProgress
    {
        /// <summary> The loader to take care of <see cref="Signature"/> database loading. </summary>
        public IDataSetLoader Loader;
        /// <summary> Defines the sampling strategy for the benchmark. </summary>
        public Sampler Sampler;

        private Verifier _verifier;
        /// <summary> Gets or sets the <see cref="Model.Verifier"/> to be benchmarked. </summary>
        public Verifier Verifier { get => _verifier;
            set {
                _verifier = value;
                if (_verifier!=null)
                {
                    _verifier.Logger = Logger;
                }
            }
        }

        private Logger _log;//TODO: ezzel kezdeni valamit: mivel ez nem pipeline item, ezert nem akartam a PipelineBase-bol szarmaztatni, ami implementalja az ILogger-t.
        /// <summary> Gets or sets the attached <see cref="Helpers.Logger"/> object used to log messages. Hands it over to the verifier. </summary>
        public Logger Logger
        {
            get => _log;
            set
            {
                _log = value;
                if (Verifier != null)
                {
                    Verifier.Logger = _log;
                }
            }
        }

        private int _progress;
        /// <inheritdoc/>
        public int Progress { get => _progress; set { _progress = value; ProgressChanged?.Invoke(this, value); } }
        /// <inheritdoc/>
        public event EventHandler<int> ProgressChanged;

        /// <inheritdoc/>
        protected void Log(LogLevel level, string message)
        {
            if (_log != null)
            {
                _log.EnqueueEntry(level, this, message);
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="VerifierBenchmark"/> class.
        /// Sets the <see cref="Model.Verifier"/> to be the <see cref="Verifier.BasicVerifier"/>.
        /// Sets the <see cref="Model.Sampler"/> to be the <see cref="Sampler.BasicSampler"/>.
        /// </summary>
        public VerifierBenchmark()
        {
            Verifier = null; // Verifier.BasicVerifier;
            Sampler = Sampler.BasicSampler;
        }

        /// <summary>
        /// Asynchronously execute the benchmarking process.
        /// </summary>
        /// <returns></returns>
        public async Task<int> ExecuteAsync()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Synchronously execute the benchmarking process.
        /// </summary>
        /// <returns></returns>
        public BenchmarkResults Execute()
        {
            Log(LogLevel.Info, "Benchmark execution started.");
            var results = new List<Result>();
            double farAcc = 0;
            double frrAcc = 0;

            Log(LogLevel.Info, "Loading data..");
            var signers = new List<Signer>(Loader.EnumerateSigners());
            Log(LogLevel.Info, signers.Count + " signers found. Benchmarking..");
            for(int i = 0; i < signers.Count; i++)
            {
                Log(LogLevel.Info, $"Benchmarking Signer ID {signers[i].ID} ({i+1}/{signers.Count})");
                Sampler.Init(signers[i]);
                List<Signature> references = Sampler.SampleReferences();
                List<Signature> genuineTests = Sampler.SampleGenuineTests();
                List<Signature> forgeryTests = Sampler.SampleForgeryTests();
                //catch: Log.Error("nem volt eleg alairas a benchmarkhoz");

                Verifier.Train(references);

                //FRR: false rejection rate
                //FRR = elutasított eredeti / összes eredeti
                int nFalseReject = 0;
                foreach (Signature genuine in genuineTests)
                {
                    if (!Verifier.Test(genuine))
                    {
                        nFalseReject++;//eredeti alairast hamisnak hisz
                    }
                }

                double FRR = nFalseReject / (double)genuineTests.Count;

                //FAR: false acceptance rate
                //FAR = elfogadott hamis / összes hamis
                int nFalseAccept = 0;
                foreach (Signature forgery in forgeryTests)
                {
                    if (Verifier.Test(forgery))
                    {
                        nFalseAccept++;//hamis alairast eredetinek hisz
                    }
                }

                double FAR = nFalseAccept / (double)forgeryTests.Count;

                //AER: average error rate
                double AER = (FRR + FAR) / 2.0;
                Log(LogLevel.Debug, $"AER for Signer ID {signers[i].ID}: {AER}");

                //EER definicio fix: ez az az ertek amikor FAR==FRR

                frrAcc += FRR;
                farAcc += FAR;
                results.Add(new Result(signers[i].ID, FRR, FAR, AER));

                //if(i%10==0)
                    Progress = (int)(i / (double)(signers.Count - 1) * 100.0);
            }

            double frrFinal = frrAcc / signers.Count;
            double farFinal = farAcc / signers.Count;
            double aerFinal = (frrFinal + farFinal) / 2.0;

            Log(LogLevel.Info, "Benchmark execution finished.");
            Log(LogLevel.Debug, $"AER: {aerFinal}");
            return new BenchmarkResults(results, new Result(null, frrFinal, farFinal, aerFinal));
        }

        /// <summary>
        /// Parallel execute the benchmarking process.
        /// </summary>
        /// <returns></returns>
        public BenchmarkResults ExecuteParallel()
        {
            Log(LogLevel.Info, "Parallel benchmark execution started.");
            var results = new List<Result>();
            double farAcc = 0;
            double frrAcc = 0;

            Log(LogLevel.Info, "Loading data..");
            var signers = new List<Signer>(Loader.EnumerateSigners(null));
            Log(LogLevel.Info, signers.Count + " signers found. Benchmarking..");
            double parallelProgress = 0;
            Parallel.ForEach(signers, iSigner =>
            {
                Log(LogLevel.Info, $"Benchmarking Signer ID {iSigner.ID}");
                Sampler localSampler = new Sampler(Sampler);
                localSampler.Init(iSigner);//parhuzamositas miatt kell kulon sampler mindenkinek
                List<Signature> references = localSampler.SampleReferences();
                List<Signature> genuineTests = localSampler.SampleGenuineTests();
                List<Signature> forgeryTests = localSampler.SampleForgeryTests();
                //catch: Log.Error("nem volt eleg alairas a benchmarkhoz");

                Verifier localVerifier = new Verifier(Verifier);
                localVerifier.Train(references);

                //FRR: false rejection rate
                //FRR = elutasított eredeti / összes eredeti
                int nFalseReject = 0;
                foreach (Signature genuine in genuineTests)
                {
                    if (!localVerifier.Test(genuine))
                    {
                        nFalseReject++;//eredeti alairast hamisnak hisz
                    }
                }

                double FRR = nFalseReject / (double)genuineTests.Count;

                //FAR: false acceptance rate
                //FAR = elfogadott hamis / összes hamis
                int nFalseAccept = 0;
                foreach (Signature forgery in forgeryTests)
                {
                    if (localVerifier.Test(forgery))
                    {
                        nFalseAccept++;//hamis alairast eredetinek hisz
                    }
                }

                double FAR = nFalseAccept / (double)forgeryTests.Count;

                //AER: average error rate
                double AER = (FRR + FAR) / 2.0;
                Log(LogLevel.Debug, $"AER for Signer ID {iSigner.ID}: {AER}");

                //EER definicio fix: ez az az ertek amikor FAR==FRR

                frrAcc += FRR;
                farAcc += FAR;
                results.Add(new Result(iSigner.ID, FRR, FAR, AER));

                parallelProgress += 1.0 / (double)(signers.Count - 1);
                Progress = (int)(parallelProgress * 100);//TODO: atterni doublere, hogy ne kelljen kulon valtozot szamolni
            });

            double frrFinal = frrAcc / signers.Count;
            double farFinal = farAcc / signers.Count;
            double aerFinal = (frrFinal + farFinal) / 2.0;

            Progress = 100;
            Log(LogLevel.Info, "Benchmark execution finished.");
            Log(LogLevel.Debug, $"AER: {aerFinal}");
            return new BenchmarkResults(results, new Result(null, frrFinal, farFinal, aerFinal));
        }


    }
}
