using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Linq;
using SigStat.Common.Helpers;
using System.IO.Compression;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Reflection;

namespace SigStat.Common.Loaders
{
    /// <summary>
    /// Set of features containing raw data loaded from SVC2004-format database.
    /// </summary>
    public static class Svc2004
    {

        /// <summary>
        /// X cooridnates from the online signature imported from the SVC2004 database
        /// </summary>
        public static readonly FeatureDescriptor<List<int>> X = FeatureDescriptor.Get<List<int>>("Svc2004.X");
        /// <summary>
        /// Y cooridnates from the online signature imported from the SVC2004 database
        /// </summary>
        public static readonly FeatureDescriptor<List<int>> Y = FeatureDescriptor.Get<List<int>>("Svc2004.Y");
        /// <summary>
        /// T values from the online signature imported from the SVC2004 database
        /// </summary>
        public static readonly FeatureDescriptor<List<int>> T = FeatureDescriptor.Get<List<int>>("Svc2004.T");
        /// <summary>
        /// Button values from the online signature imported from the SVC2004 database
        /// </summary>
        public static readonly FeatureDescriptor<List<int>> Button = FeatureDescriptor.Get<List<int>>("Svc2004.Button");
        /// <summary>
        /// Azimuth values from the online signature imported from the SVC2004 database
        /// </summary>
        public static readonly FeatureDescriptor<List<int>> Azimuth = FeatureDescriptor.Get<List<int>>("Svc2004.Azimuth");
        /// <summary>
        /// Altitude values from the online signature imported from the SVC2004 database
        /// </summary>
        public static readonly FeatureDescriptor<List<int>> Altitude = FeatureDescriptor.Get<List<int>>("Svc2004.Altitude");
        /// <summary>
        /// Pressure values from the online signature imported from the SVC2004 database
        /// </summary>
        public static readonly FeatureDescriptor<List<int>> Pressure = FeatureDescriptor.Get<List<int>>("Svc2004.Pressure");


        /// <summary>
        /// A list of all Svc2004 feature descriptors
        /// </summary>
        public static readonly FeatureDescriptor[] All =
            typeof(Svc2004)
            .GetFields(BindingFlags.Static | BindingFlags.Public)
            .Where(f => f.FieldType.IsGenericType && f.FieldType.GetGenericTypeDefinition() == typeof(FeatureDescriptor<>))
            .Select(f => (FeatureDescriptor)f.GetValue(null))
            .ToArray();

    }

    /// <summary>
    /// Loads SVC2004-format database from .zip
    /// </summary>
    [JsonObject(MemberSerialization.OptOut)]
    public class Svc2004Loader : DataSetLoader
    {
        /// <summary>
        /// Sampling Frequency of the SVC database
        /// </summary>
        public override int SamplingFrequency { get { return 100; } }

        private struct SignatureFile
        {
            public string File { get; set; }
            public string SignerID { get; set; }
            public string SignatureID { get; set; }

            public SignatureFile(string file)
            {
                File = file;
                string name = file.Split('/').Last();//handle if file is in zip directory
                var parts = Path.GetFileNameWithoutExtension(name).Replace("U", "").Split('S');
                if (parts.Length != 2)
                {
                    throw new InvalidOperationException("Invalid file format. All signature files should be in 'U__S__.txt' format");
                }
                SignerID = parts[0].PadLeft(2, '0');
                SignatureID = parts[1].PadLeft(2, '0');
            }
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



        /// <summary>
        /// Initializes a new instance of the <see cref="Svc2004Loader"/> class with specified database.
        /// </summary>
        /// <param name="databasePath">Represents the path, to load the signatures from. It supports two basic approaches:
        /// <list type="bullet">
        /// <item>DatabasePath may point to a (non password protected) zip file, containing the siganture files</item>
        /// <item>DatabasePath may point to a directory with all the signer files or with files grouped in subdirectories</item>
        /// </list></param>
        /// <param name="standardFeatures">Convert loaded data (<see cref="Svc2004"/>) to standard <see cref="Features"/>.</param>
        [JsonConstructor]
        public Svc2004Loader(string databasePath, bool standardFeatures)
        {
            DatabasePath = databasePath;
            StandardFeatures = standardFeatures;
            SignerFilter = null;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Svc2004Loader"/> class with specified database.
        /// </summary>
        /// <param name="databasePath">Represents the path, to load the signatures from. It supports two basic approaches:
        /// <list type="bullet">
        /// <item>DatabasePath may point to a (non password protected) zip file, containing the siganture files</item>
        /// <item>DatabasePath may point to a directory with all the signer files or with files grouped in subdirectories</item>
        /// </list></param>
        /// <param name="standardFeatures">Convert loaded data (<see cref="Svc2004"/>) to standard <see cref="Features"/>.</param>
        /// <param name="signerFilter">Sets the <see cref="SignerFilter"/> property</param>
        public Svc2004Loader(string databasePath, bool standardFeatures, Predicate<Signer> signerFilter = null)
        {
            DatabasePath = databasePath;
            StandardFeatures = standardFeatures;
            SignerFilter = signerFilter;
        }

        /// <inheritdoc/>
        public override IEnumerable<Signer> EnumerateSigners(Predicate<Signer> signerFilter)
        {

            //TODO: EnumerateSigners should ba able to operate with a directory path, not just a zip file
            signerFilter = signerFilter ?? SignerFilter;

            this.LogInformation("Enumerating signers started.");
            using (ZipArchive zip = ZipFile.OpenRead(DatabasePath))
            {
                //cut names if the files are in directories
                var signatureGroups = zip.Entries.Where(f => f.Name.EndsWith(".TXT")).Select(f => new SignatureFile(f.FullName)).GroupBy(sf => sf.SignerID);
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
                            ID = signatureFile.SignatureID
                        };
                        using (Stream s = zip.GetEntry(signatureFile.File).Open())
                        {
                            LoadSignature(signature, s, StandardFeatures);
                        }
                        signature.Origin = int.Parse(signature.ID) < 21 ? Origin.Genuine : Origin.Forged;
                        signer.Signatures.Add(signature);


                    }
                    signer.Signatures = signer.Signatures.OrderBy(s => s.ID).ToList();

                    yield return signer;
                }
            }
            this.LogInformation("Enumerating signers finished.");
        }

        /// <summary>
        /// Loads one signature from specified file path.
        /// </summary>
        /// <param name="signature">Signature to write features to.</param>
        /// <param name="path">Path to a file of format "U*S*.txt"</param>
        /// <param name="standardFeatures">Convert loaded data to standard <see cref="Features"/>.</param>
        public void LoadSignature(Signature signature, string path, bool standardFeatures)
        {
            ParseSignature(signature, File.ReadAllLines(path), standardFeatures);
        }

        /// <summary>
        /// Loads one signature from specified stream.
        /// </summary>
        /// <param name="signature">Signature to write features to.</param>
        /// <param name="stream">Stream to read svc2004 data from.</param>
        /// <param name="standardFeatures">Convert loaded data to standard <see cref="Features"/>.</param>
        public static void LoadSignature(Signature signature, Stream stream, bool standardFeatures)
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
                .Where(l => l != "")
                .Select(l => l.Split(' ').Select(s => int.Parse(s)).ToArray())
                .ToList();

            //HACK: same timestamp for measurements does not make sense
            // therefore, we remove the second entry
            // a better solution would be to change the timestamps based on their environments
            for (int i = 0; i < lines.Count - 1; i++)
            {
                if (lines[i][2] == lines[i + 1][2])
                {
                    lines.RemoveAt(i + 1);
                    i--;
                }
            }
            // Task1, Task2
            signature.SetFeature(Svc2004.X, lines.Select(l => l[0]).ToList());
            signature.SetFeature(Svc2004.Y, lines.Select(l => l[1]).ToList());
            signature.SetFeature(Svc2004.T, lines.Select(l => l[2]).ToList());
            signature.SetFeature(Svc2004.Button, lines.Select(l => l[3]).ToList());
            if (standardFeatures)
            {
                signature.SetFeature(Features.X, lines.Select(l => (double)l[0]).ToList());
                signature.SetFeature(Features.Y, lines.Select(l => (double)l[1]).ToList());
                signature.SetFeature(Features.T, lines.Select(l => (double)l[2]).ToList());
                
                // There are no upstrokes in the database, the starting points of downstrokes are marked by button=0 values 
                var button = signature.GetFeature(Svc2004.Button).ToArray();
                var pointType = new double[button.Length];
                for (int i = 0; i < button.Length; i++)
                {
                    if (button[i] == 0)
                        pointType[i] = 1;
                    else if (i == button.Length-1 ||  button[i + 1] == 0)
                        pointType[i] = 2;
                    else
                        pointType[i] = 0;

                }
                signature.SetFeature(Features.PointType, pointType.ToList());

                SignatureHelper.CalculateStandardStatistics(signature);
            }

            if (lines[0].Length == 7) // Task2
            {
                List<int> azimuth = lines.Select(l => l[4]).ToList();
                List<int> altitude = lines.Select(l => l[5]).ToList();
                List<int> pressure = lines.Select(l => l[6]).ToList();
                signature.SetFeature(Svc2004.Azimuth, azimuth);
                signature.SetFeature(Svc2004.Altitude, altitude);
                signature.SetFeature(Svc2004.Pressure, pressure);
                if (standardFeatures)
                {
                    signature.SetFeature(Features.Azimuth, lines.Select(l => (double)l[4]).ToList());
                    signature.SetFeature(Features.Altitude, lines.Select(l => (double)l[5]).ToList());
                    signature.SetFeature(Features.Pressure, lines.Select(l => (double)l[6]).ToList().ToList());
                }
            }

        }



    }
}
