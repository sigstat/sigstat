using System;
using System.Collections.Generic;
using System.Linq;
using System.Collections.ObjectModel;
using System.Text;

namespace SigStat.Common
{
    /// <summary>
    /// Represents a person as an <see cref="ID"/> and a list of <see cref="Signatures"/>.
    /// </summary>
    public class Signer//TODO: <TSignature> where TSignature: Signature
    {
        /// <summary>
        /// a dictionary to save the best FRRs for the signer , to use later ofr signer sampling rate dependant calssification
        /// </summary>
       public Dictionary<int, int> signerBestSteps= new Dictionary<int, int>();
        /// <summary>
        /// best sampling frequency for the signer
        /// </summary>
        public int bestSampleRate = 50;
        /// <summary>
        /// best step (nmber of skipped points) for the signer
        /// </summary>
        public int bestStep = 1;
        /// <summary>
        /// best frr for the signer, used to find the best sampling frequency and step for each signer
        /// </summary>
        public double bestFrr = 1;
       
        /// <summary>
        /// An identifier for the Signer. Keep it unique to be useful for logs.
        /// </summary>
        public string ID { get; set; }
        /// <summary>
        /// List of signatures that belong to the signer. 
        /// (Their origin is not constrained to be genuine.)
        /// </summary>
        public virtual List<Signature> Signatures { get; set; } = new List<Signature>();     
        
        /// <summary>
        /// Returns a string representation of a Signer
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            if (Signatures == null)
                return ID;

            int genuine = 0;
            int forged = 0;
            int unknown = 0;

            for (int i = 0; i < Signatures.Count; i++)
            {
                switch (Signatures[i].Origin)
                {
                    case Origin.Unknown:
                        unknown++;
                        break;
                    case Origin.Genuine:
                        genuine++;
                        break;
                    case Origin.Forged:
                        forged++;
                        break;
                    default:
                        throw new NotSupportedException();
                }
            }

            if (unknown == 0)
                return $"{ID} (G:{genuine} F:{forged})";
            else
                return $"{ID} (G:{genuine} F:{forged} U:{unknown})";
        }
 
      
    }
}
