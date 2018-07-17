using System;
using System.Collections.Generic;
using System.Text;

namespace SigStat.Common
{
    public class FeatureAttribute: Attribute
    {
        public string FeatureKey { get; set; }

        public FeatureAttribute(string featureKey)
        {
            FeatureKey = featureKey;
        }
    }
}
