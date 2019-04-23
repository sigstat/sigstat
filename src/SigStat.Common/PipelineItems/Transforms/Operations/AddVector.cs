using SigStat.Common.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Extensions.Logging;
using SigStat.Common.Pipeline;
using Newtonsoft.Json;

namespace SigStat.Common.Transforms
{
    /// <summary>
    /// Adds a vector feature's elements to other features.
    /// <para>Default Pipeline Output: Pipeline Input</para>
    /// </summary>
    /// <example>
    /// Inputs are: Centroid.xy, X, Y .
    /// Adds Centroid.x to each element of X.
    /// Adds Centroid.y to each element of Y.
    /// </example>
    [JsonObject(MemberSerialization.OptOut)]
    public class AddVector : PipelineBase, ITransformation
    {
        [Input]
        
        public List<FeatureDescriptor<List<double>>> Inputs { get; set; }

        [Output]
        public List<FeatureDescriptor<List<double>>> Outputs { get; set; }

        private readonly FeatureDescriptor<List<double>> vectorFeature;//aggregated

        /// <summary>
        /// Initializes a new instance of the <see cref="AddVector"/> class with a vector feature.
        /// Don't forget to add as many Inputs as the vector's dimension.
        /// </summary>
        /// <param name="vectorFeature">A collection-type feature where each element represents a dimension of the vector.</param>
        public AddVector(FeatureDescriptor<List<double>> vectorFeature)
        {
            this.vectorFeature = vectorFeature;
        }

        /// <inheritdoc/>
        public void Transform(Signature signature)
        {
            //TODO HACK: a konstruktorban meg nem tudjuk elore, hogy hany db FD lesz, nem tudjuk ott inicializalni
            //ezt megoldana egy Transform() elott futtatott init
            if(Outputs==null)
                Outputs = Inputs;

            var vector = signature.GetFeature(vectorFeature);
            int dim = vector.Count;
            if (Inputs.Count != dim || Outputs.Count != dim)
            {
                this.LogError("Dimension mismatch");
                throw new /*SigStatTransform*/Exception();
            }
            var inputfeatures = Inputs.Select(ifd => signature.GetFeature(ifd)).ToList();
            for (int iF = 0; iF < dim; iF++)
            {
                var listFeature = inputfeatures[iF];
                for (int i = 0; i < listFeature.Count; i++)
                {
                    listFeature[i] += vector[iF];
                }

                signature.SetFeature(Outputs[iF], listFeature);
                Progress += 100 / dim;
            }
            Progress = 100;
        }

    }
}
