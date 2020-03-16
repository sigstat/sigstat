using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;

namespace SigStat.Common.Loaders
{
    /// <summary>
    /// <see cref="DataSetLoader"/> for the SigComp13Japanese dataset
    /// </summary>
    public class SigComp13JapaneseLoader : DataSetLoader
    {
        /// <summary>
        /// Sampling Frequency of this database
        /// </summary>
        public override int SamplingFrequency { get { return 200; } }
        /// <summary>
        /// Set of features containing raw data loaded from SigComp13Japanese-format database.
        /// </summary>
        public static class SigComp13Japanese
        {
            
            /// <summary>
            /// X cooridnates from the online signature imported from the SigComp13Japanese database  
            /// </summary>
            public static readonly FeatureDescriptor<List<int>> X = FeatureDescriptor.Get<List<int>>("SigComp13Japanese.X");
            /// <summary>
            /// Y cooridnates from the online signature imported from the SigComp13Japanese database
            /// </summary>
            public static readonly FeatureDescriptor<List<int>> Y = FeatureDescriptor.Get<List<int>>("SigComp13Japanese.Y");
            /// <summary>
            /// Z cooridnates from the online signature imported from the SigComp13Japanese database (100 - pen down, 0 - pen up)
            /// </summary>
            public static readonly FeatureDescriptor<List<int>> P = FeatureDescriptor.Get<List<int>>("SigComp13Japanese.P");
            /// <summary>
            /// Generated T values from the online signature imported from the SigComp13Japanese database
            /// </summary>
            public static readonly FeatureDescriptor<List<int>> T = FeatureDescriptor.Get<List<int>>("SigComp13Japanese.T");
        }

        private struct SigComp13JapaneseSignatureFile
        {
            public string FilePath { get; set; }
            public string SignerID { get; set; }
            public string SignatureIndex { get; set; }
            public string ForgerID { get; set; }
            public string SignatureID { get; set; }


            public SigComp13JapaneseSignatureFile(string filepath) : this()
            {
                FilePath = filepath;
                SignatureID = Path.GetFileNameWithoutExtension(filepath.Split('/').Last());//handle if file is in zip directory
                var parts = SignatureID.Split('_');
                if (parts.Length == 2)
                {
                    SignerID = parts[1];
                    SignatureIndex = parts[0];
                    ForgerID = null;
                }
                else if (parts.Length == 3)
                {
                    SignerID = parts[2];
                    SignatureIndex = parts[0];
                    ForgerID = parts[1];
                }
                else
                    throw new NotSupportedException($"Unsupported filename format '{SignatureID}'");
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SigComp13JapaneseLoader"/> class.
        /// </summary>
        /// <param name="databasePath">The database path.</param>
        /// <param name="standardFeatures">if set to <c>true</c> features will be also stored in <see cref="Features"/>.</param>

        public SigComp13JapaneseLoader(string databasePath, bool standardFeatures)
        {
            DatabasePath = databasePath;
            StandardFeatures = standardFeatures;
        }

        /// <summary>
        /// Gets or sets the database path.
        /// </summary>
        public string DatabasePath { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether features are also loaded as <see cref="Features"/>
        /// </summary>
        public bool StandardFeatures { get; set; }

        /// <inheritdoc />
        public override IEnumerable<Signer> EnumerateSigners(Predicate<Signer> signerFilter)
        {
            //TODO: EnumerateSigners should ba able to operate with a directory path, not just a zip file
            //signerFilter = signerFilter ?? SignerFilter;

            this.LogInformation("Enumerating signers started.");
            using (ZipArchive zip = ZipFile.OpenRead(DatabasePath))
            {
                //cut names if the files are in directories
                var signatureGroups = zip.Entries.Where(f => f.Name.EndsWith(".HWR")).Select(f => new SigComp13JapaneseSignatureFile(f.FullName)).GroupBy(sf => sf.SignerID);
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
        /// <param name="stream">Stream to read SigComp13Japanese data from.</param>
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

            signature.SetFeature(SigComp13Japanese.X, lines.Select(l => l[0]).ToList());
            signature.SetFeature(SigComp13Japanese.Y, lines.Select(l => l[1]).ToList());
            signature.SetFeature(SigComp13Japanese.P, lines.Select(l => l[2]).ToList());
            // Sampling frequency is 200Hz ==> time should be increased by 5 msec for each slot
            signature.SetFeature(SigComp13Japanese.T, Enumerable.Range(0, lines.Count).Select(i => i * 5).ToList());

            if (standardFeatures)
            {
                signature.SetFeature(Features.X, lines.Select(l => (double)l[0]).ToList());
                signature.SetFeature(Features.Y, lines.Select(l => (double)l[1]).ToList());
                signature.SetFeature(Features.Pressure, lines.Select(l => (double)l[2]).ToList());
                signature.SetFeature(Features.T, Enumerable.Range(0, lines.Count).Select(i => i * 5d).ToList());
                signature.SetFeature(Features.PenDown, lines.Select(l => l[2] > 0).ToList());
                signature.SetFeature(Features.Azimuth, lines.Select(l => 1d).ToList());
                signature.SetFeature(Features.Altitude, lines.Select(l => 1d).ToList());
                signature.CalculateStandardStatistics();

            }
           
        }
    }
}
