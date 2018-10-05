using MaterialDesignThemes.Wpf;
using OfficeOpenXml;
using SigStat.Common;
using SigStat.Common.Loaders;
using SigStat.Common.Model;
using SigStat.WpfSample.Common;
using SigStat.WpfSample.Helpers;
using SigStat.WpfSample.Model;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Linq;
using SigStat.Common.Helpers;
using System.Diagnostics;
using SigStat.Common.Pipeline;
using SigStat.Common.Transforms;

namespace SigStat.WpfSample
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    //TODO: beolvasást meg indexeket még ellenőrizni
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        public int[] SignerIndexes { get; set; } = Common.Configuration.GetIndexes(Common.Configuration.SignerCount);
        public int[] SignatureIndexes { get; set; } = Common.Configuration.GetIndexes(Common.Configuration.SignatureCount);
        private List<Signer> Signers { get; set; } = null;

        public bool IsOptiClass { get; set; } = false;
        public bool IsNormalizationSelected { get; set; } = false;
        public bool IsCentroidSelected { get; set; } = false;
        public bool IsCenteringSelected { get; set; } = false;
        public bool IsTimeFilterSelected { get; set; } = true;
        public ClassifierType SelectedClassifier { get; set; } = ClassifierType.FusedScore;

        public List<FeatureDescriptor> FeatureFilter { get; set; } = new List<FeatureDescriptor>(new FeatureDescriptor[] { Features.X, Features.Y });
        //public String[] Databases { get; set; } = { "SVC2004_Task2" }; //"SVC2004_Task1",

        VerifierBenchmark benchmark;

        private double progressValue = 0;
        public double ProgressValue
        {
            get { return progressValue; }
            set
            {
                if (progressValue == value) return;
                progressValue = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ProgressValue)));
            }
        }



        public event PropertyChangedEventHandler PropertyChanged;

        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;
        }

        private void LoadSignatures()
        {
            Svc2004Loader loader = new Svc2004Loader(@"Databases\Online\SVC2004\Task2.zip", true);
            Signers = new List<Signer>(loader.EnumerateSigners());

            foreach (var signer in Signers)
            {
                for (int i = 0; i < signer.Signatures.Count; i++)
                {
                    FeatureExtractor featureExtractor = new FeatureExtractor(signer.Signatures[i]);
                    signer.Signatures[i] = featureExtractor.GetAllDerivedSVC2004Features();
                }
            }
        }

        private void TestClassifier(bool isOptiClass = false)
        {

            ProgressValue = 0;
            App.Current.Dispatcher.Invoke(delegate
            {
                StatisticsProgressBar.Visibility = Visibility.Visible;
            });

            var logger = new Logger(LogLevel.Debug, null, Log);
            var benchmarkResults = new Dictionary<List<FeatureDescriptor>, BenchmarkResults>(Common.Configuration.TestInputFeatures.Length);

            var sampler = Sampler.BasicSampler;
            if (isOptiClass)
            {
                sampler = new Sampler(
                    (sl) => sl,
                    (sl) => sl.Where(s => s.Origin == Origin.Genuine).Skip(10).Take(10).ToList(),
                    (sl) => sl.Where(s => s.Origin == Origin.Forged).Take(10).ToList()
                    );
            }

            string transformPipelineElementsNames = "";

            foreach (var featureFilter in Common.Configuration.TestInputFeatures)
            {
                IClassifier classifier = new TimeFilterClassifier();
                switch (SelectedClassifier)
                {
                    case ClassifierType.DTW:
                        if (IsOptiClass) { classifier = new OptimalDTWClassifier(featureFilter); }
                        else { classifier = new DTWClassifier(featureFilter); }
                        break;
                    case ClassifierType.FusedScore:
                        if (IsOptiClass) { classifier = new OptimalFusedScoreClassifier(featureFilter); }
                        else { classifier = new FusedScoreClassifier(featureFilter); }
                        break;
                    default:
                        throw new Exception("ClassifierType does not exist. Choose a valid ClassifierType!");
                }

                var transformPipeline = new SequentialTransformPipeline() { new DerivedSvc2004FeatureExtractor() };
                if (IsNormalizationSelected) { transformPipeline.Add(new Svc2004Normalize()); transformPipelineElementsNames += "_Norm"; }
                if (IsCentroidSelected) { transformPipeline.Add(new CentroidTranslate()); transformPipelineElementsNames += "_Centroid"; }
                if (IsCenteringSelected) { transformPipeline.Add(new CenteringTransform()); transformPipelineElementsNames += "_Centering";}

                benchmark = new VerifierBenchmark()
                {
                    //Loader = new Svc2004Loader(@"..\..\..\SigStat.Sample\Databases\Online\SVC2004\Task2.zip", true, s => s.ID.CompareTo("05") < 0),
                    Loader = new Svc2004Loader(@"..\..\..\SigStat.Sample\Databases\Online\SVC2004\Task2.zip", true),
                    Sampler = sampler,
                    Verifier = new MyVerifier(classifier)
                    {
                        TransformPipeline = transformPipeline
                    },
                    Logger = logger,
                };
                benchmark.ProgressChanged += Bm_ProgressChanged;
                benchmarkResults.Add(featureFilter, benchmark.Execute());

                string dumpFileName = DateTime.Now.ToString().Replace(':', '.') + "_" + classifier.GetType().Name + transformPipelineElementsNames+ "_Dump.xlsx";
                DumpLog(dumpFileName, logger.ObjectEntries);
                Process.Start(dumpFileName);
            }



            string classifierName = ((MyVerifier)benchmark.Verifier).Classifier.GetType().Name;


            string fileName = DateTime.Now.ToString().Replace(':','.') + "_" + classifierName + transformPipelineElementsNames + ".xlsx";

            using (var package = new ExcelPackage(new FileInfo(fileName)))
            {
                foreach (var featureFilter in Common.Configuration.TestInputFeatures)
                {
                    string wsName = "";
                    foreach (var feature in featureFilter)
                    {
                        wsName += feature.Name;
                    }
                    ExcelWorksheet ws = ExcelHelper.GetWorkSheetFromPackage(package, wsName);
                    ExcelHelper.SetBenchmarkresultOfClassificationHeader(ws);

                    var results = benchmarkResults[featureFilter];
                    foreach (var result in results.SignerResults)
                    {
                        ExcelHelper.SetBenchmarkresultOfClassificationRow(ws, result);
                    }

                    ExcelHelper.SetBenchmarkresultOfClassificationSummaryRow(ws, results.FinalResult, results.SignerResults.Count + 2);
                    package.Save();
                }
            }

            Process.Start(fileName);
        }

        private void DumpLog(string fileName, IReadOnlyDictionary<string, object> objectEntries)
        {

            using (var package = new ExcelPackage(new FileInfo(fileName)))
            {
                foreach (var item in objectEntries)
                {
                    ExcelWorksheet ws = ExcelHelper.GetWorkSheetFromPackage(package, item.Key);
                    ExcelHelper.WriteTable(ws, 1, 1, (object[,])item.Value);

                }
                package.Save();
            }
        }

        //Azokat a dolgokat amiket a verifier előfeldolgoz itt nincsenek csak részben
        //a loadsignature-ben van valami minimáis előfeldolgozás
        private void GoDTWButton_Click(object sender, RoutedEventArgs e)
        {
            if (Signers == null)
                LoadSignatures();

            Signature signature1 = Signers[(int)SignerComboBox1.SelectedValue - 1].Signatures[(int)SignatureComboBox1.SelectedIndex];
            Signature signature2 = Signers[(int)SignerComboBox2.SelectedValue - 1].Signatures[(int)SignatureComboBox2.SelectedIndex];

            //DTWScoreTextBlock.Text = Analyzer.GetCost(sig1, sig2, true).Cost.ToString();
            Dtw dtw = new Dtw(signature1, signature2, FeatureFilter);
            OwnDTWScoreTextBlock.Text = dtw.CalculateDtwScore().ToString();
            WarpingPathScoreTextBlock.Text = dtw.CalculateWarpingPathScore().ToString();
            FusionScoreTextBlock.Text = FusedScore.CalculateFusionOfDtwAndWPathScore(signature1, new Signature[] { signature2 }, FeatureFilter).ToString();

        }

        private void Log(LogLevel ll, string msg)
        {
            Debug.WriteLine(msg);
        }

        private void OkForAll_Click(object sender, RoutedEventArgs e)
        {
            StatisticsMessagesTextBlock.Text = "This can take longer! Creation of statistics is in progress...";
            //StatisticsMessagesTextBlock.Text = "Ez akár hosszabb ideig is eltarthat! Statisztika elkészítése folyamatban...";
            ThreadPool.QueueUserWorkItem(o => TestClassifier(IsOptiClass));
        }

        //TODO: Progressbart rendes működésre bírni
        private void Bm_ProgressChanged(object sender, int e)
        {
            Debug.WriteLine($"{benchmark.Progress}%");
            ProgressValue = benchmark.Progress / Common.Configuration.TestInputFeatures.Length;
        }


        private void SignerComboBox1_SelectionChanged(object sender, RoutedEventArgs e)
        {
            SignerComboBox2.SelectedValue = SignerComboBox1.SelectedValue;
        }

    }
}
