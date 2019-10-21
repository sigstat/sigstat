using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SigStat.Common.Framework.Samplers;
using SigStat.Common.Helpers;
using SigStat.Common.Model;
using SigStat.Common.Pipeline;
using SigStat.Common.PipelineItems.Classifiers;
using SigStat.Common.PipelineItems.Transforms.Preprocessing;

namespace SigStat.Common.Test.Helpers.Serialization
{
    [TestClass]
    public class VerifierTests
    {
        [TestMethod]
        public void TestSerializer()
        {
            var verifier = new Verifier()
            {
                Pipeline = new SequentialTransformPipeline
                {
                    new NormalizeRotation()
                    {
                        InputX = Features.X, InputY = Features.Y, InputT = Features.T, OutputX = Features.X,
                        OutputY = Features.Y
                    },
                    new Scale() {InputFeature = Features.X, OutputFeature = Features.X},

                },
                Classifier = new OptimalDtwClassifier()
                {
                    Sampler = new FirstNSampler(10),
                    Features = new List<FeatureDescriptor>() {Features.X, Features.Y, Features.Pressure}
                }
            };
            var json = SerializationHelper.JsonSerialize(verifier);
            var expectedJson = @"{
  ""AllFeatures"": [
    ""Bounds | System.Drawing.RectangleF, System.Drawing.Primitives, Version=4.2.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"",
    ""Trimmed bounds | System.Drawing.Rectangle, System.Drawing.Primitives, Version=4.2.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"",
    ""Dpi | System.Int32, System.Private.CoreLib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=7cec85d7bea7798e"",
    ""X | System.Collections.Generic.List`1[[System.Double, System.Private.CoreLib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=7cec85d7bea7798e]], System.Private.CoreLib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=7cec85d7bea7798e"",
    ""Y | System.Collections.Generic.List`1[[System.Double, System.Private.CoreLib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=7cec85d7bea7798e]], System.Private.CoreLib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=7cec85d7bea7798e"",
    ""T | System.Collections.Generic.List`1[[System.Double, System.Private.CoreLib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=7cec85d7bea7798e]], System.Private.CoreLib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=7cec85d7bea7798e"",
    ""Button | System.Collections.Generic.List`1[[System.Boolean, System.Private.CoreLib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=7cec85d7bea7798e]], System.Private.CoreLib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=7cec85d7bea7798e"",
    ""Azimuth | System.Collections.Generic.List`1[[System.Double, System.Private.CoreLib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=7cec85d7bea7798e]], System.Private.CoreLib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=7cec85d7bea7798e"",
    ""Altitude | System.Collections.Generic.List`1[[System.Double, System.Private.CoreLib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=7cec85d7bea7798e]], System.Private.CoreLib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=7cec85d7bea7798e"",
    ""Pressure | System.Collections.Generic.List`1[[System.Double, System.Private.CoreLib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=7cec85d7bea7798e]], System.Private.CoreLib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=7cec85d7bea7798e"",
    ""Image | SixLabors.ImageSharp.Image`1[[SixLabors.ImageSharp.PixelFormats.Rgba32, SixLabors.ImageSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], SixLabors.ImageSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null"",
    ""Cog | System.Drawing.Point, System.Drawing.Primitives, Version=4.2.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"",
    ""ScaledFeature | System.Collections.Generic.List`1[[System.Double, System.Private.CoreLib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=7cec85d7bea7798e]], System.Private.CoreLib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=7cec85d7bea7798e""
  ],
  ""Pipeline"": {
    ""Items"": [
      {
        ""$type"": ""SigStat.Common.PipelineItems.Transforms.Preprocessing.NormalizeRotation, SigStat.Common"",
        ""InputX"":""X"",
        ""InputY"":""Y"",
        ""InputT"":""T"",
        ""OutputX"":""X"",
        ""OutputY"":""Y""
      },
      {
        ""$type"": ""SigStat.Common.PipelineItems.Transforms.Preprocessing.Scale, SigStat.Common"",
        ""InputFeature"":""X"",
        ""NewMinValue"": 0.0,
        ""NewMaxValue"": 1.0,
        ""OutputFeature"":""X""
      }
    ]
  },
  ""Classifier"": {
    ""$type"": ""SigStat.Common.PipelineItems.Classifiers.OptimalDtwClassifier, SigStat.Common"",
    ""Features"":[""X"",""Y"",""Pressure""],
    ""Sampler"": {
      ""$type"": ""SigStat.Common.Framework.Samplers.FirstNSampler, SigStat.Common"",
      ""N"": 10
    },
    ""DistanceFunction"": ""Accord.Math.Distance, Accord.Math, Version=3.8.0.0, Culture=neutral, PublicKeyToken=null|Euclidean|System.Double[];System.Double[]""
  }
}";
            Assert.AreEqual(expectedJson, json);
        }

        [TestMethod]
        public void TestDeserialization()
        {
            var expectedVerifier = new Verifier()
            {
                Pipeline = new SequentialTransformPipeline
                {
                    new NormalizeRotation()
                    {
                        InputX = Features.X, InputY = Features.Y, InputT = Features.T, OutputX = Features.X,
                        OutputY = Features.Y
                    },
                    new Scale() {InputFeature = Features.X, OutputFeature = Features.X},

                },
                Classifier = new OptimalDtwClassifier()
                {
                    Sampler = new FirstNSampler(10),
                    Features = new List<FeatureDescriptor>() { Features.X, Features.Y, Features.Pressure }
                }
            };
            var verifierJson = @"{
  ""AllFeatures"": [
    ""Bounds | System.Drawing.RectangleF, System.Drawing.Primitives, Version=4.2.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"",
    ""Trimmed bounds | System.Drawing.Rectangle, System.Drawing.Primitives, Version=4.2.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"",
    ""Dpi | System.Int32, System.Private.CoreLib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=7cec85d7bea7798e"",
    ""X | System.Collections.Generic.List`1[[System.Double, System.Private.CoreLib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=7cec85d7bea7798e]], System.Private.CoreLib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=7cec85d7bea7798e"",
    ""Y | System.Collections.Generic.List`1[[System.Double, System.Private.CoreLib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=7cec85d7bea7798e]], System.Private.CoreLib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=7cec85d7bea7798e"",
    ""T | System.Collections.Generic.List`1[[System.Double, System.Private.CoreLib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=7cec85d7bea7798e]], System.Private.CoreLib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=7cec85d7bea7798e"",
    ""Button | System.Collections.Generic.List`1[[System.Boolean, System.Private.CoreLib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=7cec85d7bea7798e]], System.Private.CoreLib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=7cec85d7bea7798e"",
    ""Azimuth | System.Collections.Generic.List`1[[System.Double, System.Private.CoreLib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=7cec85d7bea7798e]], System.Private.CoreLib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=7cec85d7bea7798e"",
    ""Altitude | System.Collections.Generic.List`1[[System.Double, System.Private.CoreLib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=7cec85d7bea7798e]], System.Private.CoreLib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=7cec85d7bea7798e"",
    ""Pressure | System.Collections.Generic.List`1[[System.Double, System.Private.CoreLib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=7cec85d7bea7798e]], System.Private.CoreLib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=7cec85d7bea7798e"",
    ""Image | SixLabors.ImageSharp.Image`1[[SixLabors.ImageSharp.PixelFormats.Rgba32, SixLabors.ImageSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]], SixLabors.ImageSharp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null"",
    ""Cog | System.Drawing.Point, System.Drawing.Primitives, Version=4.2.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"",
    ""ScaledFeature | System.Collections.Generic.List`1[[System.Double, System.Private.CoreLib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=7cec85d7bea7798e]], System.Private.CoreLib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=7cec85d7bea7798e""
  ],
  ""Pipeline"": {
    ""Items"": [
      {
        ""$type"": ""SigStat.Common.PipelineItems.Transforms.Preprocessing.NormalizeRotation, SigStat.Common"",
        ""InputX"":""X"",
        ""InputY"":""Y"",
        ""InputT"":""T"",
        ""OutputX"":""X"",
        ""OutputY"":""Y""
      },
      {
        ""$type"": ""SigStat.Common.PipelineItems.Transforms.Preprocessing.Scale, SigStat.Common"",
        ""InputFeature"":""X"",
        ""NewMinValue"": 0.0,
        ""NewMaxValue"": 1.0,
        ""OutputFeature"":""X""
      }
    ]
  },
  ""Classifier"": {
    ""$type"": ""SigStat.Common.PipelineItems.Classifiers.OptimalDtwClassifier, SigStat.Common"",
    ""Features"":[""X"",""Y"",""Pressure""],
    ""Sampler"": {
      ""$type"": ""SigStat.Common.Framework.Samplers.FirstNSampler, SigStat.Common"",
      ""N"": 10
    },
    ""DistanceFunction"": ""Accord.Math.Distance, Accord.Math, Version=3.8.0.0, Culture=neutral, PublicKeyToken=null|Euclidean|System.Double[];System.Double[]""
  }
}";
            var deserializedVerifier = SerializationHelper.Deserialize<Verifier>(verifierJson);
            Assert.IsTrue(deserializedVerifier.Pipeline.GetType() == typeof(SequentialTransformPipeline));
            Assert.IsTrue(deserializedVerifier.Classifier.GetType() == typeof(OptimalDtwClassifier));
        }
    }
}

