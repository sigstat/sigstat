using System;
using System.Collections.Generic;
using System.Text;

namespace SigStat.Common.Logging
{
    /// <summary>
    /// Informations of a signer
    /// </summary>
    public class SignerResults

    {
        /// <summary>
        /// The ID of the signer
        /// </summary>
        public string SignerID { get; set; }

        /// <summary>
        /// False Acceptance Rate of the signer
        /// </summary>
        public object Far;

        /// <summary>
        /// False Rejection Rate of the signer
        /// </summary>
        public object Frr;

        /// <summary>
        /// Average Error Rate of the signer
        /// </summary>
        public object Aer;

        /// <summary>
        /// Distacne matrix of the signers signatures
        /// </summary>
        public DistanceMatrix<string, string, double> DistanceMatrix { get; set; } = new DistanceMatrix<string, string, double>();

        /// <summary>
        /// Creates a signer result with emty result values
        /// </summary>
        /// <param name="signerId">The id of the signer</param>
        public SignerResults(string signerId)
        {
            this.SignerID = signerId;
            DistanceMatrix = new DistanceMatrix<string, string, double>();
        }
    }
}
