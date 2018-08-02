using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using SigStat.Common.Loaders;
using System.Linq;

namespace SigStat.Common.Model
{
    public struct Result
    {
        public readonly string Signer;
        public readonly double Frr;
        public readonly double Far;
        public readonly double Aer;

        public Result(string signer, double frr, double far, double aer)
        {
            Signer = signer;
            Frr = frr;
            Far = far;
            Aer = aer;
        }
    }

    public struct BenchmarkResults
    {
        public readonly List<Result> SignerResults;
        public readonly Result FinalResult;

        public BenchmarkResults(List<Result> signerResults, Result finalResult)
        {
            SignerResults = signerResults;
            FinalResult = finalResult;
        }
    }

    public class VerifierBenchmark
    {
        public IDataSetLoader Loader;
        public Verifier Verifier;
        public Sampler Sampler;
        public Action<string> Log;
        public Action<int> Progress;

        public VerifierBenchmark()
        {
            //ha nem allit be kulon semmit, akkor ez legyen a default
            Verifier = Verifier.BasicVerifier;
            Sampler = Sampler.BasicSampler;
        }

        public async Task<int> ExecuteAsync()
        {
            throw new NotImplementedException();
        }

        public BenchmarkResults Execute()
        {
            var results = new List<Result>();
            double far_acc = 0;
            double frr_acc = 0;

            Log("Loading data..");
            var signers = new List<Signer>(Loader.EnumerateSigners(null));
            Log(signers.Count + " signers found. Benchmarking..");
            for(int i = 0; i < signers.Count; i++)
            {
                Sampler.Init(signers[i]);
                List<Signature> references = Sampler.SampleReferences();
                List<Signature> genuineTests = Sampler.SampleGenuineTests();
                List<Signature> forgeryTests = Sampler.SampleForgeryTests();
                //catch: nem volt eleg alairas a benchmarkhoz

                Verifier.Train(references);

                //FRR: false rejection rate
                //FRR = elutasított eredeti / összes eredeti
                int false_reject_cnt = 0;
                foreach (Signature genuine in genuineTests)
                    if (!Verifier.Test(genuine))
                        false_reject_cnt++;//eredeti alairast hamisnak hisz
                double FRR = false_reject_cnt / (double)genuineTests.Count;

                //FAR: false acceptance rate
                //FAR = elfogadott hamis / összes hamis
                int false_accept_cnt = 0;
                foreach (Signature forgery in forgeryTests)
                    if (Verifier.Test(forgery))
                        false_accept_cnt++;//hamis alairast eredetinek hisz
                double FAR = false_accept_cnt / (double)forgeryTests.Count;

                //AER: average error rate
                double AER = (FRR + FAR) / 2.0;

                //EER definicio fix: ez az az ertek amikor FAR==FRR

                frr_acc += FRR;
                far_acc += FAR;
                results.Add(new Result(signers[i].ID, FRR, FAR, AER));

                Progress( (int)(i / (double)(signers.Count-1) * 100.0) );
            }

            double frr_final = frr_acc / signers.Count;
            double far_final = far_acc / signers.Count;
            double aer_final = (frr_final + far_final) / 2.0;

            return new BenchmarkResults(results, new Result(null, frr_final, far_final, aer_final));
        }


    }
}
