using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace SigStat.Common
{
    public class Signer//<TSignature> where TSignature: Signature
    {
        public string ID { get; set; }
        public virtual List<Signature> Signatures { get; set; } = new List<Signature>();

        public Signer() { }
    }
}
