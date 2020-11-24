using System;

using System.Collections.Generic;

using System.Text;



namespace SigStat.Common.Logging

{
    /// <summary>
    /// Base state used in report information logging.
    /// </summary>
    public class SigStatLogState

    {
        /// <summary>
        /// Object from which the state originates.
        /// </summary>
        public string Source { get; set; }


    }


    /// <summary>
    /// Specific state used for signer information transiting
    /// </summary>
    public class SignerLogState : SigStatLogState

    {
        /// <summary>
        /// Id of the signer
        /// </summary>
        public string SignerID { get; set; }
    }


    /// <summary>
    /// Specific state used for signature information transiting
    /// </summary>
    public class SignatureLogState : SigStatLogState

    {
        /// <summary>
        /// Id of the owning signer
        /// </summary>

        public string SignerID { get; set; }

        /// <summary>
        /// Id of the signature
        /// </summary>
        public string SignatureID { get; set; }

    }


    /// <summary>
    /// Specific state used for Benchmark result transiting
    /// </summary>
    public class BenchmarkResultsLogState : SigStatLogState
    {
        /// <summary>
        /// Average error rate
        /// </summary>
        public double Aer { get; set; }

        /// <summary>
        /// False accaptance rate
        /// </summary>
        public double Far { get; set; }

        /// <summary>
        /// False rejection rate
        /// </summary>
        public double Frr { get; set; }

        /// <summary>
        /// Creates a BenchmarkResultsLogState
        /// </summary>
        /// <param name="aer"> Aer</param>
        /// <param name="far"> Far</param>
        /// <param name="frr"> Frr</param>
        public BenchmarkResultsLogState(double aer, double far, double frr)
        {
            Aer = aer;
            Far = far;
            Frr = frr;
        }

        ///<inheritdoc/>
        public override string ToString()
        {
            return $"BenchmarkResults: AER: {Aer} (FAR: {Far}, FRR: {Frr})";
        }

    }

    /// <summary>
    /// Specific state used for Signer result transiting
    /// </summary>
    public class SignerResultsLogState : SignerLogState
    {
        /// <summary>
        /// Average error rate
        /// </summary>
        public double Aer { get; set; }

        /// <summary>
        /// False accaptance rate
        /// </summary>
        public double Far { get; set; }

        /// <summary>
        /// False rejection rate
        /// </summary>
        public double Frr { get; set; }

        /// <summary>
        /// Creates a SignerResultsLogState
        /// </summary>
        /// /// <param name="signerId"> Id of the signer</param>
        /// <param name="aer"> Aer</param>
        /// <param name="far"> Far</param>
        /// <param name="frr"> Frr</param>
        public SignerResultsLogState(string signerId, double aer, double far, double frr)
        {
            SignerID = signerId;
            Aer = aer;
            Far = far;
            Frr = frr;
        }

        ///<inheritdoc/>
        public override string ToString()
        {
            return $"SignerResults for {SignerID}: AER: {Aer} (FAR: {Far}, FRR: {Frr})";
        }
    }


    /// <summary>
    /// Specific state used for Benchmarks key-value information transiting
    /// </summary>
    public class BenchmarkKeyValueLogState : SigStatLogState

    {
        /// <summary>
        /// Group of the key-value pair
        /// </summary>
        public string Group { get; set; }

        /// <summary>
        /// Key
        /// </summary>
        public string Key { get; set; }

        /// <summary>
        /// Value
        /// </summary>
        public object Value { get; set; }

        /// <summary>
        /// Creates a BenchmarkKeyValueLogState
        /// </summary>
        /// <param name="group">Group</param>
        /// <param name="key">Key</param>
        /// <param name="value">Value</param>
        public BenchmarkKeyValueLogState(string group, string key, object value)
        {
            Group = group;
            Key = key;
            Value = value;
        }

        ///<inheritdoc/>
        public override string ToString()
        {
            return $"{Group}.{Key}={Value}";
        }
    }


    /// <summary>
    /// Specific state for signature distance information transiting
    /// </summary>
    public class ClassifierDistanceLogState : SigStatLogState

    {
        /// <summary>
        /// Id of the first signature's signer
        /// </summary>
        public string Signer1Id { get; set; }

        /// <summary>
        /// /// Id of the second signature's signer
        /// </summary>
        public string Signer2Id { get; set; }

        /// <summary>
        /// Id of the first signature
        /// </summary>
        public string Signature1Id { get; set; }

        /// <summary>
        /// Id of the second signature
        /// </summary>
        public string Signature2Id { get; set; }

        /// <summary>
        /// Distance values between the signatures
        /// </summary>
        public double Distance { get; set; }

        /// <summary>
        /// Creates a ClassifierDistanceLogState
        /// </summary>
        /// <param name="signer1Id">Id of the first signature's signer</param>
        /// <param name="signer2Id">Id of the second signature's signer</param>
        /// <param name="signature1Id">Id of the first signature</param>
        /// <param name="signature2Id">Id of the second signature</param>
        /// <param name="distance">Distance values between the signatures</param>
        public ClassifierDistanceLogState(string signer1Id, string signer2Id, string signature1Id, string signature2Id, double distance)
        {
            Signer1Id = signer1Id;
            Signer2Id = signer2Id;
            Signature1Id = signature1Id;
            Signature2Id = signature2Id;
            this.Distance = distance;
        }

        ///<inheritdoc/>
        public override string ToString()
        {
            return $"Distance of {Signer1Id}.{Signature1Id} and {Signer2Id}.{Signature2Id} is {Distance}";
        }
    }

}