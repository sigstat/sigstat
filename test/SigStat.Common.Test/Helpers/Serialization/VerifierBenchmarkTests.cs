using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SigStat.Common.Framework.Samplers;
using SigStat.Common.Helpers;
using SigStat.Common.Loaders;
using SigStat.Common.Model;
using SigStat.Common.Pipeline;
using SigStat.Common.PipelineItems.Classifiers;
using SigStat.Common.PipelineItems.Transforms.Preprocessing;

namespace SigStat.Common.Test.Helpers.Serialization
{
    [TestClass]
    public class VerifierBenchmarkTests
    {
        [TestMethod]
        public void TestSerializer()
        {
            var verifier = new VerifierBenchmark()
            {
                Loader = new Svc2004Loader(@"Databases\Online\SVC2004\Task2.zip".GetPath(), true),
                Verifier = new Verifier()
                {
                    Pipeline = new SequentialTransformPipeline
                    {
                        new NormalizeRotation(){InputX = Features.X, InputY = Features.Y, InputT = Features.T, OutputX = Features.X, OutputY=Features.Y},

                        new Scale() {InputFeature = Features.X, OutputFeature = Features.X},
                        new Scale() {InputFeature = Features.Y, OutputFeature = Features.Y},
                        new FillPenUpDurations()
                        {
                            InputFeatures = new List<FeatureDescriptor<List<double>>>(){ Features.X, Features.Y, Features.Pressure },
                            OutputFeatures = new List<FeatureDescriptor<List<double>>>() { Features.X, Features.Y, Features.Pressure },
                            InterpolationType = typeof(CubicInterpolation),
                            TimeInputFeature =Features.T,
                            TimeOutputFeature = Features.T
                        }
                    }
                    ,
                    Classifier = new OptimalDtwClassifier()
                    {
                        Sampler = new FirstNSampler(10),
                        Features = new List<FeatureDescriptor>() { Features.X, Features.Y, Features.Pressure }
                    }
                },
                Sampler = new FirstNSampler(10),
                Logger = new SimpleConsoleLogger(),
            };
            var json = SerializationHelper.JsonSerialize(verifier);
            var expectedJson = @"{
  ""Verifier"": {
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
      ""ScaledFeature | System.Collections.Generic.List`1[[System.Double, System.Private.CoreLib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=7cec85d7bea7798e]], System.Private.CoreLib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=7cec85d7bea7798e"",
      ""FilledTime | System.Collections.Generic.List`1[[System.Double, System.Private.CoreLib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=7cec85d7bea7798e]], System.Private.CoreLib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=7cec85d7bea7798e""
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
        },
        {
          ""$type"": ""SigStat.Common.PipelineItems.Transforms.Preprocessing.Scale, SigStat.Common"",
          ""InputFeature"":""Y"",
          ""NewMinValue"": 0.0,
          ""NewMaxValue"": 1.0,
          ""OutputFeature"":""Y""
        },
        {
          ""$type"": ""SigStat.Common.PipelineItems.Transforms.Preprocessing.FillPenUpDurations, SigStat.Common"",
          ""TimeInputFeature"":""T"",
          ""InputFeatures"":[""X"",""Y"",""Pressure""],
          ""TimeOutputFeature"":""T"",
          ""OutputFeatures"":[""X"",""Y"",""Pressure""],
          ""InterpolationType"": ""SigStat.Common.PipelineItems.Transforms.Preprocessing.CubicInterpolation, SigStat.Common, Version=0.1.1.0, Culture=neutral, PublicKeyToken=null""
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
  },
  ""Parameters"": [],
  ""Loader"": {
    ""$type"": ""SigStat.Common.Loaders.Svc2004Loader, SigStat.Common"",
    ""DatabasePath"": ""Databases\\Online\\SVC2004\\Task2.zip"",
    ""StandardFeatures"": true
  },
  ""Sampler"": {
    ""$type"": ""SigStat.Common.Framework.Samplers.FirstNSampler, SigStat.Common"",
    ""N"": 10
  }
}";
            Assert.AreEqual(expectedJson, json);
        }

        [TestMethod]
        public void TestDeserialization()
        {
            var expectedVerifier = new VerifierBenchmark()
            {
                Loader = new Svc2004Loader(@"Databases\Online\SVC2004\Task2.zip".GetPath(), true),
                Verifier = new Verifier()
                {
                    Pipeline = new SequentialTransformPipeline
                    {
                        new NormalizeRotation(){InputX = Features.X, InputY = Features.Y, InputT = Features.T, OutputX = Features.X, OutputY=Features.Y},

                        new Scale() {InputFeature = Features.X, OutputFeature = Features.X},
                        new Scale() {InputFeature = Features.Y, OutputFeature = Features.Y},
                        new FillPenUpDurations()
                        {
                            InputFeatures = new List<FeatureDescriptor<List<double>>>(){ Features.X, Features.Y, Features.Pressure },
                            OutputFeatures = new List<FeatureDescriptor<List<double>>>() { Features.X, Features.Y, Features.Pressure },
                            InterpolationType = typeof(CubicInterpolation),
                            TimeInputFeature =Features.T,
                            TimeOutputFeature = Features.T
                        }
                    }
                    ,
                    Classifier = new OptimalDtwClassifier()
                    {
                        Sampler = new FirstNSampler(10),
                        Features = new List<FeatureDescriptor>() { Features.X, Features.Y, Features.Pressure }
                    }
                },
                Sampler = new FirstNSampler(10),
                Logger = new SimpleConsoleLogger(),
            };
            var verifierJson = @"{
  ""Verifier"": {
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
      ""ScaledFeature | System.Collections.Generic.List`1[[System.Double, System.Private.CoreLib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=7cec85d7bea7798e]], System.Private.CoreLib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=7cec85d7bea7798e"",
      ""FilledTime | System.Collections.Generic.List`1[[System.Double, System.Private.CoreLib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=7cec85d7bea7798e]], System.Private.CoreLib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=7cec85d7bea7798e""
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
        },
        {
          ""$type"": ""SigStat.Common.PipelineItems.Transforms.Preprocessing.Scale, SigStat.Common"",
          ""InputFeature"":""Y"",
          ""NewMinValue"": 0.0,
          ""NewMaxValue"": 1.0,
          ""OutputFeature"":""Y""
        },
        {
          ""$type"": ""SigStat.Common.PipelineItems.Transforms.Preprocessing.FillPenUpDurations, SigStat.Common"",
          ""TimeInputFeature"":""T"",
          ""InputFeatures"":[""X"",""Y"",""Pressure""],
          ""TimeOutputFeature"":""T"",
          ""OutputFeatures"":[""X"",""Y"",""Pressure""],
          ""InterpolationType"": ""SigStat.Common.PipelineItems.Transforms.Preprocessing.CubicInterpolation, SigStat.Common, Version=0.1.1.0, Culture=neutral, PublicKeyToken=null""
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
  },
  ""Parameters"": [],
  ""Loader"": {
    ""$type"": ""SigStat.Common.Loaders.Svc2004Loader, SigStat.Common"",
    ""DatabasePath"": ""Databases\\Online\\SVC2004\\Task2.zip"",
    ""StandardFeatures"": true
  },
  ""Sampler"": {
    ""$type"": ""SigStat.Common.Framework.Samplers.FirstNSampler, SigStat.Common"",
    ""N"": 10
  }
}";
            var deserializedVerifier = SerializationHelper.Deserialize<VerifierBenchmark>(verifierJson);
            Assert.IsTrue(deserializedVerifier.Verifier.GetType() == typeof(Verifier));
            Assert.IsTrue(deserializedVerifier.Loader.GetType() == typeof(Svc2004Loader));
            Assert.IsTrue(deserializedVerifier.Sampler.GetType() == typeof(FirstNSampler));
        }
    }
}
