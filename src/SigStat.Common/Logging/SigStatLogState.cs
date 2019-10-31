using System;
using System.Collections.Generic;
using System.Text;

namespace SigStat.Common.Logging
{
    public class SigStatLogState
    {
        //public Guid ProcessId { get; set; } // benchmarkID
        //public string SignerID { get; set; }
        //public string SignatureID { get; set; }
        public string Source { get; set; }
    }

    public class BenchmarkLogState : SigStatLogState
    {
        string Aer { get; set; }
        string Far { get; set; }
        string Frr { get; set; }
    }

    public class BenchmarkKeyValueLogState : SigStatLogState
    {
        string Group { get; set; }
        string Key { get; set; }
        string Value { get; set; }
    }

    public class SignerKeyValueLogState : SigStatLogState
    {
        string SignerID { get; set; }
        string Scope { get; set; }
        string Key { get; set; }
        string Value { get; set; }
    }

    public class SignatureKeyValueLogState : SigStatLogState
    {
        string SignerID { get; set; }
        string SignatureID { get; set; }

        string Scope { get; set; }
        string Key { get; set; }
        string Value { get; set; }
    }

    public class BenchmarkSignerLogState : SigStatLogState
    {
        string Aer { get; set; }
        string Far { get; set; }
        string Frr { get; set; }
        string SignerID { get; set; }
    }

    public class TransformLogState : SigStatLogState
    {
        string Aer { get; set; }
        string Far { get; set; }
        string Frr { get; set; }
        string SignerID { get; set; }
        string SignatureID { get; set; }
    }

    public class ClassifierDistanceLogState : SigStatLogState
    {
        string Signer1ID { get; set; }
        string Signature1ID { get; set; }
        string Signer2ID { get; set; }
        string Signature2ID { get; set; }
        double distance { get; set; }

    }
}

