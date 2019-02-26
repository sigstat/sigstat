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
        private List<Signature> signatures;
        public int BatchSize { get; set; }

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
        public Sampler(Func<List<Signature>, List<Signature>> references, Func<List<Signature>, List<Signature>> genuineTests, Func<List<Signature>, List<Signature>> forgeryTests, int batchSize=10)
        {
            this.references = references;
            this.genuineTests = genuineTests;
            this.forgeryTests = forgeryTests;
            BatchSize = batchSize;
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
        /// Initialize the Sampler with a Signer's Signatures.
        /// </summary>
        /// <param name="s">Signer to sample Signatures from.</param>
        public void Init(List<Signer> s)
        {
            Init(s.SelectMany(q=>q.Signatures).ToList());
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
        public List<Signature> SampleReferences(Func<List<Signature>, List<Signature>> prefilter)
        {
            var r = references(prefilter(signatures)).Take(BatchSize).ToList();
            //signatures = signatures.Except(r).ToList();//TODO hasznalt mintakat ne hasznaljuk ujra
            return r;
        }

        /// <summary>
        /// Samples a batch of genuine test signatures to test on.
        /// </summary>
        /// <returns>Genuine signatures to test on.</returns>
        public List<Signature> SampleGenuineTests(Func<List<Signature>, List<Signature>> prefilter)
        {
            var g = genuineTests(prefilter(signatures)).Skip(BatchSize).Take(BatchSize).ToList();
            //signatures = signatures.Except(g).ToList();//TODO hasznalt mintakat ne hasznaljuk ujra
            return g;
        }

        /// <summary>
        /// Samples a batch of forged signatures to test on.
        /// </summary>
        /// <returns>Forged signatures to test on.</returns>
        public List<Signature> SampleForgeryTests(Func<List<Signature>, List<Signature>> prefilter)
        {
            var f = forgeryTests(prefilter(signatures)).Take(BatchSize).ToList();
            //signatures=signatures.Except(f).ToList();//TODO hasznalt mintakat ne hasznaljuk ujra
            return f;
        }
    }

    public class SVC2004Sampler : Sampler
    {
        public SVC2004Sampler() : base(
            sl => sl.Where(s => s.Origin == Origin.Genuine).ToList(),
            sl => sl.Where(s => s.Origin == Origin.Genuine).ToList(),
            sl => sl.Where(s => s.Origin == Origin.Forged).ToList(),
            10)
        { }
    }

}
