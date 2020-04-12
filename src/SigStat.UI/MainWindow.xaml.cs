using Microsoft.Win32;
using SigStat.Common;
using SigStat.Common.Loaders;
using SigStat.Common.PipelineItems.Transforms.Preprocessing;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace SigStat.UI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainViewModel VM { get; set; } = new MainViewModel();
        public object NormalizeRotaiton2 { get; private set; }

        public MainWindow()
        {
            InitializeComponent();
            DataContext = VM;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var databaseDir = Environment.GetEnvironmentVariable("SigStatDB");
            OpenFileDialog dlg = new OpenFileDialog();
            if (databaseDir != null)
                dlg.InitialDirectory = databaseDir;
            if (dlg.ShowDialog() != true)
                return;
            var ctor = VM.SelectedDatasetLoader.GetConstructor(new[] { typeof(string), typeof(bool) });
            var loader = (IDataSetLoader)ctor.Invoke(new object[] { dlg.FileName, true });
            //VM.Signers = new ObservableCollection<Common.Signer>(loader.EnumerateSigners().OrderBy(s => s.ID));


            var signers = new ObservableCollection<Common.Signer>(loader.EnumerateSigners().OrderBy(s => s.ID));
            //NormalizeRotation2 tr = new NormalizeRotation2()
            //{
            //    InputX = Features.X,
            //    OutputX = Features.X,
            //    InputY = Features.Y,
            //    OutputY = Features.Y
            //};
            FillPenUpDurations tr = new FillPenUpDurations()
            {
                InputFeatures = { Features.X, Features.Y },
                OutputFeatures = { Features.X, Features.Y },
                PressureInputFeature = Features.Pressure, PressureOutputFeature = Features.Pressure,
                TimeInputFeature = Features.T, TimeOutputFeature = Features.T,
                PointTypesInputFeature = Features.PointTypes, PointTypesOutputFeature = Features.PointTypes,
                InterpolationType = typeof(CubicInterpolation)
            };
            for (int signerId = 0; signerId < signers.Count(); signerId++)
            {
                int n = signers[signerId].Signatures.Count;
                for (int signatureId = 0; signatureId < n; signatureId++)
                {
                    var signature = signers[signerId].Signatures[signatureId];
                    tr.Transform(signature);
                }
            }

            VM.Signers = signers;

        }
    }
}
