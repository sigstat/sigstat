using System;
using System.Collections.Generic;
using System.Linq;
using System.Collections.ObjectModel;
using System.Text;

namespace SigStat.Common
{
    public class Signer//<TSignature> where TSignature: Signature
    {
        public string ID { get; set; }
        public virtual List<Signature> Signatures { get; set; } = new List<Signature>();

        public List<Signature> Originals
        {
            get => Signatures.FindAll((s) => s.Origin == Origin.Genuine); 
        }

        public List<Signature> Forgeries
        {
            get => Signatures.FindAll((s) => s.Origin == Origin.Forged);
        }

        public Signer() { }
    }
}
