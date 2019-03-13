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

        //Elejen letrehozzuk oket: nem kell mindenkinek kulon instance, mert allapotmentesek vagyunk (ahol igen..)
        //miert static? Mert a Builder is static. Lehetne a Buildben is letrehozni oket, de mi sokszor hivjuk meg ezt a Buildet.
        static SVC2004Sampler svcSampler = new SVC2004Sampler();
        //TODO: more signer samplers
        static Svc2004Loader svcLoader = new Svc2004Loader(@"Databases\Online\SVC2004\Task2.zip", true);
        static MCYTLoader mcytLoader = new MCYTLoader(@"Databases\Online\MCYT100\MCYT_Signature_100.zip", true);
        static List<FeatureDescriptor<List<double>>> toFilter = new List<FeatureDescriptor<List<double>>>()
        {
            Features.X, Features.Y, Features.Azimuth, Features.Altitude
        };
        static FilterPoints filterPoints = new FilterPoints()
        {
            InputFeatures = toFilter,
            OutputFeatures = toFilter,
            KeyFeatureInput = Features.Pressure,
            KeyFeatureOutput = Features.Pressure
        };
        static NormalizeRotation normalizeRotation = new NormalizeRotation() { InputX = Features.X, InputY = Features.Y, InputT = Features.T, OutputX = Features.X, OutputY = Features.Y };
        static TranslatePreproc cxTranslate = new TranslatePreproc(OriginType.CenterOfGravity) { InputFeature = Features.X, OutputFeature = Features.X };
        static TranslatePreproc cyTranslate = new TranslatePreproc(OriginType.CenterOfGravity) { InputFeature = Features.Y, OutputFeature = Features.Y };
        static TranslatePreproc blxTranslate = new TranslatePreproc(OriginType.Minimum) { InputFeature = Features.X, OutputFeature = Features.X };
        static TranslatePreproc blyTranslate = new TranslatePreproc(OriginType.Minimum) { InputFeature = Features.Y, OutputFeature = Features.Y };
        static Scale xScale = new Scale() { InputFeature = Features.X, OutputFeature = Features.X };
        static Scale yScale = new Scale() { InputFeature = Features.Y, OutputFeature = Features.Y };
        static UniformScale xyUniformScale = new UniformScale() { BaseDimension = Features.X, BaseDimensionOutput = Features.X, ProportionalDimension = Features.Y, ProportionalDimensionOutput = Features.Y };
        static UniformScale yxUniformScale = new UniformScale() { BaseDimension = Features.Y, BaseDimensionOutput = Features.Y, ProportionalDimension = Features.X, ProportionalDimensionOutput = Features.X };
        //static LinearInterpolation linearInterpolation = new LinearInterpolation();
        //static CubicInterpolation cubicInterpolation = new CubicInterpolation(); 
        //TODO: legyen allapotmentes a maradek is?



        public static VerifierBenchmark Build(BenchmarkConfig config)
        {



            VerifierBenchmark b = new VerifierBenchmark();
            switch (config.Sampling)
            {
                case "S1":
                    b.Sampler = svcSampler;
                    break;
                case "S2"://TODO: replace with new samplers
                    b.Sampler = svcSampler;
                    break;
                case "S3":
                    b.Sampler = svcSampler;
                    break;
                default:
                    break;
            }
            switch (config.Database)
            {
                case "SVC2004":
                    b.Loader = svcLoader;
                    break;
                case "MCYT100":
                    b.Loader = mcytLoader;
                    break;
                case "..."://TODO: add 3rd db
                    b.Loader = null;
                    break;
                default:
                    break;
            }

            var pipeline = new SequentialTransformPipeline();

            switch (config.Filter)
            {
                case "P":
                    pipeline.Add(filterPoints);
                    break;
                case "None":
                default:
                    break;
            }

            if (config.Rotation)
                pipeline.Add(normalizeRotation);

            switch (config.TranslationScaling.Translation)
            {
                case "CogToOriginX":
                    pipeline.Add(cxTranslate);
                    break;
                case "CogToOriginY":
                    pipeline.Add(cyTranslate);
                    break;
                case "CogToOriginXY":
                    pipeline.Add(cxTranslate);
                    pipeline.Add(cyTranslate);
                    break;
                case "BottomLeftToOrigin":
                    pipeline.Add(blxTranslate);
                    pipeline.Add(blyTranslate);
                    break;
                case "None":
                default:
                    break;
            }

            switch (config.TranslationScaling.Scaling)
            {
                case "X01":
                    pipeline.Add(xScale);
                    break;
                case "Y01":
                    pipeline.Add(yScale);
                    break;
                case "X01Y01":
                    pipeline.Add(xScale);
                    pipeline.Add(yScale);
                    break;
                case "X01Y0prop":
                    pipeline.Add(xyUniformScale);
                    break;
                case "Y01X0prop":
                    pipeline.Add(yxUniformScale);
                    break;
                case "None":
                default:
                    break;
            }

            var featurelist = new List<FeatureDescriptor<List<double>>>()
            {
                Features.X, Features.Y, Features.Pressure, Features.Azimuth, Features.Altitude
            };

            IInterpolation ip;
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
