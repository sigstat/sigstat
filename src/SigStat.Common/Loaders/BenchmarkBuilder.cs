using SigStat.Common.Helpers;
using SigStat.Common.Pipeline;
using SigStat.Common.PipelineItems.Classifiers;
using SigStat.Common.PipelineItems.Transforms.Preprocessing;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace SigStat.Common.Loaders
{
    public static class BenchmarkBuilder
    {

        //Elejen letrehozzuk oket: nem kell mindenkinek kulon instance, mert allapotmentesek vagyunk (ahol igen..)
        //miert static? Mert a Builder is static. Lehetne a Buildben is letrehozni oket, de mi sokszor hivjuk meg ezt a Buildet.
        static SVC2004Sampler1 svcSampler1 = new SVC2004Sampler1();
        static SVC2004Sampler2 svcSampler2 = new SVC2004Sampler2();
        static SVC2004Sampler3 svcSampler3 = new SVC2004Sampler3();
        static SVC2004Sampler4 svcSampler4 = new SVC2004Sampler4();
        static McytSampler1 mcytSampler1 = new McytSampler1();
        static McytSampler2 mcytSampler2 = new McytSampler2();
        static McytSampler3 mcytSampler3 = new McytSampler3();
        static McytSampler4 mcytSampler4 = new McytSampler4();
        static DutchSampler1 dutchSampler1 = new DutchSampler1();
        static DutchSampler2 dutchSampler2 = new DutchSampler2();
        static DutchSampler3 dutchSampler3 = new DutchSampler3();
        static DutchSampler4 dutchSampler4 = new DutchSampler4();

        static Svc2004Loader svcLoader;
        static MCYTLoader mcytLoader;
        static SigComp11DutchLoader dutchLoader;

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

        public static VerifierBenchmark Build(BenchmarkConfig config, string databasePath = null)
        {
            if (databasePath == null)
                databasePath = Environment.GetEnvironmentVariable("SigStatDB");

            svcLoader = new Svc2004Loader(Path.Combine(databasePath, "SVC2004.zip"), true);
            mcytLoader = new MCYTLoader(Path.Combine(databasePath, "MCYT100.zip"), true);
            dutchLoader = new SigComp11DutchLoader(Path.Combine(databasePath, "SigComp11_Dutch.zip"), true);

            //labor:
            //svcLoader = new Svc2004Loader(@"Task2.zip", true);
            //mcytLoader = new MCYTLoader(@"MCYT_Signature_100.zip", true);
            //dutchLoader = new SigComp11DutchLoader(@"dutch_renamed.zip", true);


            Sampler sampler1 = null;
            Sampler sampler2 = null;
            Sampler sampler3 = null;
            Sampler sampler4 = null;


            VerifierBenchmark b = new VerifierBenchmark();
            switch (config.Database)
            {
                case "SVC2004":
                    b.Loader = svcLoader;
                    sampler1 = svcSampler1;
                    sampler2 = svcSampler2;
                    sampler3 = svcSampler3;
                    sampler4 = svcSampler4;
                    break;
                case "MCYT100":
                    b.Loader = mcytLoader;
                    sampler1 = mcytSampler1;
                    sampler2 = mcytSampler2;
                    sampler3 = mcytSampler3;
                    sampler4 = mcytSampler4;
                    break;
                case "DUTCH":
                    b.Loader = dutchLoader;
                    sampler1 = dutchSampler1;
                    sampler2 = dutchSampler2;
                    sampler3 = dutchSampler3;
                    sampler4 = dutchSampler4;
                    break;
                default:
                    throw new NotSupportedException();
            }

            switch (config.Sampling)
            {
                case "S1":
                    b.Sampler = sampler1;
                    break;
                case "S2"://TODO: replace with new samplers
                    b.Sampler = sampler2;
                    break;
                case "S3":
                    b.Sampler = sampler3;
                    break;
                case "S4":
                    b.Sampler = sampler4;
                    break;
                default:
                    break;
            }
            

            var pipeline = new SequentialTransformPipeline();

            //Filter first
            switch (config.ResamplingType_Filter)
            {
                case "P":
                case "P_FillPenUp":
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

            var featurelist = new List<FeatureDescriptor<List<double>>>()
            {
                Features.X, Features.Y, Features.Pressure, Features.Azimuth, Features.Altitude
            };

            //resample after transformations
            switch (config.ResamplingType_Filter)
            {
                case "SampleCount":
                    pipeline.Add(new ResampleSamplesCountBased()
                    {
                        InputFeatures = featurelist,
                        OutputFeatures = featurelist,
                        OriginalTFeature = Features.T,
                        ResampledTFeature = Features.T,
                        NumOfSamples = (int)config.ResamplingParam,
                        InterpolationType = ip
                    });
                    break;
                case "P_FillPenUp":
                case "FillPenUp":
                    pipeline.Add(new FillPenUpDurations()
                    {
                        InputFeatures = featurelist,
                        OutputFeatures = featurelist,
                        TimeInputFeature = Features.T,
                        TimeOutputFeature = Features.T,
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

            Func<double[], double[], double> distance = null;
            switch (config.Distance)
            {
                case "Euclidean":
                    distance = Accord.Math.Distance.Euclidean;
                    break;
                case "Manhattan":
                    distance = Accord.Math.Distance.Manhattan;
                    break;
                default:
                    break;
            }
            IClassifier classifier;
            if (config.Classifier == "Dtw")
            {
                classifier = new DtwClassifier(distance);
                (classifier as DtwClassifier).Features = ClassifierFeatures;
            }
            else if (config.Classifier == "OptimalDtw")
            {
                classifier = new OptimalDtwClassifier(distance)
                {
                    Features = ClassifierFeatures,
                    Sampler = b.Sampler
                };
            }
            else throw new NotSupportedException();

            b.Verifier = new Model.Verifier()
            {
                Pipeline = pipeline,
                Classifier = classifier
            };

            b.Parameters = config.ToKeyValuePairs().ToList();

            return b;

        }
    }
}
