using System;
using System.Collections.Generic;
using System.Text;

namespace SigStat.Common.Test
{
    static class TestHelper
    {
        public static Signature BuildSignature(string signatureId = "Demo", Origin origin = Origin.Genuine, string signerID = "S01")
        {
            Signature signature = new Signature();
            signature.ID = signatureId;
            signature.Origin = origin;
            signature.Signer = new Signer()
            {
                ID = signerID
            };
            return signature;
            //Signature.SetFeature(Features.X, new List<double> { 1, 2, 3 });
        }
    }
}
