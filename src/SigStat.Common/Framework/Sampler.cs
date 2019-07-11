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

    public class SVC2004Sampler1 : Sampler
    {
        public SVC2004Sampler1() : base(
            sl => sl.Where(s => s.Origin == Origin.Genuine).Take(10).ToList(),
            sl => sl.Where(s => s.Origin == Origin.Genuine).Skip(10).Take(10).ToList(),
            sl => sl.Where(s => s.Origin == Origin.Forged).ToList())
        { }
    }

    public class SVC2004Sampler2 : Sampler
    {
        public SVC2004Sampler2() : base(
            sl => sl.Where(s => s.Origin == Origin.Genuine).Skip(10).Take(10).ToList(),
            sl => sl.Where(s => s.Origin == Origin.Genuine).Take(10).ToList(),
            sl => sl.Where(s => s.Origin == Origin.Forged).ToList())
        { }
    }

    public class SVC2004Sampler3 : Sampler
    {
        public SVC2004Sampler3() : base(
            sl => sl.Where(s => s.Origin == Origin.Genuine).Where(s => int.Parse(s.ID) % 2 == 0).ToList(),
            sl => sl.Where(s => s.Origin == Origin.Genuine).Where(s => int.Parse(s.ID) % 2 == 1).ToList(),
            sl => sl.Where(s => s.Origin == Origin.Forged).ToList())
        { }
    }

    public class SVC2004Sampler4 : Sampler
    {
        public SVC2004Sampler4() : base(
            sl => sl.Where(s => s.Origin == Origin.Genuine).Where(s => int.Parse(s.ID) % 2 == 1).ToList(),
            sl => sl.Where(s => s.Origin == Origin.Genuine).Where(s => int.Parse(s.ID) % 2 == 0).ToList(),
            sl => sl.Where(s => s.Origin == Origin.Forged).ToList())
        { }
    }


    public class McytSampler1 : Sampler
    {
        public McytSampler1() : base(
            sl => sl.Where(s => s.Origin == Origin.Genuine).Take(10).ToList(),
            sl => sl.Where(s => s.Origin == Origin.Genuine).Skip(10).Take(15).ToList(),
            sl => sl.Where(s => s.Origin == Origin.Forged).ToList())
        { }
    }

    public class McytSampler2 : Sampler
    {
        public McytSampler2() : base(
            sl => sl.Where(s => s.Origin == Origin.Genuine).Skip(15).Take(10).ToList(),
            sl => sl.Where(s => s.Origin == Origin.Genuine).Take(15).ToList(),
            sl => sl.Where(s => s.Origin == Origin.Forged).ToList())
        { }
    }

    public class McytSampler3 : Sampler
    {
        static string[] training = new[] { "00", "02", "04", "06", "08", "10", "12", "14", "16", "18" };
        public McytSampler3() : base(
            sl => sl.Where(s => s.Origin == Origin.Genuine).Where(s => training.Contains(s.ID.Substring(s.ID.Length - 2))).ToList(),
            sl => sl.Where(s => s.Origin == Origin.Genuine).Where(s => !training.Contains(s.ID.Substring(s.ID.Length - 2))).ToList(),
            sl => sl.Where(s => s.Origin == Origin.Forged).ToList())
        { }
    }

    public class McytSampler4 : Sampler
    {
        static string[] training = new[] { "01", "03", "05", "07", "09", "11", "13", "15", "17", "19" };
        public McytSampler4() : base(
            sl => sl.Where(s => s.Origin == Origin.Genuine).Where(s => training.Contains(s.ID.Substring(s.ID.Length - 2))).ToList(),
            sl => sl.Where(s => s.Origin == Origin.Genuine).Where(s => !training.Contains(s.ID.Substring(s.ID.Length - 2))).ToList(),
            sl => sl.Where(s => s.Origin == Origin.Forged).ToList())
        { }
    }

    public class DutchSampler1 : Sampler
    {
        public DutchSampler1() : base(
            sl => sl.Where(s => s.Origin == Origin.Genuine).Take(10).ToList(),
            sl => sl.Where(s => s.Origin == Origin.Genuine).Skip(10).ToList(),
            sl => sl.Where(s => s.Origin == Origin.Forged).ToList())
        { }
    }

    public class DutchSampler2 : Sampler
    {
        public DutchSampler2() : base(
            sl => sl.Where(s => s.Origin == Origin.Genuine).Skip(14).Take(10).ToList(),
            sl => sl.Where(s => s.Origin == Origin.Genuine).Take(14).ToList(),
            sl => sl.Where(s => s.Origin == Origin.Forged).ToList())
        { }
    }

    public class DutchSampler3 : Sampler
    {
        static string[] training = new[] { "02", "04", "06", "08", "10", "12", "14", "16", "18", "20" };
        public DutchSampler3() : base(
            sl => sl.Where(s => s.Origin == Origin.Genuine).Where(s => training.Contains(s.ID.Substring(s.ID.Length - 2))).ToList(),
            sl => sl.Where(s => s.Origin == Origin.Genuine).Where(s => !training.Contains(s.ID.Substring(s.ID.Length - 2))).ToList(),
            sl => sl.Where(s => s.Origin == Origin.Forged).ToList())
        { }
    }

    public class DutchSampler4 : Sampler
    {
        static string[] training = new[] { "01", "03", "05", "07", "09", "11", "13", "15", "17", "19" };
        public DutchSampler4() : base(
            sl => sl.Where(s => s.Origin == Origin.Genuine).Where(s => training.Contains(s.ID.Substring(s.ID.Length - 2))).ToList(),
            sl => sl.Where(s => s.Origin == Origin.Genuine).Where(s => !training.Contains(s.ID.Substring(s.ID.Length - 2))).ToList(),
            sl => sl.Where(s => s.Origin == Origin.Forged).ToList())
        { }
    }

    public class UniversalSampler : Sampler
    {
        // TODO: this can use wrong configurations 
        public UniversalSampler(int refCount, int testCount) : base(
            sl => sl.Where(s => s.Origin == Origin.Genuine).Take(refCount).ToList(),
            sl => sl.Where(s => s.Origin == Origin.Genuine).Skip(refCount).Take(testCount).ToList(),
            sl => sl.Where(s => s.Origin == Origin.Forged).Take(testCount).ToList())
        { }
    }

    public class SigComp19Sampler : Sampler
    {
        public SigComp19Sampler(int refCount, int testCount) : base(
            sl => sl.Where(s => s.Origin == Origin.Genuine).Take(refCount).ToList(),
            sl => sl.Where(s => s.Origin == Origin.Genuine).Skip(refCount).Take(testCount).ToList(),
            sl => sl.Where(s => s.Origin == Origin.Forged).ToList())
        { }
    }

    public class GermanSampler : Sampler
    {
        // TODO: this can use wrong configurations 
        public GermanSampler() : base(
            sl => sl.Where(s => s.Origin == Origin.Genuine && s.ID.Contains("_R_")).ToList(),//10
            sl => sl.Where(s => s.Origin == Origin.Genuine && s.ID.Contains("_G")).ToList(),//5
            sl => sl.Where(s => s.Origin == Origin.Forged).ToList())
        { }
    }

    public class JapaneseSampler1 : Sampler
    {
        public JapaneseSampler1() : base(
            sl => sl.Where(s => s.Origin == Origin.Genuine).Take(10).ToList(),
            sl => sl.Where(s => s.Origin == Origin.Genuine).Skip(10).Take(32).ToList(),
            sl => sl.Where(s => s.Origin == Origin.Forged).ToList())
        { }
    }

    public class JapaneseSampler2 : Sampler
    {
        public JapaneseSampler2() : base(
            sl => sl.Where(s => s.Origin == Origin.Genuine).Skip(32).Take(10).ToList(),
            sl => sl.Where(s => s.Origin == Origin.Genuine).Take(32).ToList(),
            sl => sl.Where(s => s.Origin == Origin.Forged).ToList())
        { }
    }

   
}
