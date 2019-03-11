using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using SigStat.Common.Loaders;
using System.Linq;
using SigStat.Common.Helpers;
using Microsoft.Extensions.Logging;
using SigStat.Common.Model;
using SigStat.Common.Framework.Exceptions;

namespace SigStat.Common
{
    /// <summary>Contains the benchmark results of a single <see cref="Common.Signer"/></summary>
    public class Result
    {
        /// <summary>Identifier of the <see cref="Signer"/></summary>
        private readonly string Signer;
        /// <summary>False Rejection Rate</summary>
        private readonly double Frr;
        /// <summary>False Acceptance Rate</summary>
        private readonly double Far;
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
    public class VerifierBenchmark : ILoggerObject
    {
        readonly EventId benchmarkEvent = SigStatEvents.BenchmarkEvent;

        /// <summary> The loader to take care of <see cref="Signature"/> database loading. </summary>
        private IDataSetLoader loader;
        /// <summary> Defines the sampling strategy for the benchmark. </summary>
        private Sampler sampler;

        private Verifier verifier;
        /// <summary> Gets or sets the <see cref="Model.Verifier"/> to be benchmarked. </summary>
        public Verifier Verifier { get => verifier;
            set {
                verifier = value;
                if (verifier!=null)
                {
                    verifier.Logger = Logger;
                }
            }
        }

        private ILogger logger;
        /// <summary> Gets or sets the attached <see cref="ILogger"/> object used to log messages. Hands it over to the verifier. </summary>
        public ILogger Logger
        {
            get => logger;
            set
            {
                logger = value;
                if (Verifier != null)
                {
                    Verifier.Logger = logger;
                }
            }
        }

        private int progress;
        /// <inheritdoc/>
        public int Progress { get => progress; set { progress = value; ProgressChanged?.Invoke(this, value); } }

        /// <summary>
        /// The loader that will provide the database for benchmarking
        /// </summary>
        public IDataSetLoader Loader { get => loader; set => loader = value; }
        /// <summary>
        /// The <see cref="Common.Sampler"/> to be used for benchmarking
        /// </summary>
        public Sampler Sampler { get => sampler; set => sampler = value; }

        /// <inheritdoc/>
        public event EventHandler<int> ProgressChanged;


        /// <summary>
        /// Initializes a new instance of the <see cref="VerifierBenchmark"/> class.
        /// Sets the <see cref="Common.Sampler"/> to the default <see cref="SVC2004Sampler"/>.
        /// </summary>
        public VerifierBenchmark()
        {
            Verifier = null;
            Sampler = new SVC2004Sampler();
        }

        private double farAcc = 0;
        private double frrAcc = 0;
        private double pCnt = 0;

        /// <summary>
        /// Execute the benchmarking process.
        /// </summary>
        /// <returns></returns>
        public BenchmarkResults Execute(bool ParallelMode = true)
        {
            this.LogInformation("Benchmark execution started. Parallel mode: {ParallelMode}", ParallelMode);
            var results = new List<Result>();
            farAcc = 0;
            frrAcc = 0;
            pCnt = 0;

            this.LogTrace("Loading data..");
            var signers = new List<Signer>(Loader.EnumerateSigners());
            this.LogInformation("{signersCount} signers found. Benchmarking..", signers.Count);
            
            if (ParallelMode)
            {
                //Parallel.ForEach(signers, a=>benchmarkSigner(a));
                results = signers.AsParallel().SelectMany(s => benchmarkSigner(s, signers.Count)).ToList();
            }
            else
            {
                //signers.ForEach(iSigner => benchmarkSigner(iSigner));
                results = signers.SelectMany(s => benchmarkSigner(s, signers.Count)).ToList();
            }

            double frrFinal = frrAcc / results.Count;
            double farFinal = farAcc / results.Count;
            double aerFinal = (frrFinal + farFinal) / 2.0;

            int failedSigners = signers.Count - results.Count;
            if (failedSigners > 0)
            {
                this.LogWarning("{skippedCount} out of {signerCount} Signers were skipped.", signers.Count - results.Count, signers.Count);
            }

            Progress = 100;
            var r = new BenchmarkResults(results, new Result(null, frrFinal, farFinal, aerFinal));
            this.LogInformation("Benchmark execution finished.");
            return r;
        }

        private IEnumerable<Result> benchmarkSigner(Signer iSigner, int cntSigners)
        {
            this.LogInformation("Benchmarking Signer {iSignerID}", iSigner.ID);
            List<Signature> references = Sampler.SampleReferences(iSigner.Signatures);
            List<Signature> genuineTests = Sampler.SampleGenuineTests(iSigner.Signatures);
            List<Signature> forgeryTests = Sampler.SampleForgeryTests(iSigner.Signatures);

            try
            {
                Verifier.Train(references);
            }
            catch //(VerifierTrainingException ex)
            {
                this.LogError("Training Verifier on Signer {iSignerID} failed. Skipping..", iSigner.ID);
                pCnt += 1.0 / (cntSigners - 1);
                Progress = (int)(pCnt * 100);
                yield break;
            }

            //FRR: false rejection rate
            //FRR = elutasított eredeti / összes eredeti
            int nFalseReject = 0;
            int failedGenuineTests = 0;
            foreach (Signature genuine in genuineTests)
            {
                try
                {
                    if (Verifier.Test(genuine) <= 0.5)
                    {
                        nFalseReject++;//eredeti alairast hamisnak hisz
                    }
                }
                catch //(VerifierTestingException ex)
                {
                    this.LogError("Testing genuine Signature {signatureID} of Signer {signerID} failed. Skipping..", genuine.ID, iSigner.ID);
                    failedGenuineTests++;
                }
            }
            double FRR = nFalseReject / (double)(genuineTests.Count - failedGenuineTests);

            //FAR: false acceptance rate
            //FAR = elfogadott hamis / összes hamis
            int nFalseAccept = 0;
            int failedForgeryTests = 0;
            foreach (Signature forgery in forgeryTests)
            {
                try
                {
                    if (Verifier.Test(forgery) > 0.5)
                    {
                        nFalseAccept++;//hamis alairast eredetinek hisz
                    }
                }
                catch //(VerifierTestingException ex)
                {
                    this.LogError("Testing forged Signature {signatureID} of Signer {signerID} failed. Skipping..", forgery.ID, iSigner.ID);
                    failedForgeryTests++;
                }
            }
            double FAR = nFalseAccept / (double)(forgeryTests.Count - failedForgeryTests);

            //AER: average error rate
            double AER = (FRR + FAR) / 2.0;
            this.LogTrace("AER for Signer {iSignerID}: {AER}", iSigner.ID, AER);

            frrAcc += FRR;
            farAcc += FAR;

            pCnt += 1.0 / (cntSigners - 1);
            Progress = (int)(pCnt * 100);
            yield return new Result(iSigner.ID, FRR, FAR, AER);
        }



    }
}
