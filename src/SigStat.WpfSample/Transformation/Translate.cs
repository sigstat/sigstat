using SigStat.Common;
using SigStat.Common.Helpers;
using SigStat.Common.Pipeline;
using SigStat.Common.Transforms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SigStat.WpfSample.Transformation
{
    /// <summary>
    /// Translates values of a feature to a specific origin.
    /// <para>Pipeline Input type: List{double}</para>
    /// <para>Default Pipeline Output: (List{double}) xxTranslationResult</para>
    /// </summary>
    /// <remarks> This is a specific case of the <see cref="Map"/> transform. </remarks>
    public class Translate : PipelineBase, ITransformation
    {
        public FeatureDescriptor InputFeature { get; set; }
        public FeatureDescriptor OutputFeature { get; set; }
        public OriginType GoalOrigin { get; set; } = OriginType.Predefined;
        public double NewOrigin { get; set; }

        public Translate() { }

        /// <summary> Initializes a new instance of the <see cref="Map"/> class with specified settings. </summary>
        public Translate(FeatureDescriptor inputFeature, OriginType goalOrigin, FeatureDescriptor outputFeature = null)
        {
            this.InputFeature = inputFeature;
            GoalOrigin = goalOrigin;
            this.OutputFeature = outputFeature ?? InputFeature;
        }

        /// <summary> Initializes a new instance of the <see cref="Map"/> class with specified settings. </summary>
        public Translate(FeatureDescriptor inputFeature, double newOrigin, FeatureDescriptor outputFeature = null)
        {
            this.InputFeature = inputFeature;
            this.NewOrigin = newOrigin;
            this.OutputFeature = outputFeature ?? InputFeature;
        }


        public void Transform(Signature signature)
        {
            switch (GoalOrigin)
            {
                case OriginType.CenterOfGravity:
                    COGTransform(signature);
                    break;
                case OriginType.Minimum:
                    ExtremaTransform(signature, false);
                    break;
                case OriginType.Maximum:
                    ExtremaTransform(signature, true);
                    break;
                case OriginType.Predefined:
                    TranslateToPredefinedOrigin(signature, NewOrigin);
                    break;
                default:
                    break;
            }
        }

        private void COGTransform(Signature sig)
        {
            var translatedValues = new List<double>(sig.GetFeature<List<double>>(InputFeature));
            var cog = translatedValues.Average();

            for (int i = 0; i < translatedValues.Count; i++)
            {
                translatedValues[i] = translatedValues[i] - cog;
            }

            sig.SetFeature(OutputFeature, translatedValues);
        }

        private void ExtremaTransform(Signature sig, bool isMax)
        {
            var translatedValues = new List<double>(sig.GetFeature<List<double>>(InputFeature));

            var origin = isMax ? translatedValues.Max() : translatedValues.Min();

            for (int i = 0; i < translatedValues.Count; i++)
            {
                translatedValues[i] = translatedValues[i] - origin;
            }

            sig.SetFeature(OutputFeature, translatedValues);
        }

        private void TranslateToPredefinedOrigin(Signature sig, double newOrigin)
        {
            var translatedValues = new List<double>(sig.GetFeature<List<double>>(InputFeature));

            for (int i = 0; i < translatedValues.Count; i++)
            {
                translatedValues[i] = translatedValues[i] - newOrigin;
            }

            sig.SetFeature(OutputFeature, translatedValues);
        }
    }
}

