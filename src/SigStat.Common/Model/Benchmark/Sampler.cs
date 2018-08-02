using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace SigStat.Common.Model
{
    public class Sampler
    {
        private readonly Func<List<Signature>, List<Signature>> references;
        private readonly Func<List<Signature>, List<Signature>> genuineTests;
        private readonly Func<List<Signature>, List<Signature>> forgeryTests;
        private List<Signature> signatures;

        public Sampler(Func<List<Signature>, List<Signature>> references, Func<List<Signature>, List<Signature>> genuineTests, Func<List<Signature>, List<Signature>> forgeryTests)
        {
            this.references = references;
            this.genuineTests = genuineTests;
            this.forgeryTests = forgeryTests;
        }

        public void Init(Signer s)
        {
            Init(s.Signatures);
        }

        public void Init(List<Signature> s)
        {
            signatures = new List<Signature>(s);//copy
        }

        public List<Signature> SampleReferences()
        {
            var r =  references(signatures);
            /*foreach (var s in r)
                signatures.Remove(s);*/
            return r;
        }

        public List<Signature> SampleGenuineTests()
        {
            var g = genuineTests(signatures);
            /*foreach (var s in g)
                signatures.Remove(s);*/
            return g;
        }

        public List<Signature> SampleForgeryTests()
        {
            var f = forgeryTests(signatures);
            /*foreach (var s in f)
                signatures.Remove(s);*/
            return f;
        }

        /// <summary>
        /// Default sampler for SVC2004 database.
        /// 10 references, 10 genuine tests, 10 forged tests
        /// </summary>
        public static Sampler BasicSampler
        {
            get
            {
                return new Sampler(
                    (sl) => sl.Where(s => s.Origin == Origin.Genuine).Take(10).ToList(),
                    (sl) => sl.Where(s => s.Origin == Origin.Genuine).Skip(10).Take(10).ToList(),
                    (sl) => sl.Where(s => s.Origin == Origin.Forged).Take(10).ToList()
                    );
            }
        }

    }
}
