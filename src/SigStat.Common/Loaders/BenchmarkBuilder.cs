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
        static Svc2004Loader svcLoader = new Svc2004Loader(@"Task2.zip", true);
        static MCYTLoader mcytLoader = new MCYTLoader(@"MCYT_Signature_100.zip", true);
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

            //Filter first
            switch (config.ResamplingType_Filter)
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

            switch (config.Translation_Scaling.Translation)
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

            switch (config.Translation_Scaling.Scaling)
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

            Type ip;
            switch (config.Interpolation)
            {
                case "Cubic":
                    ip = typeof(CubicInterpolation);
                    break;
                case "Linear":
                default:
                    ip = typeof(LinearInterpolation);
                    break;
            }

            //resample after transformations
            switch (config.ResamplingType_Filter)
            {
                case "SampleCount":
                    pipeline.Add(new ResampleSamplesCountBased()
                    {
                        InputFeatures = featurelist,
                        OutputFeatures = featurelist,
                        NumOfSamples = (int)config.ResamplingParam,
                        InterpolationType = ip
                    });
                    break;
                case "FillPenUp":
                    pipeline.Add(new FillPenUpDurations()
                    { 
                        InputFeatures = featurelist,
                        OutputFeatures = featurelist,
                        InterpolationType = ip
                    });
                    break;
                case "None":
                default:
                    break;
            }

            var ClassifierFeatures = new List<FeatureDescriptor>();
            switch (config.Features)
            {
                case "X":
                    ClassifierFeatures.Add(Features.X);
                    break;
                case "Y":
                    ClassifierFeatures.Add(Features.Y);
                    break;
                case "P":
                    ClassifierFeatures.Add(Features.Pressure);
                    break;
                case "Azimuth":
                    ClassifierFeatures.Add(Features.Azimuth);
                    break;
                case "Altitude":
                    ClassifierFeatures.Add(Features.Altitude);
                    break;
                case "XY":
                    ClassifierFeatures.Add(Features.X);
                    ClassifierFeatures.Add(Features.Y);
                    break;
                case "XYP":
                    ClassifierFeatures.Add(Features.X);
                    ClassifierFeatures.Add(Features.Y);
                    ClassifierFeatures.Add(Features.Pressure);
                    break;
                case "XYPAzimuthAltitude":
                    ClassifierFeatures.Add(Features.X);
                    ClassifierFeatures.Add(Features.Y);
                    ClassifierFeatures.Add(Features.Pressure);
                    ClassifierFeatures.Add(Features.Azimuth);
                    ClassifierFeatures.Add(Features.Altitude);
                    break;
                default:
                    break;
            }

            IClassifier classifier;
            if (config.Classifier == "Dtw")
            {
                classifier = new DtwClassifier(Accord.Math.Distance.Euclidean);
                (classifier as DtwClassifier).Features = ClassifierFeatures;
            }
            else//if (config.Classifier == "OptimalDtw")
            {
                classifier = new OptimalDtwClassifier()
                {
                    Features = ClassifierFeatures,
                    Sampler = b.Sampler

                };
            }

            b.Verifier = new Model.Verifier()
            {
                Pipeline = pipeline,
                Classifier = classifier
            };

            return b;

        }
    }
}
