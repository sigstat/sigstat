using SigStat.Common.Pipeline;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SigStat.Common.PipelineItems.Transforms.Preprocessing
{
    public enum OriginType
    {
        CenterOfGravity,
        Minimum,
        Maximum,
        Predefined
    }

    public class TranslatePreproc : PipelineBase, ITransformation
    {
        [Input]
        public FeatureDescriptor<List<double>> InputFeature { get; set; }

        [Output("TranslatedFeature")]
        public FeatureDescriptor<List<double>> OutputFeature { get; set; }


        public OriginType GoalOrigin { get; set; } = OriginType.Predefined;

        private double _newOrigin = 0;
        public double NewOrigin
        {
            get => _newOrigin;
            set
            {
                _newOrigin = value;
                GoalOrigin = OriginType.Predefined;
            }
        }

        public TranslatePreproc() { }

        public TranslatePreproc(OriginType goalOrigin)
        {
            GoalOrigin = goalOrigin;
        }

        public TranslatePreproc(double newOrigin)
        {
            NewOrigin = newOrigin;
            GoalOrigin = OriginType.Predefined;
        }


        public void Transform(Signature signature)
        {
            if (InputFeature == null || OutputFeature == null)
                throw new NullReferenceException("Input or output feature is null");

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
            var translatedValues = new List<double>(sig.GetFeature(InputFeature));
            var cog = translatedValues.Average();
            _newOrigin = cog;

            for (int i = 0; i < translatedValues.Count; i++)
            {
                translatedValues[i] = translatedValues[i] - cog;
            }

            sig.SetFeature(OutputFeature, translatedValues);
        }

        private void ExtremaTransform(Signature sig, bool isMax)
        {
            var translatedValues = new List<double>(sig.GetFeature(InputFeature));

            var origin = isMax ? translatedValues.Max() : translatedValues.Min();
            _newOrigin = origin;

            for (int i = 0; i < translatedValues.Count; i++)
            {
                translatedValues[i] = translatedValues[i] - origin;
            }

            sig.SetFeature(OutputFeature, translatedValues);
        }

        private void TranslateToPredefinedOrigin(Signature sig, double newOrigin)
        {
            var translatedValues = new List<double>(sig.GetFeature(InputFeature));

            for (int i = 0; i < translatedValues.Count; i++)
            {
                translatedValues[i] = translatedValues[i] - newOrigin;
            }

            sig.SetFeature(OutputFeature, translatedValues);
        }
    }
}
