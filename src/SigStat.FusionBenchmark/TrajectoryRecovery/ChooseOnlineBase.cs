using SigStat.Common;
using SigStat.Common.Pipeline;
using SigStat.FusionBenchmark.GraphExtraction;
using System;
using System.Collections.Generic;
using System.Text;

namespace SigStat.FusionBenchmark.TrajectoryRecovery
{
    class ChooseOnlineBase : PipelineBase, ITransformation
    {
        [Input]
        public string InputID { get; set; }

        [Input]
        public FeatureDescriptor<List<Vertex>> InputBaseTrajectory { get; set; }

        [Input]
        public List<Signer> OnlineSigners { get; set; }

        [Output("BaseTrajectory")]
        public FeatureDescriptor<List<Vertex>> OutputBaseTrajectory { get; set; }

        public void Transform(Signature signature)
        {
            this.LogInformation("ChooseOnlineBase - transform started");
            var onlineSig = OnlineSigners.Find(signer => signer.ID == signature.Signer.ID).Signatures.Find(sig => sig.ID == InputID);
            if (onlineSig.Origin == Origin.Forged)
            {
                throw new Exception();
            }
            signature.SetFeature<List<Vertex>>(OutputBaseTrajectory,
                            new List<Vertex>(onlineSig.GetFeature<List<Vertex>>(InputBaseTrajectory)));
            this.LogInformation("{0} {1} - {2} {3}", signature.Signer.ID, signature.ID,
                                                     onlineSig.Signer.ID, onlineSig.ID);
            this.LogInformation("ChooseOnlineBase - transform finished");

        }

    }
}
