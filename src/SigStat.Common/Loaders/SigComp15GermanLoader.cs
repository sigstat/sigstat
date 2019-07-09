using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;

namespace SigStat.Common.Loaders
{
    public class SigComp15GermanLoader : DataSetLoader
    {
        /// <summary>
        /// Set of features containing raw data loaded from SigComp15German-format database.
        /// </summary>
        public static class SigComp15
        {
            /// <summary>
            /// X cooridnates from the online signature imported from the SigComp15German database
            /// </summary>
            public static readonly FeatureDescriptor<List<int>> X = FeatureDescriptor.Get<List<int>>("SigComp15.X");
            /// <summary>
            /// Y cooridnates from the online signature imported from the SigComp15German database
            /// </summary>
            public static readonly FeatureDescriptor<List<int>> Y = FeatureDescriptor.Get<List<int>>("SigComp15.Y");
            /// <summary>
            /// Z cooridnates from the online signature imported from the SigComp15German database
            /// </summary>
            public static readonly FeatureDescriptor<List<int>> P = FeatureDescriptor.Get<List<int>>("SigComp15.p");
            /// <summary>
            /// T values from the online signature imported from the SigComp15German database
            /// </summary>
            public static readonly FeatureDescriptor<List<int>> T = FeatureDescriptor.Get<List<int>>("SigComp15.T");
        }

        private struct SigComp15GermanSignatureFile
        {
            public string FilePath { get; set; }
            public string SignerID { get; set; }
            public string SignatureIndex { get; set; }
            public string ForgerID { get; set; }
            public string SignatureID { get; set; }


            public SigComp15GermanSignatureFile(string filepath)
            {
                // TODO: Support original filename format
                FilePath = filepath;
                SignatureID = Path.GetFileNameWithoutExtension(filepath.Split('/').Last());//handle if file is in zip directory
                //F: forged, G: original (testing), R: references (training)
                var parts = SignatureID.Split('_');
                if (parts[1][0] == 'F')
                {
                    SignerID = parts[0];
                    SignatureIndex = parts[1];
                    ForgerID = parts[1].Skip(1).ToString();
                }
                else if (parts[1][0] == 'G')
                {
                    SignerID = parts[0];
                    SignatureIndex = parts[2];
                    ForgerID = null;
                }
                else if (parts[1][0] == 'R')
                {
                    SignerID = parts[0];
                    SignatureIndex = parts[2];
                    ForgerID = null;
                }
                else
                    throw new NotSupportedException($"Unsupported filename format '{SignatureID}'");
            }
        }

        public SigComp15GermanLoader(string databasePath, bool standardFeatures)
        {
            DatabasePath = databasePath;
            StandardFeatures = standardFeatures;
        }

        public string DatabasePath { get; set;  }
        public bool StandardFeatures { get; set; }

        public override IEnumerable<Signer> EnumerateSigners(Predicate<Signer> signerFilter)
        {
            //TODO: EnumerateSigners should ba able to operate with a directory path, not just a zip file
            //signerFilter = signerFilter ?? SignerFilter;

            this.LogInformation("Enumerating signers started.");
            using (ZipArchive zip = ZipFile.OpenRead(DatabasePath))
            {
                //cut names if the files are in directories
                var signatureGroups = zip.Entries.Where(f => f.Name.EndsWith(".trj")).Select(f => new SigComp15GermanSignatureFile(f.FullName)).GroupBy(sf => sf.SignerID);
                this.LogTrace(signatureGroups.Count().ToString() + " signers found in database");
                foreach (var group in signatureGroups)
                {
                    Signer signer = new Signer { ID = group.Key };

                    if (signerFilter != null && !signerFilter(signer))
                    {
                        continue;
                    }
                    foreach (var signatureFile in group)
                    {
                        Signature signature = new Signature
                        {
                            Signer = signer,
                            ID = signatureFile.SignatureID,
                            Origin = signatureFile.ForgerID == null ? Origin.Genuine : Origin.Forged
                        };
                        using (MemoryStream ms = new MemoryStream())
                        {
                            using (Stream s = zip.GetEntry(signatureFile.FilePath).Open())
                            {
                                s.CopyTo(ms);//must use memory stream to use Seek()
                            }
                            ms.Position = 0;//needed after CopyTo
                            LoadSignature(signature, ms, StandardFeatures);
                        }
                        signer.Signatures.Add(signature);

                    }
                    signer.Signatures = signer.Signatures.OrderBy(s => s.ID).ToList();
                    yield return signer;
                }
            }
            this.LogInformation("Enumerating signers finished.");
        }

        /// <summary>
        /// Loads one signature from specified stream.
        /// </summary>
        /// <remarks>
        /// Based on Mohammad's MCYT reader.
        /// </remarks>
        /// <param name="signature">Signature to write features to.</param>
        /// <param name="stream">Stream to read MCYT data from.</param>
        /// <param name="standardFeatures">Convert loaded data to standard <see cref="Features"/>.</param>
        public static void LoadSignature(Signature signature, MemoryStream stream, bool standardFeatures)
        {
            using (StreamReader sr = new StreamReader(stream))
            {
                ParseSignature(signature, sr.ReadToEnd().Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None), standardFeatures);
            }

        }
        private static void ParseSignature(Signature signature, string[] linesArray, bool standardFeatures)
        {
            linesArray = linesArray.Skip(1).ToArray();
            var lines = linesArray
                .Where(l => !string.IsNullOrWhiteSpace(l))
                .Select(l => l.Split(' ').Select(s => int.Parse(s)).ToArray())
                .ToList();

            // Remove noise (points with 0 pressure) from the beginning of the signature
            while (lines.Count > 0 && lines[0][2] == 0)
            {
                lines.RemoveAt(0);
            }
            // Remove noise (points with 0 pressure) from the end of the signature
            while (lines.Count > 0 && lines[lines.Count - 1][2] == 0)
            {
                lines.RemoveAt(lines.Count - 1);
            }

            signature.SetFeature(SigComp15.X, lines.Select(l => l[0]).ToList());
            signature.SetFeature(SigComp15.Y, lines.Select(l => l[1]).ToList());
            signature.SetFeature(SigComp15.P, lines.Select(l => l[2]).ToList());
            // Sampling frequency is 75Hz ==> time should be increased by 13.333 msec for each slot
            signature.SetFeature(SigComp15.T, Enumerable.Range(0, lines.Count).Select(i => i * (1.0/75.0)*1000).ToList());

            if (standardFeatures)
            {
                signature.SetFeature(Features.X, lines.Select(l => (double)l[0]).ToList());
                signature.SetFeature(Features.Y, lines.Select(l => (double)l[1]).ToList());
                signature.SetFeature(Features.Pressure, lines.Select(l => (double)l[2]).ToList());
                signature.SetFeature(Features.T, Enumerable.Range(0, lines.Count).Select(i => i * 5d).ToList());
                signature.SetFeature(Features.Button, lines.Select(l => l[2] > 0).ToList());
                signature.SetFeature(Features.Azimuth, lines.Select(l => 1d).ToList());
                signature.SetFeature(Features.Altitude, lines.Select(l => 1d).ToList());


            }
        }

    }
}
