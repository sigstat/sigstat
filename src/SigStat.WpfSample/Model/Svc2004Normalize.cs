using SigStat.Common;
using SigStat.Common.Pipeline;
using SigStat.Common.Transforms;
using SigStat.WpfSample.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SigStat.WpfSample.Model
{
    public class Svc2004Normalize : SequentialTransformPipeline //ParallelTransformPipeline
    {
        public Svc2004Normalize()
        {
            foreach (var fd in DerivableSvc2004Features.All)
            {
                //Add(new Normalize().Input(fd).Output(fd));
            }
        }
    }
}
