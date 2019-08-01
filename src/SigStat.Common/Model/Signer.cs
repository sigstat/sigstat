using System;
using System.Collections.Generic;
using System.Linq;
using System.Collections.ObjectModel;
using System.Text;

namespace SigStat.Common
{
    /// <summary>
    /// Represents a person as a <see cref="ID"/> and a list of <see cref="Signatures"/>.
    /// </summary>
    public class Signer//TODO: <TSignature> where TSignature: Signature
    {
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
