using GalaSoft.MvvmLight;
using SigStat.Common;
using SigStat.Common.Loaders;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SigStat.UI
{
    public class MainViewModel : ObservableObject
    {
        private ObservableCollection<Type> datasetLoaders;
        public ObservableCollection<Type> DatasetLoaders { get { return datasetLoaders; } set { Set(ref datasetLoaders, value); } }

        private Type selectedDatasetLoader;
        public Type SelectedDatasetLoader {get { return selectedDatasetLoader; } set { Set(ref selectedDatasetLoader, value); }}

        private ObservableCollection<Signer> signers;
        public ObservableCollection<Signer> Signers { get { return signers; } set { Set(ref signers, value); } }

        private Signer selectedSigner;
        public Signer SelectedSigner
        {
            get { return selectedSigner; }
            set { Set(ref selectedSigner, value); SelectedSignature = selectedSigner?.Signatures[0]; }
        }

        private Signature selectedSignature;
        public Signature SelectedSignature { get { return selectedSignature; } set { Set(ref selectedSignature, value); } }


        public MainViewModel()
        {
            DatasetLoaders = new ObservableCollection<Type>(
                typeof(Svc2004Loader).Assembly.GetTypes()
                .Where(t => t.GetInterface(typeof(IDataSetLoader).FullName) != null));

            SelectedDatasetLoader = typeof(Svc2004Loader);
        }
    }
}
