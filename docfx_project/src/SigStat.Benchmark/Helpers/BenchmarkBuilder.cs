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
                { "mcyt100", new MCYTLoader(Path.Combine(databasePath, "MCYT100.zip"), true)},
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

            b.Parameters.AddRange(config);

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
                        b.Verifier.Pipeline.Add(new NormalizeRotation2() { InputX = Features.X, InputY = Features.Y, OutputX = Features.X, OutputY = Features.Y });
                        break;
                    default:
                        throw new NotSupportedException("Unsupported rotation: " + config["Rotation"]);
                }
            }

            if (config.ContainsKey("FilterGap"))
            {
                switch (config["FilterGap"])
                {
                    case "none":
                        // Nothing to do here
                        break;
                    case "filter":
                        var filterFeatures = features.ToList();
                        if (!filterFeatures.Contains(Features.T))
                            filterFeatures.Add(Features.T);
                        if (!filterFeatures.Contains(Features.Y))
                            filterFeatures.Add(Features.Y);
                        if (!filterFeatures.Contains(Features.PointType))
                            filterFeatures.Add(Features.PointType);
                        b.Verifier.Pipeline.Add(new FilterPoints()
                        {
                            InputFeatures = filterFeatures,
                            OutputFeatures = filterFeatures,
                            KeyFeatureInput = Features.Pressure,
                            KeyFeatureOutput = Features.Pressure
                        });
                        break;
                    default:
                        throw new NotSupportedException("Unsupported gap filter: " + config["FilterGap"]);
                }
            }

            if (config.ContainsKey("FillGap"))
            {
                switch (config["FillGap"])
                {
                    case "none":
                        // Nothing to do here
                        break;
                    case "fill":
                        var fillingFeatures = features.ToList();
                        if (!fillingFeatures.Contains(Features.Y))
                            fillingFeatures.Add(Features.Y);
                        b.Verifier.Pipeline.Add(new FillPenUpDurations()
                        {
                            InputFeatures = fillingFeatures,
                            OutputFeatures = fillingFeatures,
                            TimeInputFeature = Features.T,
                            TimeOutputFeature = Features.T,
                            PressureInputFeature = Features.Pressure,
                            PressureOutputFeature = Features.Pressure,
                            PointTypeInputFeature = Features.PointType,
                            PointTypeOutputFeature = Features.PointType,
                            InterpolationType = interpolations[config["FillInterpolation"]]
                        });
                        break;
                    default:
                        throw new NotSupportedException("Unsupported gap fill: " + config["FillGap"]);
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
                        var resamplingFeatures = features.ToList();
                        if (!resamplingFeatures.Contains(Features.Pressure))
                            resamplingFeatures.Add(Features.Pressure);
                        if (!resamplingFeatures.Contains(Features.Y))
                            resamplingFeatures.Add(Features.Y);
                        if (!resamplingFeatures.Contains(Features.PointType))
                            resamplingFeatures.Add(Features.PointType);
                        b.Verifier.Pipeline.Add(new ResampleSamplesCountBased()
                        {
                            InputFeatures = resamplingFeatures,
                            OutputFeatures = resamplingFeatures,
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

            if (config.ContainsKey("Scaling"))
            {
                switch (config["Scaling"])
                {
                    case "none":
                        // Nothing to do here
                        break;
                    case "scale1":
                        foreach (var f in features)
                        {
                            b.Verifier.Pipeline.Add(new Scale() { InputFeature = f, OutputFeature = f, Mode = ScalingMode.Scaling1 });
                        }
                        break;
                    case "scaleS":
                        foreach (var f in features)
                        {
                            b.Verifier.Pipeline.Add(new Scale() { InputFeature = f, OutputFeature = f, Mode = ScalingMode.ScalingS });
                        }
                        break;
                    default:
                        throw new NotSupportedException("Unsupported scaling: " + config["Scaling"]);
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
                    case "to0":
                        foreach (var f in features)
                        {
                            b.Verifier.Pipeline.Add(new TranslatePreproc(OriginType.Minimum) { InputFeature = f, OutputFeature = f });
                        }
                        break;
                    case "toCog":
                        foreach (var f in features)
                        {
                            b.Verifier.Pipeline.Add(new TranslatePreproc(OriginType.CenterOfGravity) { InputFeature = f, OutputFeature = f });
                        }
                        break;
                    default:
                        throw new NotSupportedException("Unsupported translation: " + config["Translation"]);
                }
            }






            return b;

        }

        public Sampler GetSampler(string key)
        {
            // Only return a clone, to avoid side effects
            return samplers[key];
        }

        public IEnumerable<KeyValuePair<string, DataSetLoader>> GetLoaders()
        {
            return loaders;
        }


        public List<FeatureDescriptor<List<double>>> ParseFeatures(string featuresString)
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
