using Newtonsoft.Json;
using SigStat.Common.Pipeline;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SigStat.Common.PipelineItems.Transforms.Preprocessing
{
    /// <summary>
    /// Performs rotation normalization on the online signature
    /// </summary>
    /// <seealso cref="SigStat.Common.PipelineBase" />
    /// <seealso cref="SigStat.Common.ITransformation" />
    [JsonObject(MemberSerialization.OptOut)]
    public class SampleRate : PipelineBase, ITransformation
    {

        /// <summary>
        /// Gets or sets the input feature representing the X coordinates of an online signature
        /// </summary>
        [Input]
        public int samplerate { get; set; } 
        /// <summary>
        /// Gets or sets the input feature representing the X coordinates of an online signature
        /// </summary>
        [Input]
        public FeatureDescriptor<List<double>> InputX { get; set; } = Features.X;

        /// <summary>
        /// Gets or sets the input feature representing the Y coordinates of an online signature
        /// </summary>
        [Input]
        public FeatureDescriptor<List<double>> InputY { get; set; } = Features.Y;

        /// <summary>
        /// Gets or sets the input feature representing the timestamps of an online signature
        /// </summary>
        [Input]
        public FeatureDescriptor<List<double>> InputP { get; set; } = Features.Pressure;

        /// <summary>
        /// Gets or sets the output feature representing the X coordinates of an online signature
        /// </summary>
        [Output]
        public FeatureDescriptor<List<double>> OutputX { get; set; } = Features.X;

        /// <summary>
        /// Gets or sets the input feature representing the Y coordinates of an online signature
        /// </summary>
        [Output]
        public FeatureDescriptor<List<double>> OutputY { get; set; } = Features.Y;
        /// <summary>
        /// Gets or sets the input feature representing the Pressure values of an online signature
        /// </summary>
        [Output]
        public FeatureDescriptor<List<double>> OutputP { get; set; } = Features.Pressure;

        /// <inheritdoc/>
        public void Transform(Signature signature)
        {
            var xValues = new List<double>(signature.GetFeature(InputX));
            var yValues = new List<double>(signature.GetFeature(InputY));
            var pValues = new List<double>(signature.GetFeature(InputP));


            int pointsNum = xValues.Count;
          
            var newX = new List<double>();
            var newY = new List<double>();
            var newP = new List<double>();

            for (int i = 0; i < pointsNum; i = i + samplerate)
                {
                    newX.Add(xValues[i]);
                    newY.Add(yValues[i]);
                    newP.Add(pValues[i]);


            }
            
            signature.SetFeature(OutputX, newX);
            signature.SetFeature(OutputY, newY);
            signature.SetFeature(OutputP, newP);

        }


    

    }
}
