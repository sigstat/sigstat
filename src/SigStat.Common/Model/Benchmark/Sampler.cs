using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace SigStat.Common.Model
{
    /// <summary>
    /// Takes samples from a set of <see cref="Signature"/>s by given sampling strategies.
    /// Use this to fine-tune the <see cref="VerifierBenchmark"/>
    /// </summary>
    public class Sampler
    {
        private readonly Func<List<Signature>, List<Signature>> references;
        private readonly Func<List<Signature>, List<Signature>> genuineTests;
        private readonly Func<List<Signature>, List<Signature>> forgeryTests;
        private List<Signature> signatures;

        /// <summary>
        /// Initialize a new instance of the <see cref="Sampler"/> class based on the <paramref name="s"/> parameter's strategy.
        /// </summary>
        /// <param name="s">A Sampler to copy.</param>
        public Sampler(Sampler s)
        {
            this.references = s.references;
            this.genuineTests = s.genuineTests;
            this.forgeryTests = s.forgeryTests;
        }

        /// <summary>
        /// Initialize a new instance of the <see cref="Sampler"/> class by given sampling strategies.
        /// </summary>
        /// <param name="references">Strategy to sample genuine signatures to be used for training.</param>
        /// <param name="genuineTests">Strategy to sample genuine signatures to be used for testing.</param>
        /// <param name="forgeryTests">Strategy to sample forged signatures to be used for testing.</param>
        public Sampler(Func<List<Signature>, List<Signature>> references, Func<List<Signature>, List<Signature>> genuineTests, Func<List<Signature>, List<Signature>> forgeryTests)
        {
            this.references = references;
            this.genuineTests = genuineTests;
            this.forgeryTests = forgeryTests;
        }

        /// <summary>
        /// Initialize the Sampler with a Signer's Signatures.
        /// </summary>
        /// <param name="s">Signer to sample Signatures from.</param>
        public void Init(Signer s)
        {
            Init(s.Signatures);
        }

        /// <summary>
        /// Initialize the Sampler with a list of Signatures.
        /// </summary>
        /// <param name="s">List of Signatures to take samples from.</param>
        public void Init(List<Signature> s)
        {
            signatures = new List<Signature>(s);//copy
        }

        /// <summary>
        /// Samples a batch of genuine reference signatures to train on.
        /// </summary>
        /// <returns>Genuine reference signatures to train on.</returns>
        public List<Signature> SampleReferences()
        {
            var r =  references(signatures);
            /*foreach (var s in r)
                signatures.Remove(s);*/
            return r;
        }

        /// <summary>
        /// Samples a batch of genuine signatures to test on.
        /// </summary>
        /// <returns>Genuine signatures to test on.</returns>
        public List<Signature> SampleGenuineTests()
        {
            var g = genuineTests(signatures);
            /*foreach (var s in g)
                signatures.Remove(s);*/
            return g;
        }

        /// <summary>
        /// Samples a batch of forged signatures to test on.
        /// </summary>
        /// <returns>Forged signatures to test on.</returns>
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
                // TODO: remove or generalize
                return new Sampler(
                    (sl) => sl.Where(s => s.Origin == Origin.Genuine).Take(10).ToList(),
                    (sl) => sl.Where(s => s.Origin == Origin.Genuine).Skip(10).Take(10).ToList(),
                    (sl) => sl.Where(s => s.Origin == Origin.Forged).Take(10).ToList()
                    );
            }
        }
    }
}
