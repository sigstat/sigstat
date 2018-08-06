using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using SigStat.Common.Loaders;
using System.Linq;
using SigStat.Common.Helpers;

namespace SigStat.Common.Model
{
    public struct Result
    {
        public readonly string Signer;
        public readonly double Frr;
        public readonly double Far;
        public readonly double Aer;

        public Result(string signer, double frr, double far, double aer)
        {
            Signer = signer;
            Frr = frr;
            Far = far;
            Aer = aer;
        }
    }

    public struct BenchmarkResults
    {
        public readonly List<Result> SignerResults;
        public readonly Result FinalResult;

        public BenchmarkResults(List<Result> signerResults, Result finalResult)
        {
            SignerResults = signerResults;
            FinalResult = finalResult;
        }
    }

    public class VerifierBenchmark : ILogger, IProgress
    {
        public IDataSetLoader Loader;
        public Sampler Sampler;

        private Verifier _verifier;
        public Verifier Verifier { get => _verifier;
            set {
                _verifier = value;
                _verifier.Logger = Logger;
            }
        }

        private Logger _log;//TODO: ezzel kezdeni valamit
        public Logger Logger
        {
            get => _log;
            set
            {
                _log = value;
                if (Verifier != null)
                    Verifier.Logger = _log;
            }
        }

        private int _progress;
        public int Progress { get => _progress; set { _progress = value; ProgressChanged(this, value); } }
        public event EventHandler<int> ProgressChanged = delegate { };

        protected void Log(LogLevel level, string message)
        {
            if (_log != null)
                _log.AddEntry(level, this, message);
        }

        public VerifierBenchmark()
        {
            //ha nem allit be kulon semmit, akkor ez legyen a default
            Verifier = Verifier.BasicVerifier;
            Sampler = Sampler.BasicSampler;
        }

        public async Task<int> ExecuteAsync()
        {
            throw new NotImplementedException();
        }

        public BenchmarkResults Execute()
        {
            Log(LogLevel.Info, "Benchmark execution started.");
            var results = new List<Result>();
            double farAcc = 0;
            double frrAcc = 0;

            Log(LogLevel.Info, "Loading data..");
            var signers = new List<Signer>(Loader.EnumerateSigners(null));
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
                    if (!Verifier.Test(genuine))
                        nFalseReject++;//eredeti alairast hamisnak hisz
                double FRR = nFalseReject / (double)genuineTests.Count;

                //FAR: false acceptance rate
                //FAR = elfogadott hamis / összes hamis
                int nFalseAccept = 0;
                foreach (Signature forgery in forgeryTests)
                    if (Verifier.Test(forgery))
                        nFalseAccept++;//hamis alairast eredetinek hisz
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


    }
}
