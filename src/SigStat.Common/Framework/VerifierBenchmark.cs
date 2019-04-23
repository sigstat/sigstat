using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;
using System.Threading.Tasks;
using SigStat.Common.Loaders;
using System.Linq;
using SigStat.Common.Helpers;
using Microsoft.Extensions.Logging;
using SigStat.Common.Model;
using SigStat.Common.Framework.Exceptions;
using System.IO;
using SigStat.Common.PipelineItems.Classifiers;
using OfficeOpenXml;
using SigStat.Common.Helpers.Excel;
using System.Diagnostics;
using SigStat.Common.Helpers.Excel.Palette;
using SigStat.Common.Pipeline;
using static SigStat.Common.PipelineItems.Classifiers.OptimalDtwClassifier;
using Newtonsoft.Json;

namespace SigStat.Common
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

        /// HACK: Consider removing this after benchmark
        public readonly ISignerModel Model;

        //ez internal, mert csak a Benchmark keszithet uj Resultokat
        internal Result(string signer, double frr, double far, double aer, ISignerModel model)
        {
            Signer = signer;
            Frr = frr;
            Far = far;
            Aer = aer;
            Model = model;
        }
    }

    /// <summary>Contains the benchmark results of every <see cref="Common.Signer"/> and the summarized final results.</summary>
    [JsonObject(MemberSerialization.OptOut)]
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
    [JsonObject(MemberSerialization.OptOut)]
    public class VerifierBenchmark : ILoggerObject
    {
        readonly EventId benchmarkEvent = SigStatEvents.BenchmarkEvent;

        /// <summary> The loader to take care of <see cref="Signature"/> database loading. </summary>
        private IDataSetLoader loader;
        /// <summary> Defines the sampling strategy for the benchmark. </summary>
        private Sampler sampler;

        private Verifier verifier;
        /// <summary> Gets or sets the <see cref="Model.Verifier"/> to be benchmarked. </summary>
        
        public Verifier Verifier
        {
            get => verifier;
            set
            {
                verifier = value;
                if (verifier != null)
                {
                    verifier.Logger = Logger;
                }
            }
        }

        public void Dump(string filename, IEnumerable<KeyValuePair<string, string>> parameters)
        {
            using (var p = new ExcelPackage())
            {
                var summarySheet = p.Workbook.Worksheets.Add("Summary");
                summarySheet.Cells[2, 2].Value = "Preprocessing benchmark";
                summarySheet.Cells[3, 2].Value = DateTime.Now.ToString();
                summarySheet.Cells[4, 2].Value = Path.GetFileNameWithoutExtension(filename);
                //summarySheet.Cells[5, 2, 6, 9].InsertLegend("Go to http://sigstat.org/PreprocessingBenchmark for details", "Description", true);
                summarySheet.Cells[5, 2, 6, 9].InsertLegend("Go to http://sigstat.org/PreprocessingBenchmark for details", "Description");
                var execution = new List<KeyValuePair<string, object>>
                {
                    new KeyValuePair<string, object>("Name:","Preprocessing benchmark"),
                    new KeyValuePair<string, object>("Date:",DateTime.Now.ToString()),
                    new KeyValuePair<string, object>("Agent:",Environment.MachineName),
                    new KeyValuePair<string, object>("Duration:",duration.ToString()),
                };
                summarySheet.InsertTable(2, 8, execution, "Execution", ExcelColor.Secondary, true);
                summarySheet.InsertTable(5, 8, parameters, "Parameters", ExcelColor.Secondary, true);
                var resultsSummary = new List<KeyValuePair<string, object>>
                {
                    new KeyValuePair<string, object>("FAR:",benchmarkResults.FinalResult.Far),
                    new KeyValuePair<string, object>("FRR:",benchmarkResults.FinalResult.Frr),
                    new KeyValuePair<string, object>("AER:",benchmarkResults.FinalResult.Aer),
                };
                summarySheet.InsertTable(8, 8, resultsSummary, "Results", ExcelColor.Warning, true);
                var resultsSheet = p.Workbook.Worksheets.Add("Results");
                var signers = benchmarkResults.SignerResults.OrderBy(s => s.Signer).ToList();
                var signerSummaries = signers.Select(s => new {
                    s.Signer,
                    FAR = s.Far,
                    FRR = s.Frr,
                    AER = s.Aer
                });

                resultsSheet.InsertTable(2, 2, signerSummaries);

                foreach (var signer in signers)
                {
                    var signerSheet = p.Workbook.Worksheets.Add(signer.Signer);
                    if (signer.Model is OptimalDtwSignerModel om)
                    {
                        var distances = om.DistanceMatrix.ToArray();
                        var errorRates = om.ErrorRates.Select(r => new { Threshold = r.Key, FAR = r.Value.Far, FRR = r.Value.Frr, AER = r.Value.Aer });
                        var threshold = new List<KeyValuePair<string, object>> { new KeyValuePair<string, object>("Threshold", om.Threshold) };
                        signerSheet.InsertTable(2, 2, distances, "Distance matrix", ExcelColor.Info, true, true);
                        signerSheet.InsertTable(4 + distances.GetLength(0), 2, threshold, null, ExcelColor.Danger, false);
                        signerSheet.InsertTable(4 + distances.GetLength(0), 6, errorRates, "Error rates", ExcelColor.Info, true);
                    }
                    else if (signer.Model is DtwSignerModel dm)
                    {
                        var distances = dm.DistanceMatrix.ToArray();
                        var threshold = new List<KeyValuePair<string, object>> { new KeyValuePair<string, object>("Threshold", dm.Threshold) };
                        signerSheet.InsertTable(2, 2, distances, "Distance matrix", ExcelColor.Info, true, true);
                        signerSheet.InsertTable(4 + distances.GetLength(0), 2, threshold, null, ExcelColor.Danger, false);
                    }
                }
                p.SaveAs(new FileInfo(filename));
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
        /// Sets the <see cref="Common.Sampler"/> to the default <see cref="SVC2004Sampler1"/>.
        /// </summary>
        public VerifierBenchmark()
        {
            Verifier = null;
            Sampler = new SVC2004Sampler1();
        }

        private double farAcc = 0;
        private double frrAcc = 0;
        private double pCnt = 0;
        // HACK: should be refactored after preprocessing benchmark
        private TimeSpan duration = TimeSpan.MinValue;
        private BenchmarkResults benchmarkResults;

        /// <summary>
        /// Execute the benchmarking process.
        /// </summary>
        /// <returns></returns>
        public BenchmarkResults Execute(bool ParallelMode = true)
        {
            var stopwatch = Stopwatch.StartNew();

            // TODO: centralize logger injection
            Verifier.Logger = logger;
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
                results = signers.AsParallel().SelectMany(s => benchmarkSigner(s, signers.Count)).ToList();
            }
            else
            {
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
            if (results.Count == 0)
            {
                Exception ex = new Exception("Benchmark returned no results.");
                this.LogError(ex, "Exception: " + ex.Message);//otlet: LogError(ex); ugyanezt csinalja meg
                throw ex;

            }

            Progress = 100;
            this.LogInformation("Benchmark execution finished.");
            benchmarkResults = new BenchmarkResults(results, new Result(null, frrFinal, farFinal, aerFinal, null));
            duration = stopwatch.Elapsed;
            return benchmarkResults;
        }

        private IEnumerable<Result> benchmarkSigner(Signer iSigner, int cntSigners)
        {
            this.LogInformation("Benchmarking Signer {iSignerID}", iSigner.ID);
            List<Signature> references = Sampler.SampleReferences(iSigner.Signatures);
            List<Signature> genuineTests = Sampler.SampleGenuineTests(iSigner.Signatures);
            List<Signature> forgeryTests = Sampler.SampleForgeryTests(iSigner.Signatures);

            //HACK: kulon Verifier kell minden Signerhez
            Verifier vClone = new Verifier(Verifier);

            //HACK: remove this after preprocessing benchamrk
            if (vClone.Classifier is OptimalDtwClassifier)
                references = iSigner.Signatures;

            try
            {
                vClone.Train(references);
            }
            catch (Exception exc)
            {
                this.LogError(exc, "Training Verifier on Signer {iSignerID} failed. Skipping..", iSigner.ID);
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
                    if (vClone.Test(genuine) <= 0.5)
                    {
                        nFalseReject++;//eredeti alairast hamisnak hisz
                    }
                }
                catch (/*VerifierTesting*/Exception ex)
                {
                    this.LogError(ex, "Testing genuine Signature {signatureID} of Signer {signerID} failed. Skipping..", genuine.ID, iSigner.ID);
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
                    if (vClone.Test(forgery) > 0.5)
                    {
                        nFalseAccept++;//hamis alairast eredetinek hisz
                    }
                }
                catch (/*VerifierTesting*/Exception ex)
                {
                    this.LogError(ex, "Testing forged Signature {signatureID} of Signer {signerID} failed. Skipping..", forgery.ID, iSigner.ID);
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
            yield return new Result(iSigner.ID, FRR, FAR, AER, vClone.SignerModel);
        }



    }
}
