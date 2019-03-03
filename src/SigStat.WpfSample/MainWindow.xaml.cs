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
using System.Collections;
using SigStat.WpfSample.Transformation;

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
        public List<string> Transformations { get; set; } = Common.Configuration.PreprocessingTransformations;

        public bool IsOptiClass { get; set; } = false;
        public bool IsNormalizationSelected { get; set; } = false;
        public bool IsCentroidSelected { get; set; } = false;
        public bool IsCenteringSelected { get; set; } = false;
        public bool IsTimeFilterSelected { get; set; } = true;

        public bool IsNDtwSelected { get; set; } = false;
        public bool IsFrameworkDtwSelected { get; set; } = false;
        public bool IsMyDtwSelected { get; set; } = false;
        public bool IsDtwPySelected { get; set; } = false;

        public bool IsCompositeClass { get; set; } = false;
        public bool IsWeightedClass { get; set; } = false;
        public ClassifierType SelectedClassifier { get; set; } = ClassifierType.FusedScore;

        public List<FeatureDescriptor> FeatureFilter { get; set; } = new List<FeatureDescriptor>(new FeatureDescriptor[] { Features.X, Features.Y });
        //public String[] Databases { get; set; } = { "SVC2004_Task2" }; //"SVC2004_Task1",

        VerifierBenchmark benchmark;

        public DtwType DtwType
        {
            get
            {
                if (IsMyDtwSelected) return DtwType.MyDtw;
                else if (IsNDtwSelected) return DtwType.NDtw;
                else if (IsFrameworkDtwSelected) return DtwType.FrameworkDtw;
                else if (IsDtwPySelected) return DtwType.DtwPy;
                else throw new InvalidDataException("None of DtwTypes is selected");
            }
        }

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

        private void TestClassifier()
        {

            ProgressValue = 0;
            App.Current.Dispatcher.Invoke(delegate
            {
                StatisticsProgressBar.Visibility = Visibility.Visible;
            });

            var logger = new Logger(LogLevel.Debug, null, Log);
            var benchmarkResults = new Dictionary<List<FeatureDescriptor>, BenchmarkResults>(Common.Configuration.TestInputFeatures.Length);


            string transformPipelineElementsNames = "";
            foreach (var featureFilter in Common.Configuration.TestInputFeatures)
            {
                IClassifier classifier = GetClassifier(featureFilter);

                transformPipelineElementsNames = "";
                var transformPipeline = new SequentialTransformPipeline() { new DerivedSvc2004FeatureExtractor() };
                if (IsNormalizationSelected) { transformPipeline.Add(new Svc2004Normalize()); transformPipelineElementsNames += "_Norm"; }
                if (IsCentroidSelected) { transformPipeline.Add(new CentroidTranslate()); transformPipelineElementsNames += "_Centroid"; }
                if (IsCenteringSelected) { transformPipeline.Add(new CenteringTransform()); transformPipelineElementsNames += "_Centering"; }
                //transformPipeline.Add(new DerivedSvc2004FeatureExtractor());

                benchmark = new VerifierBenchmark()
                {
                    //Loader = new Svc2004Loader(@"..\..\..\SigStat.Sample\Databases\Online\SVC2004\Task2.zip", true, s => s.ID.CompareTo("05") < 0),
                    //Loader = new Svc2004Loader(@"..\..\..\SigStat.Sample\Databases\Online\SVC2004\Task2.zip", true, s => s.ID == "02"),
                    Loader = new Svc2004Loader(@"..\..\..\SigStat.Sample\Databases\Online\SVC2004\Task2.zip", true),
                    Sampler = IsOptiClass ? MySampler.AllReferences : MySampler.Basic,
                    Verifier = new MyVerifier(classifier)
                    {
                        TransformPipeline = transformPipeline
                    },
                    Logger = logger,
                };
                benchmark.ProgressChanged += Bm_ProgressChanged;
                benchmarkResults.Add(featureFilter, benchmark.Execute());

                string dumpFileName = DateTime.Now.ToString().Replace(':', '.') + "_" + classifier.Name + transformPipelineElementsNames + "_Dump.xlsx";
                DumpLog(dumpFileName, logger.ObjectEntries);
                Process.Start(dumpFileName);
            }



            //string classifierName = ((MyVerifier)benchmark.Verifier).Classifier.GetType().Name;


            string fileName = DateTime.Now.ToString().Replace(':', '.') + "_" + GetClassifier(Common.Configuration.DefaultInputFeatures).Name + transformPipelineElementsNames + ".xlsx";

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

        private IClassifier GetClassifier(List<FeatureDescriptor> featureFilter)
        {
            IClassifier classifier = new TimeFilterClassifier();
            if (!IsTimeFilterSelected)
            {
                switch (SelectedClassifier)
                {
                    case ClassifierType.DTW:
                        if (IsOptiClass)
                            return new OptimalDTWClassifier(featureFilter, DtwType);
                        else
                            return new DTWClassifier(featureFilter, DtwType);
                    case ClassifierType.FusedScore:
                        if (IsOptiClass)
                            return new OptimalFusedScoreClassifier(featureFilter);
                        else
                            return new FusedScoreClassifier(featureFilter);
                    default:
                        throw new Exception("ClassifierType does not exist. Choose a valid ClassifierType!");
                }
            }
            else if (IsCompositeClass)
            {
                switch (SelectedClassifier)
                {
                    case ClassifierType.DTW:
                        return new CompositeTimeFilterClassifier(new DTWClassifier(featureFilter, DtwType));
                    case ClassifierType.FusedScore:
                        return new CompositeTimeFilterClassifier(new FusedScoreClassifier(featureFilter));
                    default:
                        throw new Exception("ClassifierType does not exist. Choose a valid ClassifierType!");
                }
            }
            else if (IsWeightedClass)
            {
                var mainWeight = 0.5; // TODO: állítható
                switch (SelectedClassifier)
                {
                    case ClassifierType.DTW:
                        return new WeightedTimeFilterClassifier(new DTWClassifier(featureFilter, DtwType), mainWeight);
                    case ClassifierType.FusedScore:
                        return new WeightedTimeFilterClassifier(new FusedScoreClassifier(featureFilter), mainWeight);
                    default:
                        throw new Exception("ClassifierType does not exist. Choose a valid ClassifierType!");
                }
            }
            return classifier;
        }

        private void TestTransformation()
        {
            if (Signers == null)
                LoadSignatures();

            Signature signature = Signers[0].Signatures[0];
            string selectedTransformation = "";
            App.Current.Dispatcher.Invoke(delegate
            {
                selectedTransformation = (string)TransformationChooserComboBox.SelectedValue;

            });

            if (selectedTransformation == "Translate")
            {
                var originalValues = signature.GetFeature(Features.X);

                new Transformation.Translate(Features.X, OriginType.CenterOfGravity, 
                    FeatureDescriptor<List<double>>.Descriptor(OriginType.CenterOfGravity + "TranslationResult")).Transform(signature);
                var cogValues = signature.GetFeature<List<double>>(OriginType.CenterOfGravity + "TranslationResult");

                new Transformation.Translate(Features.X, OriginType.Minimum,
                    FeatureDescriptor<List<double>>.Descriptor(OriginType.Minimum + "TranslationResult")).Transform(signature);
                var minValues = signature.GetFeature<List<double>>(OriginType.Minimum + "TranslationResult");

                new Transformation.Translate(Features.X, OriginType.Maximum, 
                    FeatureDescriptor<List<double>>.Descriptor(OriginType.Maximum + "TranslationResult")).Transform(signature);
                var maxValues = signature.GetFeature<List<double>>(OriginType.Maximum + "TranslationResult");

                new Transformation.Translate(Features.X, 100.0, 
                    FeatureDescriptor<List<double>>.Descriptor(OriginType.Predefined + "TranslationResult")).Transform(signature);
                var predefValues = signature.GetFeature<List<double>>(OriginType.Predefined + "TranslationResult");

                string outputFileName = selectedTransformation + "TransformationOutputTest.csv";
                StreamWriter sw = new StreamWriter(outputFileName);
                sw.WriteLine("Original ; COG translated ; Min translated ; Max translated ; 100.0 translated");
                for (int i = 0; i < signature.GetFeature(Features.X).Count; i++)
                {
                    sw.WriteLine($"{originalValues[i]};{cogValues[i]};{minValues[i]};{maxValues[i]};{predefValues[i]}");
                }
                sw.Close();

                Process.Start(outputFileName);
            }
            else if (selectedTransformation == "UniformScale")
            {
                var originalXValues = signature.GetFeature(Features.X);
                var originalYValues = signature.GetFeature(Features.Y);


                new UniformScale(Features.X, Features.Y, 100, 200, 150,
                    FeatureDescriptor<List<double>>.Descriptor("UniformScalingResultBaseDim"),
                    FeatureDescriptor<List<double>>.Descriptor("UniformScalingResultProportionalDim")).Transform(signature);
                var defScaledXValues = new List<double>(signature.GetFeature<List<double>>("UniformScalingResultBaseDim"));
                var defScaledYValues = new List<double>(signature.GetFeature<List<double>>("UniformScalingResultProportionalDim"));


                new UniformScale
                {
                    BaseDimension = Features.X,
                    ProportionalDimension = Features.Y,
                    BaseDimensionOutput = FeatureDescriptor<List<double>>.Descriptor("UniformScalingResultBaseDim"),
                    ProportionalDimensionOutput = FeatureDescriptor<List<double>>.Descriptor("UniformScalingResultProportionalDim")
                }.Transform(signature);

                var autoScaledXValues = new List<double>(signature.GetFeature<List<double>>("UniformScalingResultBaseDim"));
                var autoScaledYValues = new List<double>(signature.GetFeature<List<double>>("UniformScalingResultProportionalDim"));

                string outputFileName = selectedTransformation + "TransformationOutputTest.csv";
                StreamWriter sw = new StreamWriter(outputFileName);
                sw.WriteLine("OriginalX; OriginalY ; DefScaledX ; DefScaledY ; AutoScaledX; AutoScaledY");
                for (int i = 0; i < signature.GetFeature(Features.X).Count; i++)
                {
                    sw.WriteLine($"{originalXValues[i]};{originalYValues[i]};{defScaledXValues[i]};{defScaledYValues[i]};{autoScaledXValues[i]}; {autoScaledYValues[i]}");
                }
                sw.Close();

                Process.Start(outputFileName);
            }
            else if(selectedTransformation == "Scale")
            {
                var originalXValues = signature.GetFeature(Features.X);
                new Scale(Features.X, 100, 500,
                    FeatureDescriptor<List<double>>.Descriptor("ScalingResult")).Transform(signature);
                var defScaledXValues = new List<double>(signature.GetFeature<List<double>>("ScalingResult"));

                new Scale
                {
                    InputFeature = Features.X,
                    OutputFeature = FeatureDescriptor<List<double>>.Descriptor("ScalingResult")
                }.Transform(signature);
                var autoScaledXValues = new List<double>(signature.GetFeature<List<double>>("ScalingResult"));

                string outputFileName = selectedTransformation + "TransformationOutputTest.csv";
                StreamWriter sw = new StreamWriter(outputFileName);
                sw.WriteLine("OriginalX;  DefScaledX ;  AutoScaledX");
                for (int i = 0; i < signature.GetFeature(Features.X).Count; i++)
                {
                    sw.WriteLine($"{originalXValues[i]};{defScaledXValues[i]};{autoScaledXValues[i]}");
                }
                sw.Close();

                Process.Start(outputFileName);
            }
            else if(selectedTransformation == "NormalizeRotation")
            {
                var originalXValues = new List<double>(signature.GetFeature(Features.X));
                var originalYValues = new List<double>(signature.GetFeature(Features.Y));
                new NormalizeRotation().Transform(signature);
                var rotatedXValues = new List<double>(signature.GetFeature(Features.X));
                var rotatedYValues = new List<double>(signature.GetFeature(Features.Y));

                string outputFileName = selectedTransformation + "TransformationOutputTest.csv";
                StreamWriter sw = new StreamWriter(outputFileName);
                sw.WriteLine("OriginalX; OriginalY; RotatedX ; RotatedY");
                for (int i = 0; i < signature.GetFeature(Features.X).Count; i++)
                {
                    sw.WriteLine($"{originalXValues[i]};{originalYValues[i]};{rotatedXValues[i]};{rotatedYValues[i]}");
                }
                sw.Close();

                Process.Start(outputFileName);
            }
        }

        private void DumpLog(string fileName, IReadOnlyDictionary<string, object> objectEntries)
        {

            using (var package = new ExcelPackage(new FileInfo(fileName)))
            {
                foreach (var item in objectEntries)
                {
                    ExcelWorksheet ws = ExcelHelper.GetWorkSheetFromPackage(package, item.Key);
                    var value = item.Value;
                    if (value is List<object[]>)
                        value = ((List<object[]>)value).ToMatrix();

                    if (value is object[,])
                        ExcelHelper.WriteTable(ws, 1, 1, (object[,])value);
                    else if (value is IEnumerable)
                        ExcelHelper.WriteTable(ws, 1, 1, (IEnumerable)value);
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
            ThreadPool.QueueUserWorkItem(o => TestClassifier());
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

        private void TestTransformationButton_Click(object sender, RoutedEventArgs e)
        {
            StatisticsMessagesTextBlock.Text = "This can take longer! Creation of statistics is in progress...";
            ThreadPool.QueueUserWorkItem(o => TestTransformation());
        }
    }
}
