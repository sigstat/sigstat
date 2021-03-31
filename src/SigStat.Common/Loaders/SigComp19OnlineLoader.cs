﻿using SigStat.Common.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;

namespace SigStat.Common.Loaders
{
    /// <summary>
    /// <see cref="DataSetLoader"/> for the SigComp19 dataset
    /// </summary>
    public class SigComp19OnlineLoader : DataSetLoader
    {
        /// <summary>
        /// sampling frequency for this database
        /// </summary>
        public override int SamplingFrequency { get { return 0; } }
        /// <summary>
        /// Set of features containing raw data loaded from SigComp19-format database.
        /// </summary>
        public static class SigComp19
        {
            /// <summary>
            /// X cooridnates from the online signature imported from the SigComp19 database
            /// </summary>
            public static readonly FeatureDescriptor<List<int>> X = FeatureDescriptor.Get<List<int>>("SigComp19.X");
            /// <summary>
            /// Y cooridnates from the online signature imported from the SigComp19 database
            /// </summary>
            public static readonly FeatureDescriptor<List<int>> Y = FeatureDescriptor.Get<List<int>>("SigComp19.Y");
            /// <summary>
            /// Pressure from the online signature imported from the SigComp19 database
            /// </summary>
            public static readonly FeatureDescriptor<List<int>> P = FeatureDescriptor.Get<List<int>>("SigComp19.P");
            /// <summary>
            /// Altitude from the online signature imported from the SigComp19 database
            /// </summary>
            public static readonly FeatureDescriptor<List<int>> Altitude = FeatureDescriptor.Get<List<int>>("SigComp19.Altitude");
            /// <summary>
            /// Azimuth from the online signature imported from the SigComp19 database
            /// </summary>
            public static readonly FeatureDescriptor<List<int>> Azimuth = FeatureDescriptor.Get<List<int>>("SigComp19.Azimuth");
            /// <summary>
            /// Distance from the surface of the tablet from the online signature imported from the SigComp19 database
            /// </summary>
            public static readonly FeatureDescriptor<List<int>> Distance = FeatureDescriptor.Get<List<int>>("SigComp19.Distance");
            /// <summary>
            /// T values from the online signature imported from the SigComp19 database
            /// </summary>
            public static readonly FeatureDescriptor<List<int>> T = FeatureDescriptor.Get<List<int>>("SigComp19.T");
            /// <summary>
            /// EventType (pen up) values from the online signature imported from the SigComp19 database
            /// </summary>
            public static readonly FeatureDescriptor<List<int>> EventType = FeatureDescriptor.Get<List<int>>("SigComp19.EventType");

        }

        private struct SigComp19OnlineSignatureFile : IEquatable<SigComp19OnlineSignatureFile>
        {
            public string FilePath { get; set; }
            public string SignerID { get; set; }
            public string SignatureIndex { get; set; }
            public string ForgerID { get; set; }
            public string SignatureID { get; set; }


            public SigComp19OnlineSignatureFile(string filepath)
            {
                FilePath = filepath;
                SignatureID = Path.GetFileNameWithoutExtension(filepath.Split('/').Last());//handle if file is in zip directory

                var parts = SignatureID.Split('_')[1].Split('-');
                if (parts.Length == 2)
                {
                    SignerID = parts[0];
                    SignatureIndex = parts[1];
                    ForgerID = null;
                }
                else
                    throw new NotSupportedException($"Unsupported filename format '{SignatureID}'");
            }

            public bool Equals(SigComp19OnlineSignatureFile other)
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
        /// Initializes a new instance of the <see cref="SigComp19OnlineLoader"/> class.
        /// </summary>
        /// <param name="databasePath">The database path.</param>
        /// <param name="standardFeatures">if set to <c>true</c> features will be also stored in <see cref="Features"/>.</param>
        public SigComp19OnlineLoader(string databasePath, bool standardFeatures)
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
        /// <summary>
        /// Ignores any signers during the loading, that do not match the predicate
        /// </summary>
        public Predicate<Signer> SignerFilter { get; set; }
        /// <inheritdoc />
        public override IEnumerable<Signer> EnumerateSigners(Predicate<Signer> signerFilter)
        {
            //TODO: EnumerateSigners should ba able to operate with a directory path, not just a zip file
            signerFilter = signerFilter ?? SignerFilter;

            //TODO: EnumerateSigners should ba able to operate with a directory path, not just a zip file
            

            this.LogInformation("Enumerating signers started.");
            using (ZipArchive zip = ZipFile.OpenRead(DatabasePath))
            {
                //cut names if the files are in directories
                var signatureGroups = zip.Entries.Where(f => f.Name.EndsWith(".csv")).Select(f => new SigComp19OnlineSignatureFile(f.FullName)).GroupBy(sf => sf.SignerID);
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
                            ID = signatureFile.SignatureIndex,
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
        /// <param name="stream">Stream to read SigComp19 data from.</param>
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
            var lines = linesArray
                .Skip(1)
                .Where(l => !string.IsNullOrWhiteSpace(l))
                .Select(l => l.Split(';').Select(s => int.Parse(s)).ToArray())
                .ToList();

            // Remove noise (points with 0 pressure) from the beginning of the signature
            while (lines.Count > 0 && lines[0][4] == 0)
            {
                lines.RemoveAt(0);
            }
            // Remove noise (points with 0 pressure) from the end of the signature
            while (lines.Count > 0 && lines[lines.Count - 1][4] == 0)
            {
                lines.RemoveAt(lines.Count - 1);
            }

            signature.SetFeature(SigComp19.EventType, lines.Select(l => l[0]).ToList());
            signature.SetFeature(SigComp19.T, lines.Select(l => l[1]).ToList());
            signature.SetFeature(SigComp19.X, lines.Select(l => l[2]).ToList());
            signature.SetFeature(SigComp19.Y, lines.Select(l => l[3]).ToList());
            signature.SetFeature(SigComp19.P, lines.Select(l => l[4]).ToList());
            signature.SetFeature(SigComp19.Altitude, lines.Select(l => l[5]).ToList());
            signature.SetFeature(SigComp19.Azimuth, lines.Select(l => l[6]).ToList());
            signature.SetFeature(SigComp19.Distance, lines.Select(l => l[7]).ToList());

            if (standardFeatures)
            {
                signature.SetFeature(Features.X, lines.Select(l => (double)l[2]).ToList());
                signature.SetFeature(Features.Y, lines.Select(l => (double)l[3]).ToList());
                signature.SetFeature(Features.Pressure, lines.Select(l => (double)l[4]).ToList());
                signature.SetFeature(Features.T, lines.Select(l => (double)l[1]).ToList());
                signature.SetFeature(Features.PenDown, lines.Select(l => l[0] == 1).ToList()); // 1 -pen down, 3 - pen up
                signature.SetFeature(Features.Azimuth, lines.Select(l => (double)l[6]).ToList());
                signature.SetFeature(Features.Altitude, lines.Select(l => (double)l[5]).ToList());
                // Upstorkes are represented by zero pressure points
                var pressureValues = signature.GetFeature(Features.Pressure).ToArray();
                signature.SetFeature(Features.PointType, DataCleaningHelper.GeneratePointTypeValuesFromPressure(pressureValues).ToList());
                SignatureHelper.CalculateStandardStatistics(signature);

            }
        }
    }
}
