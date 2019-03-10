using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace SigStat.Common
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
        /// Samples a batch of genuine reference signatures to train on.
        /// </summary>
        /// <returns>Genuine reference signatures to train on.</returns>
        public List<Signature> SampleReferences(List<Signature> signatures)
        {
            return references(signatures);
        }

        /// <summary>
        /// Samples a batch of genuine test signatures to test on.
        /// </summary>
        /// <returns>Genuine signatures to test on.</returns>
        public List<Signature> SampleGenuineTests(List<Signature> signatures)
        {
            return genuineTests(signatures);
        }

        /// <summary>
        /// Samples a batch of forged signatures to test on.
        /// </summary>
        /// <returns>Forged signatures to test on.</returns>
        public List<Signature> SampleForgeryTests(List<Signature> signatures)
        {
            return forgeryTests(signatures);
        }
    }

    public class SVC2004Sampler : Sampler
    {
        public SVC2004Sampler() : base(
            sl => sl.Where(s => s.Origin == Origin.Genuine).Take(10).ToList(),
            sl => sl.Where(s => s.Origin == Origin.Genuine).Skip(10).Take(10).ToList(),
            sl => sl.Where(s => s.Origin == Origin.Forged).Take(10).ToList())
        { }
    }

}
