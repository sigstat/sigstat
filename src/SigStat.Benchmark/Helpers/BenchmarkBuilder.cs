using SigStat.Common;
using SigStat.Common.Framework.Samplers;
using SigStat.Common.Helpers;
using SigStat.Common.Loaders;
using SigStat.Common.Model;
using SigStat.Common.Pipeline;
using SigStat.Common.PipelineItems.Classifiers;
using SigStat.Common.PipelineItems.Transforms.Preprocessing;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace SigStat.Benchmark
{
    public class BenchmarkBuilder
    {
        Dictionary<string, Sampler> samplers;
        Dictionary<string, DataSetLoader> loaders;
        Dictionary<string, Type> interpolations;

        public BenchmarkBuilder(string databasePath = null)
        {
            samplers = new Dictionary<string, Sampler>()
            {
                { "s1", new FirstNSampler(10) },
                { "s2", new LastNSampler(10) },
                { "s3", new EvenNSampler(10) },
                { "s4", new OddNSampler(10) },
            };

            if (databasePath == null)
                databasePath = Environment.GetEnvironmentVariable("SigStatDB");

            loaders = new Dictionary<string, DataSetLoader>()
            {
                { "svc2004", new Svc2004Loader(Path.Combine(databasePath, "SVC2004.zip"), true)},
                { "mcyt", new MCYTLoader(Path.Combine(databasePath, "MCYT100.zip"), true)},
                { "dutch", new SigComp11DutchLoader(Path.Combine(databasePath, "SigComp11_Dutch.zip"), true)},
                { "chinese", new SigComp11ChineseLoader(Path.Combine(databasePath, "SigComp11Chinese.zip"), true)},
                { "german", new SigComp15GermanLoader(Path.Combine(databasePath, "SigWiComp2015_German.zip"), true)},
                { "japanese", new SigComp13JapaneseLoader(Path.Combine(databasePath, "SigWiComp2013_Japanese.zip"), true)}
            };

            interpolations = new Dictionary<string, Type>()
            {
                {"cubic", typeof(CubicInterpolation)},
                {"linear", typeof(LinearInterpolation)}
            };
        }




        static RelativeScale pRelativeScale = new RelativeScale() { InputFeature = Features.Pressure, ReferenceFeature = Features.Y, OutputFeature = Features.Pressure };
        static UniformScale xyUniformScale = new UniformScale() { BaseDimension = Features.X, BaseDimensionOutput = Features.X, ProportionalDimension = Features.Y, ProportionalDimensionOutput = Features.Y };
        static UniformScale yxUniformScale = new UniformScale() { BaseDimension = Features.Y, BaseDimensionOutput = Features.Y, ProportionalDimension = Features.X, ProportionalDimensionOutput = Features.X };

        public VerifierBenchmark Build(Dictionary<string, string> config)
        {

            VerifierBenchmark b = new VerifierBenchmark()
            {
                Loader = loaders[config["Database"]],
                Sampler = samplers[config["Split"]],
                Verifier = new Verifier()
            };

            var features = ParseFeatures(config["Feature"]);
            var dft = typeof(Func<double[], double[], double>);
            var distance = (Func<double[], double[], double>)typeof(Accord.Math.Distance)
                    //.GetMethod(config["Distance"], new Type[] { dft })
                    .GetMethods().First(i => 
                        i.Name == config["Distance"] && 
                        i.GetParameters()[0].ParameterType == typeof(double[]))
                    .CreateDelegate(dft);

            switch (config["Classifier"])
            {
                case "Dtw":
                    b.Verifier.Classifier = new DtwClassifier { Features = features.Cast<FeatureDescriptor>().ToList(), DistanceFunction = distance };
                    break;
                case "OptimalDtw":
                    b.Verifier.Classifier = new OptimalDtwClassifier { Features = features.Cast<FeatureDescriptor>().ToList(), DistanceFunction = distance, Sampler = samplers[config["Split"]] };
                    break;
                default:
                    throw new NotSupportedException("Unsupported feature: " + config["Feature"]);
            }


            if (config.ContainsKey("Rotation"))
            {
                switch (config["Rotation"])
                {
                    case "none":
                        // Nothing to do here
                        break;
                    case "rotation":
                        b.Verifier.Pipeline.Add(new NormalizeRotation() { InputX = Features.X, InputY = Features.Y, InputT = Features.T, OutputX = Features.X, OutputY = Features.Y });
                        break;
                    default:
                        throw new NotSupportedException("Unsupported rotation: " + config["Rotation"]);
                }
            }

            if (config.ContainsKey("Gap"))
            {
                switch (config["Gap"])
                {
                    case "none":
                        // Nothing to do here
                        break;
                    case "filter":
                        b.Verifier.Pipeline.Add(new FilterPoints() 
                        { 
                            InputFeatures = features, 
                            OutputFeatures = features, 
                            KeyFeatureInput = Features.Pressure, 
                            KeyFeatureOutput = Features.Pressure 
                        });
                        break;
                    case "fill":
                        b.Verifier.Pipeline.Add(new FillPenUpDurations()
                        {
                            InputFeatures = features,
                            OutputFeatures = features,
                            TimeInputFeature = Features.T,
                            TimeOutputFeature = Features.T,
                            InterpolationType = interpolations[config["FillInterpolation"]]
                        });
                        break;
                    default:
                        throw new NotSupportedException("Unsupported gap: " + config["Gap"]);
                }
            }



            if (config.ContainsKey("Resampling"))
            {
                switch (config["Resampling"])
                {
                    case "none":
                        // Nothing to do here
                        break;
                    case "samples":
                        b.Verifier.Pipeline.Add(new ResampleSamplesCountBased()
                        {
                            InputFeatures = features,
                            OutputFeatures = features,
                            OriginalTFeature = Features.T,
                            ResampledTFeature = Features.T,
                            NumOfSamples = int.Parse(config["SampleCount"]),
                            InterpolationType = interpolations[config["ResamplingInterpolation"]]
                        });
                        break;
                    default:
                        throw new NotSupportedException("Unsupported resampling: " + config["Resampling"]);
                }
            }

            var cxTranslate = new TranslatePreproc(OriginType.CenterOfGravity) { InputFeature = Features.X, OutputFeature = Features.X };
            var cyTranslate = new TranslatePreproc(OriginType.CenterOfGravity) { InputFeature = Features.Y, OutputFeature = Features.Y };
            var x0Translate = new TranslatePreproc(OriginType.Minimum) { InputFeature = Features.X, OutputFeature = Features.X };
            var y0Translate = new TranslatePreproc(OriginType.Minimum) { InputFeature = Features.Y, OutputFeature = Features.Y };

            if (config.ContainsKey("Translation"))
            {
                switch (config["Translation"])
                {
                    case "none":
                        // Nothing to do here
                        break;
                    case "X0":
                        b.Verifier.Pipeline.Add(x0Translate);
                        break;
                    case "Y0":
                        b.Verifier.Pipeline.Add(y0Translate);
                        break;
                    case "XY0":
                        b.Verifier.Pipeline.Add(x0Translate); 
                        b.Verifier.Pipeline.Add(y0Translate);
                        break;
                    case "CogX":
                        b.Verifier.Pipeline.Add(cxTranslate);
                        break;
                    case "CogY":
                        b.Verifier.Pipeline.Add(cyTranslate);
                        break;
                    case "CogXY":
                        b.Verifier.Pipeline.Add(cxTranslate);
                        b.Verifier.Pipeline.Add(cyTranslate);
                        break;
                    default:
                        throw new NotSupportedException("Unsupported translation: " + config["Translation"]);
                }
            }
            
            if (config.ContainsKey("Scaling"))
            {
                switch (config["Scaling"])
                {
                    case "none":
                        // Nothing to do here
                        break;
                    case "01":
                        foreach (var f in features)
                        {
                            b.Verifier.Pipeline.Add(new Scale() { InputFeature = f, OutputFeature = f });
                        }
                        break;
                    case "s":
                        foreach (var f in features)
                        {
                            b.Verifier.Pipeline.Add(new Scale() { InputFeature = f, OutputFeature = f });
                        }
                        break;
                    default:
                        throw new NotSupportedException("Unsupported classifier: " + config["Classifier"]);
                }
            }





            return b;

        }


        private List<FeatureDescriptor<List<double>>> ParseFeatures(string featuresString)
        {
            switch (featuresString)
            {
                case "X":
                    return new List<FeatureDescriptor<List<double>>> { Features.X };
                case "Y":
                    return new List<FeatureDescriptor<List<double>>> { Features.Y };
                case "P":
                    return new List<FeatureDescriptor<List<double>>> { Features.Pressure };
                case "XY":
                    return new List<FeatureDescriptor<List<double>>> { Features.X, Features.Y };
                case "XP":
                    return new List<FeatureDescriptor<List<double>>> { Features.X, Features.Pressure };
                case "YP":
                    return new List<FeatureDescriptor<List<double>>> { Features.Y, Features.Pressure };
                case "XYP":
                    return new List<FeatureDescriptor<List<double>>> { Features.X, Features.Y, Features.Pressure };
                default:
                    throw new NotSupportedException("Unsupported feature: " + featuresString);

            }
        }
    }
}
