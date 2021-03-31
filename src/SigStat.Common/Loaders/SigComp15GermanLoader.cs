using SigStat.Common.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;

namespace SigStat.Common.Loaders
{
    /// <summary>
    /// <see cref="DataSetLoader"/> for the SigComp15German dataset
    /// </summary>
    public class SigComp15GermanLoader : DataSetLoader
    {
        /// <summary>
        /// Sampling Frequency of this database
        /// </summary>
        public override int SamplingFrequency { get { return 75; } }
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



        private struct SigComp15GermanSignatureFile : IEquatable<SigComp15GermanSignatureFile>
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
                //F: forged, G: genuine (testing), R: references (training)
                var parts = SignatureID.Split('_');
                if (parts[1][0] == 'F')
                {
                    SignerID = parts[0];
                    SignatureIndex = parts[1];
                    ForgerID = parts[1].Skip(1).ToString();
                }
                else if (parts[1][0] == 'G' || parts[1][0] == 'R')
                {
                    SignerID = parts[0];
                    SignatureIndex = parts[2];
                    ForgerID = null;
                }

                else
                    throw new NotSupportedException($"Unsupported filename format '{SignatureID}'");
            }

            public bool Equals(SigComp15GermanSignatureFile other)
            {
                return
                    FilePath == other.FilePath
                    && SignerID == other.SignerID
                    && SignatureIndex == other.SignatureIndex
                    && ForgerID == other.ForgerID
                    && SignatureID == other.SignatureID;
            }
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="SigComp15GermanLoader"/> class.
        /// </summary>
        /// <param name="databasePath">The database path.</param>
        /// <param name="standardFeatures">if set to <c>true</c> features will be also stored in <see cref="Features"/>.</param>
        public SigComp15GermanLoader(string databasePath, bool standardFeatures)
        {
            DatabasePath = databasePath;
            StandardFeatures = standardFeatures;
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="SigComp15GermanLoader"/> class with specified database.
        /// </summary>
        /// <param name="databasePath">Represents the path, to load the signatures from. It supports two basic approaches:
        /// <list type="bullet">
        /// <item>DatabasePath may point to a (non password protected) zip file, containing the siganture files</item>
        /// <item>DatabasePath may point to a directory with all the signer files or with files grouped in subdirectories</item>
        /// </list></param>
        /// <param name="standardFeatures">Convert loaded data  to standard <see cref="Features"/>.</param>
        /// <param name="signerFilter">Sets the <see cref="SignerFilter"/> property</param>
        public SigComp15GermanLoader(string databasePath, bool standardFeatures, Predicate<Signer> signerFilter)
        {
            DatabasePath = databasePath;
            StandardFeatures = standardFeatures;
            SignerFilter = signerFilter;
        }
        /// <summary>
        /// Gets or sets the database path.
        /// </summary>
        public string DatabasePath { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether features are also loaded as <see cref="Features"/>
        /// </summary>
        public bool StandardFeatures { get; set; }
        /// <summary>
        /// Ignores any signers during the loading, that do not match the predicate
        /// </summary>
        public Predicate<Signer> SignerFilter { get; set; }

        /// <inheritdoc />
        public override IEnumerable<Signer> EnumerateSigners(Predicate<Signer> signerFilter)
        {
            signerFilter = signerFilter ?? SignerFilter;

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
            signature.SetFeature(SigComp15.T, Enumerable.Range(0, lines.Count).Select(k => k * (1.0 / 75.0) * 1000).ToList());

            // The database uses special datapoints to signal stroke boundaries
            // We are going to remove them in standard features for smoother comparison, and to save this information by PointTypes
            var stopIndexes = new List<int>();
            var startIndexes = new List<int>();

            int i = 1;
            while (i < lines.Count - 1)
            {
                if (lines[i][0] != -1)
                {
                    i++;
                    continue;
                }
                if (lines[i][0] == -1 && lines[i + 1][0] == -1)
                {
                    // Remove duplicate stroke boundaries
                    lines.RemoveAt(i);
                    continue;
                }
                // Save the index of the points before and after the gap and remove the special datapoint
                startIndexes.Add(i);
                stopIndexes.Add(i - 1);
                lines.RemoveAt(i);
            }

            if (standardFeatures)
            {
                signature.SetFeature(Features.X, lines.Select(l => (double)l[0]).ToList());
                signature.SetFeature(Features.Y, lines.Select(l => (double)l[1]).ToList());
                signature.SetFeature(Features.Pressure, lines.Select(l => (double)l[2]).ToList());
                signature.SetFeature(Features.PenDown, lines.Select(l => true).ToList());

                // The database uses special datapoints to signal stroke boundaries which were removed above
                // Stroke start point indexes are in startIndexes, stroke end point indexes are in stopIndexes
                var pointType = new double[lines.Count];
                for (i = 0; i < lines.Count; i++)
                {
                    if (i == 0 || startIndexes.Contains(i)) pointType[i] = 1; // stroke start
                    else if (i == lines.Count - 1 || stopIndexes.Contains(i)) pointType[i] = 2; // stroke end
                    else pointType[i] = 0; // Internal stroke point
                }
                signature.SetFeature(Features.PointType, pointType.ToList());

                // Sampling frequency is 75Hz ==> time should be increased by 13.333 msec for each slot
                var unitTimeSlot = (1.0 / 75.0) * 1000;
                DataCleaningHelper.InitializeTimestamps(signature, unitTimeSlot);

                var x = signature.GetFeature(Features.X);
                signature.SetFeature(Features.Azimuth, x.Select(v => 1d).ToList());
                signature.SetFeature(Features.Altitude, x.Select(v => 1d).ToList());

                signature.CalculateStandardStatistics();
            }

        }
    }
}
