using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using SigStat.Common;
using SigStat.FusionBenchmark.VisualHelpers;

namespace SigStat.FusionBenchmark.FusionDemos
{
    public static class Strokepairingmatrix
    {

        public static void Calculate(string[] ids)
        {
            var offlineLoader = FusionPipelines.GetOfflineLoader();
            var onlineLoader = FusionPipelines.GetOnlineLoader();

            var offlineSigners = offlineLoader.EnumerateSigners(signer => ids.Contains(signer.ID)).ToList();
            var onlineSigners = onlineLoader.EnumerateSigners(signer => ids.Contains(signer.ID)).ToList();


            var offlinePipeline = FusionPipelines.GetOfflinePipeline();

            foreach (var offSigner in offlineSigners)
            {
                Console.WriteLine(offSigner.ID + " started at " + DateTime.Now.ToString("h:mm:ss tt"));
                var onSigner = onlineSigners.Find(signer => signer.ID == offSigner.ID);
                Parallel.ForEach(offSigner.Signatures, offSig =>
                {
                    Console.WriteLine("Preprocess - " + offSig.Signer.ID + "_" + offSig.ID + "started at " + DateTime.Now.ToString("h:mm:ss tt"));
                    offlinePipeline.Transform(offSig);
                    var onSig = onSigner.Signatures.Find(sig => sig.ID == offSig.ID);
                    var onToOnPipeline = FusionPipelines.GetHackedOnToOnPipeline(offSig.GetFeature(FusionFeatures.Bounds));
                    onToOnPipeline.Transform(onSig);
                }
                );

                int numOfRef = 10;
                int numOfGen = 20;
                var references = onSigner.Signatures.FindAll(sig => sig.Origin == Origin.Genuine).Take(numOfRef).ToList();
                var genuines = offSigner.Signatures.FindAll(sig => sig.Origin == Origin.Genuine).Take(numOfGen).ToList();

                double[,] results = new double[references.Count, genuines.Count];

                for (int i = 0; i < references.Count; i++)
                {
                    var fusionPipeline = FusionPipelines.GetFusionPipeline(onlineSigners, false, references[i].ID);
                    Parallel.For(0, genuines.Count, j=>
                    {
                        Console.WriteLine("ref: {0}, sig: {1}", i, j);
                        fusionPipeline.Transform(genuines[j]);
                    }
                    );
                    var pairingDists = new StrokePairingDistances
                    {
                        InputOfflineTrajectory = FusionFeatures.Trajectory,
                        InputOnlineTrajectory = FusionFeatures.Trajectory,
                        OnlineSignatures = onSigner.Signatures,
                        OfflineSignatures = genuines
                    };
                    var newDists = pairingDists.Calculate();
                    if (newDists.Count != results.GetLength(1))
                    {
                        throw new Exception();
                    }
                    for (int j = 0; j < newDists.Count; j++)
                    {
                        results[i, j] = newDists[j].Item3;
                        Console.WriteLine(results[i, j]);
                    }
                }
                TxtHelper.Save(TxtHelper.ArrayToLines(results), "pairingmatrix" + offSigner.ID);
            }

        }
    }
}
