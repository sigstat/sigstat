using Newtonsoft.Json;
using SigStat.Common.Pipeline;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SigStat.Common.PipelineItems.Transforms.Preprocessing
{
    /// <summary>
    /// Origin specification for <see cref="TranslatePreproc"/>
    /// </summary>
    public enum OriginType
    {
        /// <summary>Center of gravity</summary>
        CenterOfGravity = 0,
        /// <summary>Minimum</summary>
        Minimum = 1,
        /// <summary>Maximum</summary>
        Maximum = 2,
        /// <summary>Predefined</summary>
        Predefined = 3
    }
    /// <summary>
    /// This transformations can be used to translate the coordinates of an online signature
    /// </summary>
    /// <seealso cref="SigStat.Common.PipelineBase" />
    /// <seealso cref="SigStat.Common.ITransformation" />
    [JsonObject(MemberSerialization.OptOut)]
    public class TranslatePreproc : PipelineBase, ITransformation
    {
        /// <summary>
        /// Input <see cref="FeatureDescriptor"/> (e.g. <see cref="Features.X"/>)
        /// </summary>
        [Input]
        public FeatureDescriptor<List<double>> InputFeature { get; set; }

        /// <summary>
        /// Output <see cref="FeatureDescriptor"/> (e.g. <see cref="Features.X"/>)
        /// </summary>
        [Output("TranslatedFeature")]
        public FeatureDescriptor<List<double>> OutputFeature { get; set; }


        /// <summary>
        /// Goal origin of the translation
        /// </summary>
        public OriginType GoalOrigin { get; set; } = OriginType.Predefined;

        private double _newOrigin = 0;
        /// <summary>
        /// New origin after the translation
        /// </summary>
        public double NewOrigin
        {
            get => _newOrigin;
            set
            {
                _newOrigin = value;
                // TODO: Make sure that goal origin and new origins are consistent
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TranslatePreproc"/> class.
        /// </summary>
        public TranslatePreproc() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="TranslatePreproc"/> class.
        /// </summary>
        /// <param name="goalOrigin">The goal origin.</param>
        public TranslatePreproc(OriginType goalOrigin)
        {
            GoalOrigin = goalOrigin;
        }


        /// <inheritdoc/>
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
                case OriginType.Maximum: //TODO: think over the proper behavior
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
