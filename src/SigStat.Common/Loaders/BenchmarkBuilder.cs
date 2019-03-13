using SigStat.Common.Helpers;
using SigStat.Common.Pipeline;
using SigStat.Common.PipelineItems.Classifiers;
using SigStat.Common.PipelineItems.Transforms.Preprocessing;
using System;
using System.Collections.Generic;
using System.Text;

namespace SigStat.Common.Loaders
{
    public static class BenchmarkBuilder
    {
        public static VerifierBenchmark Build(BenchmarkConfig config)
        {
            VerifierBenchmark b = new VerifierBenchmark();
            switch (config.Sampling)
            {
                case "S1":
                    b.Sampler = new SVC2004Sampler();
                    break;
                case "S2"://TODO: replace with new samplers
                    b.Sampler = new SVC2004Sampler();
                    break;
                case "S3":
                    b.Sampler = new SVC2004Sampler();
                    break;
                default:
                    break;
            }
            switch (config.Database)
            {
                case "SVC2004":
                    b.Loader = new Svc2004Loader(@"Databases\Online\SVC2004\Task2.zip", true);
                    break;
                case "MCYT100":
                    b.Loader = new MCYTLoader(@"Databases\Online\MCYT100\MCYT_Signature_100.zip", true);
                    break;
                case "..."://TODO: add 3rd db
                    b.Loader = null;
                    break;
                default:
                    break;
            }

            var pipeline = new SequentialTransformPipeline();

            var toFilter = new List<FeatureDescriptor<List<double>>>()
            {
                Features.X, Features.Y, Features.Azimuth, Features.Altitude
            };
            switch (config.Filter)
            {
                case "P":
                    pipeline.Add(new FilterPoints() {
                        InputFeatures = toFilter,
                        OutputFeatures = toFilter,
                        KeyFeatureInput = Features.Pressure,
                        KeyFeatureOutput = Features.Pressure
                    });
                    break;
                case "None":
                default:
                    break;
            }

            switch (config.Rotation)
            {
                case true:
                    pipeline.Add(new NormalizeRotation() { InputX = Features.X, InputY = Features.Y, InputT = Features.T, OutputX = Features.X, OutputY = Features.Y });
                    break;
                case false:
                default:
                    break;
            }

            switch (config.TranslationScaling.Translation)
            {
                case "CogToOriginX":
                    pipeline.Add(new TranslatePreproc(OriginType.CenterOfGravity) { InputFeature = Features.X, OutputFeature = Features.X });
                    break;
                case "CogToOriginY":
                    pipeline.Add(new TranslatePreproc(OriginType.CenterOfGravity) { InputFeature = Features.Y, OutputFeature = Features.Y });
                    break;
                case "CogToOriginXY":
                    pipeline.Add(new TranslatePreproc(OriginType.CenterOfGravity) { InputFeature = Features.X, OutputFeature = Features.X });
                    pipeline.Add(new TranslatePreproc(OriginType.CenterOfGravity) { InputFeature = Features.Y, OutputFeature = Features.Y });
                    break;
                case "BottomLeftToOrigin":
                    pipeline.Add(new TranslatePreproc(OriginType.Minimum) { InputFeature = Features.X, OutputFeature = Features.X });
                    pipeline.Add(new TranslatePreproc(OriginType.Minimum) { InputFeature = Features.Y, OutputFeature = Features.Y });
                    break;
                case "None":
                default:
                    break;
            }

            //kulonbozo scale-ek kizarjak egymast, ezert osszevonhatjuk
            switch (config.TranslationScaling.Scaling)
            {
                case "X01":
                    pipeline.Add(new Scale() { InputFeature = Features.X, OutputFeature = Features.X });
                    break;
                case "Y01":
                    pipeline.Add(new Scale() { InputFeature = Features.Y, OutputFeature = Features.Y });
                    break;
                case "X01Y01":
                    pipeline.Add(new Scale() { InputFeature = Features.X, OutputFeature = Features.X });
                    pipeline.Add(new Scale() { InputFeature = Features.Y, OutputFeature = Features.Y });
                    break;
                case "X01Y0prop":
                    pipeline.Add(new UniformScale() { BaseDimension = Features.X, BaseDimensionOutput = Features.X, ProportionalDimension = Features.Y, ProportionalDimensionOutput = Features.Y });
                    break;
                case "Y01X0prop":
                    pipeline.Add(new UniformScale() { BaseDimension = Features.Y, BaseDimensionOutput = Features.Y, ProportionalDimension = Features.X, ProportionalDimensionOutput = Features.X });
                    break;
                case "None":
                default:
                    break;
            }

            var featurelist = new List<FeatureDescriptor<List<double>>>()
            {
                Features.X, Features.Y, Features.Pressure, Features.Azimuth, Features.Altitude
            };

            IInterpolation ip = new LinearInterpolation();
            switch (config.Interpolation)
            {
                case "Cubic":
                    ip = new CubicInterpolation();
                    break;
                case "Linear":
                default:
                    ip = new LinearInterpolation();
                    break;
            }

            switch (config.ResamplingType)
            {
                case "TimeSlot":
                    pipeline.Add(new ResampleTimeBased() {
                        InputFeatures = featurelist,
                        OutputFeatures = featurelist,
                        TimeSlot = config.ResamplingParam,
                        Interpolation = ip
                    });
                    break;
                case "SampleCount":
                    pipeline.Add(new ResampleSamplesCountBased()
                    {
                        InputFeatures = featurelist,
                        OutputFeatures = featurelist,
                        NumOfSamples = (int)config.ResamplingParam,
                        Interpolation = ip
                    });
                    break;
                case "FillPenUp":
                    pipeline.Add(new FillPenUpDurations()
                    { 
                        InputFeatures = featurelist,
                        OutputFeatures = featurelist,
                        Interpolation = ip
                    });
                    break;
                case "None":
                default:
                    break;
            }

            var classifier = new OptimalDtwClassifier();//TODO: csak ez?
            classifier.Features = new List<FeatureDescriptor>();
            switch (config.Features)
            {
                case "X":
                    classifier.Features.Add(Features.X);
                    break;
                case "Y":
                    classifier.Features.Add(Features.Y);
                    break;
                case "P":
                    classifier.Features.Add(Features.Pressure);
                    break;
                case "Azimuth":
                    classifier.Features.Add(Features.Azimuth);
                    break;
                case "Altitude":
                    classifier.Features.Add(Features.Altitude);
                    break;
                case "XY":
                    classifier.Features.Add(Features.X);
                    classifier.Features.Add(Features.Y);
                    break;
                case "XYP":
                    classifier.Features.Add(Features.X);
                    classifier.Features.Add(Features.Y);
                    classifier.Features.Add(Features.Pressure);
                    break;
                case "XYPAzimuthAltitude":
                    classifier.Features.Add(Features.X);
                    classifier.Features.Add(Features.Y);
                    classifier.Features.Add(Features.Pressure);
                    classifier.Features.Add(Features.Azimuth);
                    classifier.Features.Add(Features.Altitude);
                    break;
                default:
                    break;
            }

            b.Verifier = new Model.Verifier()
            {
                Pipeline = pipeline,
                Classifier = classifier
            };

            b.Logger = new SimpleConsoleLogger();//TODO: ezt a preprocessing benchmark adja meg?
            return b;

        }
    }
}
