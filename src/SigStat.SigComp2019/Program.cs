using SigStat.Common;
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
using System.Threading.Tasks;

namespace SigStat.SigComp2019
{
    class Program
    {
        static string outputFileName;
        static Signature questionedSignature = new Signature();
        static List<Signature> referenceSignatures = new List<Signature>();
        static List<string> fileNames = new List<string>();

        static void Main(string[] args)
        {
            if (!ReadParameters(args)) return;

            var verifier = new Verifier()
            {
                Pipeline = new SequentialTransformPipeline {
                        new Scale() {InputFeature = Features.X, OutputFeature= Features.X },
                        new Scale() {InputFeature = Features.Y, OutputFeature= Features.Y },

                        new FilterPoints() { KeyFeatureInput = Features.Pressure, KeyFeatureOutput = Features.Pressure,
                        InputFeatures = new List<FeatureDescriptor<List<double>>>() { Features.X, Features.Y },
                        OutputFeatures = new List<FeatureDescriptor<List<double>>>() { Features.X, Features.Y }},
                    },

                Classifier = new DtwClassifier()
                {
                    Features = new List<FeatureDescriptor>() { Features.X, Features.Y, Features.Pressure },
                    MultiplicationFactor = 1.7
                }
            };

            verifier.Train(referenceSignatures);
            var comparsionScore = verifier.Test(questionedSignature);
            var evidentialValue = comparsionScore == 1 ? comparsionScore / 0.001 : comparsionScore / (1 - comparsionScore); 

            string results = "";
            for (int i = 0; i < fileNames.Count; i++)
            {
                results += (fileNames.ElementAt(i) + " ");
            }
            results = results + comparsionScore + " " + evidentialValue;

            using (StreamWriter sr = new StreamWriter(new FileStream(outputFileName, FileMode.Append)))
            {
                sr.WriteLine(results);
            }

        }

        private static bool ReadParameters(string[] args)
        {
            if(args == null || args.Length < 5)
            {
                Console.WriteLine(ResourceHelper.ReadString("Help"));
                return false;
            }

            outputFileName = args[0];

            using (MemoryStream ms = new MemoryStream())
            {
                var sigFileName = args[1];
                fileNames.Add(sigFileName);
                using (Stream s = new FileStream(sigFileName, FileMode.Open))
                {
                    s.CopyTo(ms);//must use memory stream to use Seek()
                }
                ms.Position = 0;//needed after CopyTo
                SigComp19OnlineLoader.LoadSignature(questionedSignature, ms, true);
            }

            for (int i = 2; i < args.Length; i++)
            {
                Signature sig = new Signature()
                {
                    Origin = Origin.Genuine,
                    ID = (i - 1).ToString()
                };
                var sigFileName = args[i];
                fileNames.Add(sigFileName);

                using (MemoryStream ms = new MemoryStream())
                {
                    using (Stream s = new FileStream(sigFileName, FileMode.Open))
                    {
                        s.CopyTo(ms);//must use memory stream to use Seek()
                    }
                    ms.Position = 0;//needed after CopyTo
                    SigComp19OnlineLoader.LoadSignature(sig, ms, true);
                }
                
                referenceSignatures.Add(sig);
            }

            return true;
        }
    }
}
