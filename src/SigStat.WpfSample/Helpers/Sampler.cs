using SigStat.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SigStat.WpfSample.Helpers
{
    public class Sampler
    {
        public List<Signature> TrainingOriginals { get; private set; }
        public List<Signature> GenuineTestSignatures { get; private set; }
        public List<Signature> ForgedTestSignatures { get; private set; }

        //TODO: range helyett jobb nevet találni
        public Sampler(List<Signature> signatures, int range)
        {
            List<Signature> originals = signatures.FindAll(s => s.Origin == Origin.Genuine);
            List<Signature> forgeries = signatures.FindAll(s => s.Origin == Origin.Forged);

            if (range * 2 > originals.Count)
                throw new ArgumentOutOfRangeException("The parameter range is too high");

            originals = Shuffle(originals);
            forgeries = Shuffle(forgeries);

            TrainingOriginals = originals.GetRange(0, range);
            GenuineTestSignatures = originals.GetRange(range, range);
            ForgedTestSignatures = forgeries.GetRange(0, range);
        }

       private List<Signature> Shuffle(List<Signature> list)
        {
            Random rnd = new Random();
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = rnd.Next(n + 1);
                Signature value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
            return list;
        }
    }
}
