using SigStat.Common;
using SigStat.Common.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SigStat.WpfSample.Helpers
{
    public class MySampler
    {
        public static Sampler Basic
        {
            get
            {
                // TODO: remove or generalize
                return new Sampler(
                    (sl) => sl.Where(s => s.Origin == Origin.Genuine).Take(10).ToList(),
                    (sl) => sl.Where(s => s.Origin == Origin.Genuine).Skip(10).Take(10).ToList(),
                    (sl) => sl.Where(s => s.Origin == Origin.Forged).Take(10).ToList()
                    );
            }
        }

        public static Sampler AllReferences
        {
            get
            {
                // TODO: remove or generalize
                return new Sampler(
                    (sl) => sl,
                    (sl) => sl.Where(s => s.Origin == Origin.Genuine).Skip(10).Take(10).ToList(),
                    (sl) => sl.Where(s => s.Origin == Origin.Forged).Take(10).ToList()
                    );
            }
        }
    }
}
