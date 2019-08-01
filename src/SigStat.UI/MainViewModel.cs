using GalaSoft.MvvmLight;
using SigStat.Common;
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
        private ObservableCollection<Signer> signers;
        public ObservableCollection<Signer> Signers { get { return signers; } set { Set(ref signers, value); } }

        private Signer selectedSigner;
        public Signer SelectedSigner
        {
            get { return selectedSigner; }
            set { Set(ref selectedSigner, value); SelectedSignature = selectedSigner.Signatures[0]; }
        }

        private Signature selectedSignature;
        public Signature SelectedSignature { get { return selectedSignature; } set { Set(ref selectedSignature, value); } }

    }
}
