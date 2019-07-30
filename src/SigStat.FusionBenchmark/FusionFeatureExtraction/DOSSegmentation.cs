using SigStat.Common;
using SigStat.Common.Pipeline;
using System;
using System.Collections.Generic;
using System.Text;

namespace SigStat.FusionBenchmark.FusionFeatureExtraction
{
    class DOSSegmentation : PipelineBase, ITransformation
    {
        [Input]
        public FeatureDescriptor<List<double>> InputCurvature { get; set; }

        [Input]
        public FeatureDescriptor<List<bool>> InputButton { get; set; }

        [Output]
        public FeatureDescriptor<List<ComplexSegment>> OutputComplexSegments { get; set; }

        [Output]
        public FeatureDescriptor<List<SimpleSegment>> InputSimpleSegments { get; set; }

    public void Transform(Signature signature)
        {
        }
    }
}
