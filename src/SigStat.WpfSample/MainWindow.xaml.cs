using SigStat.Common;
using SigStat.Common.Loaders;
using SigStat.WpfSample.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SigStat.WpfSample
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        //TODO: test
        private void OK_Click(object sender, RoutedEventArgs e)
        {
            
            Svc2004Loader loader = new Svc2004Loader(@"Databases\Online\SVC2004\Task2.zip", true);
            var signers = new List<Signer>(loader.EnumerateSigners(p => p == "01"));//Load the first signer only


            Signature baseSig = signers[0].Signatures[0];
            FeatureExtractor featureExtractor = new FeatureExtractor(baseSig);
            var fodX = featureExtractor.GetFirstOrderDifference(Features.X);
            //var fodfodX = featureExtractor.GetFirstOrderDifference(DerivedSvc2004Features.FODX);
            var lengthBasedFO = featureExtractor.GetLengthBasedFeatureFirstOrder();
            var acceleration = featureExtractor.GetAccelerations();
            var velocity = featureExtractor.GetVelocities();
            var sodX = featureExtractor.GetSecondOrderDifference(Features.X);
            var sineMeasure = featureExtractor.GetSineMeasure();
            Signature derivedSig = featureExtractor.Signature;
        }
    }
}
