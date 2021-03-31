using Newtonsoft.Json;
using SigStat.Common.Helpers;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;

namespace SigStat.Common.Loaders
{
    /// <summary>
    /// <see cref="DataSetLoader"/> for the MCYT dataset
    /// </summary>
    [JsonObject(MemberSerialization.OptOut)]
    public class MCYTLoader : DataSetLoader
    {

        /// <summary>
        /// Set of features containing raw data loaded from MCYT-format database.
        /// </summary>
        public static class MCYT
        {

            /// <summary>
            /// X cooridnates from the online signature imported from the MCYT database
            /// </summary>
            public static readonly FeatureDescriptor<List<int>> X = FeatureDescriptor.Get<List<int>>("MCYT.X");
            /// <summary>
            /// Y cooridnates from the online signature imported from the MCYT database
            /// </summary>
            public static readonly FeatureDescriptor<List<int>> Y = FeatureDescriptor.Get<List<int>>("MCYT.Y");
            /// <summary>
            /// Azimuth values from the online signature imported from the MCYT database
            /// </summary>
            public static readonly FeatureDescriptor<List<int>> Azimuth = FeatureDescriptor.Get<List<int>>("MCYT.Azimuth");
            /// <summary>
            /// Altitude values from the online signature imported from the MCYT database
            /// </summary>
            public static readonly FeatureDescriptor<List<int>> Altitude = FeatureDescriptor.Get<List<int>>("MCYT.Altitude");
            /// <summary>
            /// Pressure values from the online signature imported from the MCYT database
            /// </summary>
            public static readonly FeatureDescriptor<List<int>> Pressure = FeatureDescriptor.Get<List<int>>("MCYT.Pressure");
        }

        private struct MCYTSignatureFile : IEquatable<MCYTSignatureFile>
        {
            public string FilePath { get; set; }
            public string SignerID { get; set; }
            public string SignatureID { get; set; }
            public char Origin { get; set; }

            public MCYTSignatureFile(string filepath)
            {
                FilePath = filepath;
                string name = Path.GetFileNameWithoutExtension(filepath.Split('/').Last());//handle if file is in zip directory
                if (name.Contains('f'))
                    Origin = 'f';
                else//if (name.Contains('v'))
                    Origin = 'v';
                var parts = name.Split(Origin);
                SignerID = parts[0];
                SignatureID = name;
            }

            public bool Equals(MCYTSignatureFile other)
            {
                return
                    FilePath == other.FilePath
                    && SignerID == other.SignerID
                    && SignatureID == other.SignatureID
                    && Origin == other.Origin;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MCYTLoader"/> class.
        /// </summary>
        /// <param name="databasePath">The database path.</param>
        /// <param name="standardFeatures">if set to <c>true</c> features will be also stored in <see cref="Features"/>.</param>
        public MCYTLoader(string databasePath, bool standardFeatures)
        {
            DatabasePath = databasePath;
            StandardFeatures = standardFeatures;
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="MCYTLoader"/> class with specified database.
        /// </summary>
        /// <param name="databasePath">Represents the path, to load the signatures from. It supports two basic approaches:
        /// <list type="bullet">
        /// <item>DatabasePath may point to a (non password protected) zip file, containing the siganture files</item>
        /// <item>DatabasePath may point to a directory with all the signer files or with files grouped in subdirectories</item>
        /// </list></param>
        /// <param name="standardFeatures">Convert loaded data to standard <see cref="Features"/>.</param>
        /// <param name="signerFilter">Sets the <see cref="SignerFilter"/> property</param>
        public MCYTLoader(string databasePath, bool standardFeatures, Predicate<Signer> signerFilter)
        {
            DatabasePath = databasePath;
            StandardFeatures = standardFeatures;
            SignerFilter = signerFilter;
        }

        /// <summary>
        /// Set MCYT sampling frequenct to 100hz
        /// </summary>
        public override int SamplingFrequency { get { return 100; } }


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
                var signatureGroups = zip.Entries.Where(f => f.Name.EndsWith(".fpg")).Select(f => new MCYTSignatureFile(f.FullName)).GroupBy(sf => sf.SignerID);
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
                            Origin = signatureFile.Origin == 'f' ? Origin.Forged : /*v*/ Origin.Genuine //TODO: tipp: v valid, f forgery
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
            BinaryReader reader = new BinaryReader(stream);
            var hsize = reader.ReadUInt16(); // Header size
            int ver = 1;
            if ((hsize == 48) || (hsize == 60))
            {
                ver = 2;
            }
            var res = reader.ReadUInt16();
            var nvectores = reader.ReadUInt32(); // number of vectors
            if (ver == 2)
            {
                //No need to store these values as they are not used here
                reader.ReadUInt32();    //Fs
                reader.ReadUInt32();    //mventana
                reader.ReadUInt32();   //msolapadas
            }
            stream.Seek(hsize - 12, SeekOrigin.Begin);

            stream.Seek(hsize, SeekOrigin.Begin); // not actually needed

            if (res != 32)
            {
                //TODO: ezt loggolni
                new NotSupportedException("Only 32 bit floating point values are supported at the moment");
            }

            List<double> X = new List<double>();
            List<double> Y = new List<double>();
            List<double> Pressure = new List<double>();
            List<double> Azimuth = new List<double>();
            List<double> Altitude = new List<double>();
            for (int i = 0; i < nvectores; i++)
            {//read each sample
                X.Add(reader.ReadSingle());
                Y.Add(reader.ReadSingle());
                Pressure.Add(reader.ReadSingle());
                Azimuth.Add(reader.ReadSingle());
                Altitude.Add(reader.ReadSingle());
            }

            //set signature features
            signature.SetFeature(MCYT.X, X);
            signature.SetFeature(MCYT.Y, Y);
            signature.SetFeature(MCYT.Pressure, Pressure);//TODO: readme-bol feltetelezzuk, hogy ez a sorrend
            signature.SetFeature(MCYT.Azimuth, Azimuth);
            signature.SetFeature(MCYT.Altitude, Altitude);
            if (standardFeatures)
            {
                signature.SetFeature(Features.X, X);
                signature.SetFeature(Features.Y, Y);
                signature.SetFeature(Features.Pressure, Pressure);
                signature.SetFeature(Features.Azimuth, Azimuth);
                signature.SetFeature(Features.Altitude, Altitude);
                // Sampling frequency is 100Hz ==> time should be increased by 10 msec for each slot
                signature.SetFeature(Features.T, Enumerable.Range(0, X.Count).Select(i => i * 10d).ToList());
                signature.SetFeature(Features.PenDown, Pressure.Select(p => p > 0).ToList());
                // Upstorkes are represented by zero pressure points
                var pressureValues = signature.GetFeature(Features.Pressure).ToArray();
                signature.SetFeature(Features.PointType, DataCleaningHelper.GeneratePointTypeValuesFromPressure(pressureValues).ToList());
                signature.CalculateStandardStatistics();
            }
        }

    }

}
