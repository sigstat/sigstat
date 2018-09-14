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

        public List<FeatureDescriptor> FeatureFilter { get; set; } = new List<FeatureDescriptor>(new FeatureDescriptor[] {Features.X, Features.Y});
        //public String[] Databases { get; set; } = { "SVC2004_Task2" }; //"SVC2004_Task1",

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

        private void DoStatistics(bool areSelectedFeaturesUsed = true, bool areTestFeaturesUsed = true)
        {
            ProgressValue = 0;
            App.Current.Dispatcher.Invoke(delegate
            {
                StatisticsProgressBar.Visibility = Visibility.Visible;
            });

            List<FeatureDescriptor>[] testFeatureFilters = Common.Configuration.TestInputFeatures;
            //if (areSelectedFeaturesUsed && areTestFeaturesUsed)
            //{
            //    if (!Configuration.TestFeatureFilters.Contains(FeatureFilter))
            //    {
            //        testFeatureFilters = new Features[Configuration.TestFeatureFilters.Length + 1];
            //        testFeatureFilters[0] = FeatureFilter;
            //        Configuration.TestFeatureFilters.CopyTo(testFeatureFilters, 1);
            //    }
            //    else
            //        testFeatureFilters = Configuration.TestFeatureFilters;
            //}
            //else if (areSelectedFeaturesUsed)
            //    testFeatureFilters = new Features[] { FeatureFilter };
            //else
            //    testFeatureFilters = Configuration.TestFeatureFilters;

            ////Features[] rowFeatures = Configuration.Top10FeatureFilters;

            ////Combinations<Features> combinedfeatures = new Combinations<Features>(rowFeatures, 2);
            ////testFeatureFilters = new Features[combinedfeatures.Count];
            ////int filterIndex = 0;
            ////foreach (var item in combinedfeatures)
            ////{
            ////    testFeatureFilters[filterIndex] = item[0] | item[1];
            ////    Console.WriteLine(testFeatureFilters[filterIndex]);
            ////    filterIndex++;
            ////}

            ////using (var package = new ExcelPackage(new FileInfo(Configuration.FusedScoreAutoClassificationName)))
            ////{

            //foreach (var featureFilter in testFeatureFilters)
            //{
            //    using (var package = new ExcelPackage(new FileInfo(DateTime.Today.ToShortDateString() + " " + featureFilter + ".xlsx")))
            //    {
            //        //ExcelWorksheet ws = ExcelHelper.GetWorkSheetFromPackage(package, featureFilter.ToString());

            //        //ExcelHelper.SetFusedScoreAutomaticClassificationHeader(ws);

            //        //ConcurrentDictionary<int, double[]> dictOfResults = new ConcurrentDictionary<int, double[]>(Environment.ProcessorCount, Configuration.SignerCount);
            //        ThresholdResult[,] results = new ThresholdResult[Configuration.CheckThresholdNumOfSteps, Configuration.SignerCount];

            //        Parallel.For(1, Configuration.SignerCount + 1, signerIndex =>
            //        {

            //            //double farDTW = 0, farFusedScore = 0;
            //            //double frrDTW = 0, frrFusedScore = 0;
            //            //double aerDTW = 0, aerFusedScore = 0;


            //            //for (int i = 0; i < Configuration.FusedScoreAutomaticClassificationTrainingSetCount; i++)
            //            //{
            //            TestDatabase db = new TestDatabase();
            //            db.LoadSignatures(signerIndex, SignatureDir);

            //            //double thresholdDTW = Analyzer.CalculateAvg(db.GetTrainingCosts(SimilarityValueTypes.OwnImplementedDtw, featureFilter));
            //            //double thresholdFusedScore = Analyzer.CalculateAvg(db.GetTrainingCosts(SimilarityValueTypes.FusedScore, featureFilter));

            //            List<double> trainingCosts = db.GetTrainingCosts(SimilarityValueTypes.FusedScore, featureFilter);
            //            double initThresholdFusedScore = Analyzer.CalculateAvg(trainingCosts);
            //            double deviation = Analyzer.CalculateDeviation(trainingCosts);
            //            ThresholdDefiner thDef = new ThresholdDefiner(initThresholdFusedScore, deviation * 0.3, Configuration.CheckThresholdNumOfSteps);

            //            for (int i = 0; i < thDef.MaxNumberOfSteps; i++)
            //            {
            //                double thresholdFusedScore = thDef.GetNextThreshold();
            //                double farFusedScore = 0, frrFusedScore = 0, aerFusedScore = 0;

            //                //farDTW += automaticClassificationWindow.CalculateFxR(db, thresholdDTW, true, SimilarityValueTypes.OwnImplementedDtw, featureFilter);
            //                farFusedScore += automaticClassificationWindow.CalculateFxR(db, thresholdFusedScore, true, SimilarityValueTypes.FusedScore, featureFilter);
            //                //frrDTW += automaticClassificationWindow.CalculateFxR(db, thresholdDTW, false, SimilarityValueTypes.OwnImplementedDtw, featureFilter);
            //                frrFusedScore += automaticClassificationWindow.CalculateFxR(db, thresholdFusedScore, false, SimilarityValueTypes.FusedScore, featureFilter);
            //                //}

            //                //farDTW /= Configuration.FusedScoreAutomaticClassificationTrainingSetCount;
            //                //frrDTW /= Configuration.FusedScoreAutomaticClassificationTrainingSetCount;
            //                //aerDTW = (farDTW + frrDTW) / 2; ;
            //                //farFusedScore /= Configuration.FusedScoreAutomaticClassificationTrainingSetCount;
            //                //frrFusedScore /= Configuration.FusedScoreAutomaticClassificationTrainingSetCount;
            //                aerFusedScore = (farFusedScore + frrFusedScore) / 2;

            //                //dictOfResults.TryAdd(signerIndex, new double[] { farDTW, farFusedScore, frrDTW, frrFusedScore, aerDTW, aerFusedScore });
            //                results[i, signerIndex - 1] = new ThresholdResult(signerIndex, farFusedScore, frrFusedScore, aerFusedScore, thresholdFusedScore, featureFilter);

            //                //ProgressValue += 1 / ((double)Configuration.SignerCount * testFeatureFilters.Length) * 100;
            //                ProgressValue += 1 / ((double)Configuration.SignerCount * testFeatureFilters.Length * Configuration.CheckThresholdNumOfSteps) * 100;
            //            }

            //        });

            //        ExcelWorksheet summaryWS = ExcelHelper.GetWorkSheetFromPackage(package, "Summary");
            //        ExcelHelper.SetFusedScoreAutoClassThresholdTestSummarySheetHeader(summaryWS);

            //        for (int i = 0; i < Configuration.CheckThresholdNumOfSteps; i++)
            //        {
            //            string wsName = "AVG + " + i + " x DEV x 0.3";
            //            ExcelWorksheet ws = ExcelHelper.GetWorkSheetFromPackage(package, wsName);

            //            ExcelHelper.SetFusedScoreAutomaticClassificationThresholdTestHeader(ws);

            //            for (int signerIndex = 1; signerIndex <= Configuration.SignerCount; signerIndex++)
            //            {
            //                //double[] rateValues = dictOfResults[signerIndex];
            //                //ExcelHelper.SetFusedScoreAutomaticClassificationRow(ws, signerIndex, rateValues[0], rateValues[1], rateValues[2], rateValues[3], rateValues[4], rateValues[5]);

            //                ThresholdResult result = results[i, signerIndex - 1];
            //                ExcelHelper.SetFusedScoreAutomaticClassificationThresholdTestRow(ws, result);
            //            }

            //            //ExcelHelper.SetFusedScoreAutomaticClassificationSummary(ws, Configuration.SignerCount + 2);
            //            ExcelHelper.SetFusedScoreAutomaticClassificationThresholdTestSummary(ws, Configuration.SignerCount + 2);

            //            ExcelHelper.SetFusedScoreAutoClassThresholdTestSummarySheetRow(summaryWS, wsName, i + 2, Configuration.SignerCount + 2);

            //            package.Save();
            //        }
            //    }
            //}


            //App.Current.Dispatcher.Invoke(delegate
            //{
            //    //StatisticsMessagesTextBlock.Text = "A statisztika elkészült!";
            //    StatisticsMessagesTextBlock.Text = "Statistics are ready!";
            //    StatisticsProgressBar.Visibility = Visibility.Hidden;
            //    StatisticsMessagesTextBlock.Text = "";
            //});


            ////System.Diagnostics.Process.Start(Configuration.FusedScoreAutoClassificationName);
        }

        private void CreateDebugTables()
        {

            ProgressValue = 0;
            App.Current.Dispatcher.Invoke(delegate
            {
                StatisticsProgressBar.Visibility = Visibility.Visible;
            });


            #region AutoClass debug
            //string fileName = DateTime.Today.ToShortDateString() + "YCoordinate_Avg+3Dev0.3_Threshold_DebugTable_AutoClass_NDTW.xlsx";
            //using (var package = new ExcelPackage(new FileInfo(fileName)))
            //{
            //    foreach (var featureFilter in Common.Configuration.TestInputFeatures)
            //    {
            //        ConcurrentDictionary<string, ThresholdResult> dictOfResults = new ConcurrentDictionary<string, ThresholdResult>(Environment.ProcessorCount, Common.Configuration.SignerCount);
            //        ConcurrentDictionary<string, double[,]> dictOfCosts = new ConcurrentDictionary<string, double[,]>(Environment.ProcessorCount, Common.Configuration.SignerCount);

            //        var classifier = new DTWClassifier(featureFilter);

            //        foreach (var signer in Signers)
            //        {
            //            double[,] costs = new double[30, 10];

            //            Helpers.Sampler sampler = new Helpers.Sampler(signer.Signatures, 10);
            //            double threshold = classifier.Train(sampler.TrainingOriginals);

            //            double far = ClassifierHelper.CalculateFAR(sampler.ForgedTestSignatures, classifier);
            //            double frr = ClassifierHelper.CalculateFRR(sampler.GenuineTestSignatures, classifier);
            //            double aer = (far + frr) / 2;

            //            dictOfResults.TryAdd(signer.ID, new ThresholdResult(signer.ID, frr, far, aer, threshold));
            //            dictOfCosts.TryAdd(signer.ID, costs);

            //            ProgressValue += 1.0 / Common.Configuration.SignerCount * 100;
            //        }

            //        ExcelWorksheet summaryWs = ExcelHelper.GetWorkSheetFromPackage(package, "Summary");
            //        ExcelHelper.SetDebugTableSummarySheetHeader(summaryWs);

            //        foreach (var signer in Signers)
            //        {
            //            string wsName = "Signer" + signer.ID;
            //            ExcelWorksheet ws = ExcelHelper.GetWorkSheetFromPackage(package, wsName);
            //            ExcelHelper.SetDebugTableHeader(ws);

            //            double[,] costs = dictOfCosts[signer.ID];

            //            for (int r = 0; r < 30; r++)
            //            {
            //                for (int c = 0; c < 10; c++)
            //                {
            //                    ExcelHelper.SetDebugTableDistanceValueCell(ws, c + 1, r + 1, costs[r, c]);
            //                }
            //            }

            //            ThresholdResult result = dictOfResults[signer.ID];
            //            ExcelHelper.SetDebugTableResults(ws, result.Threshold, result.Far, result.Far, result.Aer);

            //            ExcelHelper.SetDebugTableSummarySheetRow(summaryWs, wsName, int.Parse(signer.ID) + 1);

            //            package.Save();
            //        }

            //    }
            //}
            //System.Diagnostics.Process.Start(fileName);
            #endregion

            #region OptiClass debug
            foreach (var featureFilter in Common.Configuration.TestInputFeatures)
            {
                string fileName = DateTime.Today.ToShortDateString() + featureFilter + "_DebugTable_OptiClass_NoSigmoidFusion_predByAvgFromOrigDist_avgminNo0dist_FusedScore.xlsx";

                using (var package = new ExcelPackage(new FileInfo(fileName)))
                {
                    ExcelWorksheet summaryWs = ExcelHelper.GetWorkSheetFromPackage(package, "Summary");
                    ExcelHelper.SetDebugTableSummarySheetHeader(summaryWs);

                    Parallel.ForEach(Signers, (signer) => 
                    {
                        ExcelWorksheet ws = ExcelHelper.GetWorkSheetFromPackage(package, "Signer" + signer.ID);
                        ExcelHelper.SetOptiClassDebugTableHeader(ws);

                        var optimalClassifier = new OptimalDTWClassifier(featureFilter);

                        var genuines = signer.Signatures.FindAll(s => s.Origin == Origin.Genuine);
                        var forgeries = signer.Signatures.FindAll(s => s.Origin == Origin.Forged);


                        double threshold = optimalClassifier.Train(signer.Signatures);
                        double far = ClassifierHelper.CalculateFAR(forgeries, optimalClassifier);
                        double frr = ClassifierHelper.CalculateFRR(genuines, optimalClassifier);
                        double aer = (far + frr) / 2;

                        ExcelHelper.SetOptiClassDebugTableResults(ws, threshold, far, frr, aer);
                        ExcelHelper.SetOptiClassDebugTableSummarySheetRow(summaryWs, ws.Name, int.Parse(signer.ID) + 1);

                        ProgressValue += 1.0 / Signers.Count * 100;
                    });

                    package.Save();
                }


                System.Diagnostics.Process.Start(fileName);
            }
            #endregion

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

        private void GoDTWButton_Click(object sender, RoutedEventArgs e)
        {
            if (Signers == null)
                LoadSignatures();

            Signature signature1 = Signers[(int)SignerComboBox1.SelectedValue-1].Signatures[(int)SignatureComboBox1.SelectedIndex];
            Signature signature2 = Signers[(int)SignerComboBox2.SelectedValue - 1].Signatures[(int)SignatureComboBox2.SelectedIndex];

            //DTWScoreTextBlock.Text = Analyzer.GetCost(sig1, sig2, true).Cost.ToString();
            Dtw dtw = new Dtw(signature1, signature2, FeatureFilter);
            OwnDTWScoreTextBlock.Text = dtw.CalculateDtwScore().ToString();
            WarpingPathScoreTextBlock.Text = dtw.CalculateWarpingPathScore().ToString();
            FusionScoreTextBlock.Text = FusedScore.CalculateFusionOfDtwAndWPathScore(signature1, new Signature[] { signature2 }, FeatureFilter).ToString();

        }


        private void OkForAll_Click(object sender, RoutedEventArgs e)
        {
                //StatisticsMessagesTextBlock.Text = "Ez akár hosszabb ideig is eltarthat! Statisztika elkészítése folyamatban...";
                StatisticsMessagesTextBlock.Text = "This can take longer! Creation of statistics is in progress...";
                ThreadPool.QueueUserWorkItem(o => DoStatistics(false));
        }


        private async void OpenStatistics_Click(object sender, RoutedEventArgs e)
        {
            if (!new FileInfo(Common.Configuration.FusedScoreAutoClassificationName).Exists)
            {
                var view = new OpenStatisticDialog { };

                var result = await DialogHost.Show(view);

                switch ((bool)result)
                {
                    case true:
                        //StatisticsMessagesTextBlock.Text = "Ez akár hosszabb ideig is eltarthat! Statisztika elkészítése folyamatban...";
                        StatisticsMessagesTextBlock.Text = "This can take longer! Creation of statistics is in progress...";
                        ThreadPool.QueueUserWorkItem(o => DoStatistics());
                        break;
                    case false:
                        break;
                    default:
                        break;
                }
            }
            else
                System.Diagnostics.Process.Start(Common.Configuration.FusedScoreAutoClassificationName);
        }

        private void SignerComboBox1_SelectionChanged(object sender, RoutedEventArgs e)
        {
            SignerComboBox2.SelectedValue = SignerComboBox1.SelectedValue;
        }

        private void CreateDebugTables_Click(object sender, RoutedEventArgs e)
        {
            StatisticsMessagesTextBlock.Text = "This can take longer! Creation of debug tables is in progress...";
            ThreadPool.QueueUserWorkItem(o => CreateDebugTables());
        }
       

    }
}
