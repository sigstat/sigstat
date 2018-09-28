using SigStat.Common;
using SigStat.WpfSample.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SigStat.WpfSample.Model
{
    /// <summary>
    /// Maps values of a feature to 0.0 - 1.0 range.
    /// <para>Pipeline Input type: List{double}</para>
    /// <para>Default Pipeline Output: (List{double}) NormalizationResult</para>
    /// </summary>
    /// <remarks> This is a specific case of the <see cref="Map"/> transform. </remarks>
    public class DerivedSvc2004FeatureExtractor : PipelineBase, ITransformation
    {

        /// <inheritdoc/>
        public void Transform(Signature signature)
        {
            var extractor = new FeatureExtractor(signature);
            extractor.GetAllDerivedSVC2004Features();
        }

    }
}
