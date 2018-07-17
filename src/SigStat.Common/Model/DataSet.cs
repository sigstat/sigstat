using System;
using System.Collections.Generic;
using System.Text;

namespace SigStat.Common
{
    public class DataSet
    {
        public virtual List<Signer> Signers { get; set; }

        public DataSet()
        {
            Signers = new List<Signer>();
        }
    }
}
