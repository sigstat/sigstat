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
        public int samplerate { get; set; } = 1;
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
        public FeatureDescriptor<List<double>> InputT { get; set; } = Features.T;

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

        /// <inheritdoc/>
        public void Transform(Signature signature)
        {
            //Console.WriteLine(signature.Signer.ID + "   " + signature.ID);
            var xValues = new List<double>(signature.GetFeature(InputX));
            var yValues = new List<double>(signature.GetFeature(InputY));
           

            int pointsNum = xValues.Count();
          
            var newX = new List<double>();
            var newY = new List<double>();
            int s = 0;
            
                for (int i = 0; i < pointsNum; i = i + samplerate)
                {
                    newX.Add(xValues[i]);
                    newY.Add(yValues[i]);
               

            }
            if ((signature.Signer.ID == "0043" && signature.ID == "0043v01") || (signature.Signer.ID == "01" && signature.ID == "01"))
                Console.WriteLine("Signer= " + signature.Signer.ID + "   points= " + newX.Count());

            signature.SetFeature(OutputX, newX);
            signature.SetFeature(OutputY, newY);

        }


    

    }
}
