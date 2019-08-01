using Microsoft.Win32;
using SigStat.Common.Loaders;
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
            var loader = new SigComp15GermanLoader(dlg.FileName, true);
            VM.Signers = new ObservableCollection<Common.Signer>( loader.EnumerateSigners().OrderBy(s=>s.ID));

        }
    }
}
