using SigStat.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SigStat.WpfSample.Common
{
    public static class Configuration
    {
        public const int DefaultSpacingParameter = 1;

        public static readonly List<FeatureDescriptor> DefaultInputFeatures = new List<FeatureDescriptor>(new FeatureDescriptor[] { Features.X, Features.Y });
    }
}
