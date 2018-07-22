using System;
using System.Collections.Generic;
using System.Text;

namespace SigStat.Common
{
    public interface IPiplineItem
    {        
    }

    public interface ITransformation : IPiplineItem
    {
        void Transform(Signature signature);
    }

    public interface IClassification : IPiplineItem
    {
        void Train(IEnumerable<Signature> signatures);
        void Test(Signature signature);
    }
}
