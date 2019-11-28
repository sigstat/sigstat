using Newtonsoft.Json;
using SigStat.Common;
using SigStat.Common.Loaders;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;

/// <summary>
/// Copied from SigStat.Common
/// Features are different
/// </summary>
namespace SigStat.FusionBenchmark.Loaders
{
    /// <summary>
    /// Set of features containing raw data loaded from SVC2004-format database.
    /// This part is different from SigStat.Common.Svc2004Loader
    /// </summary>
    public static class Svc2004
    {
        public static readonly FeatureDescriptor<List<double>> X = FusionFeatures.X;
        /// <summary>
        /// Y cooridnates from the online signature imported from the SVC2004 database
        /// </summary>
        public static readonly FeatureDescriptor<List<double>> Y = FusionFeatures.Y;
        /// <summary>
        /// T values from the online signature imported from the SVC2004 database
        /// </summary>
        public static readonly FeatureDescriptor<List<double>> T = FusionFeatures.T;
        /// <summary>
        /// Button values from the online signature imported from the SVC2004 database
        /// </summary>
        public static readonly FeatureDescriptor<List<bool>> Button = FusionFeatures.Button;
        /// <summary>
        /// Azimuth values from the online signature imported from the SVC2004 database
        /// </summary>
        public static readonly FeatureDescriptor<List<double>> Azimuth = FusionFeatures.Azimuth;
        /// <summary>
        /// Altitude values from the online signature imported from the SVC2004 database
        /// </summary>
        public static readonly FeatureDescriptor<List<double>> Altitude = FusionFeatures.Altitude;
        /// <summary>
        /// Pressure values from the online signature imported from the SVC2004 database
        /// </summary>
        public static readonly FeatureDescriptor<List<double>> Pressure = FusionFeatures.Pressure;



    }

    [JsonObject(MemberSerialization.OptOut)]
    public class Svc2004OnlineLoader : DataSetLoader
    {
        private struct SignatureFile
        {
            public string File { get; set; }
            public string SignerID { get; set; }
            public string SignatureID { get; set; }

            public SignatureFile(string file)
            {
                File = file;
                string name = file.Split(Path.PathSeparator).Last();//handle if file is in zip directory
                var parts = Path.GetFileNameWithoutExtension(name).Replace("U", "").Split("S");
                if (parts.Length != 2)
                {
                    throw new InvalidOperationException("Invalid file format. All signature files should be in 'U__S__.txt' format");
                }
                SignerID = parts[0].PadLeft(3, '0');
                SignatureID = parts[1].PadLeft(3, '0');
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
        public Svc2004OnlineLoader(string databasePath, bool standardFeatures)
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
        public Svc2004OnlineLoader(string databasePath, bool standardFeatures, Predicate<Signer> signerFilter = null)
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

            var signatureGroups = Directory.GetFileSystemEntries(DatabasePath, "*.TXT", SearchOption.AllDirectories).Select(f => new SignatureFile(f)).GroupBy(sf => sf.SignerID);

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
                    this.LoadSignature(signature, (signatureFile.File).GetPath(), StandardFeatures);

                    signature.Origin = int.Parse(signature.ID) < 21 ? Origin.Genuine : Origin.Forged;
                    signer.Signatures.Add(signature);

                }
                signer.Signatures = signer.Signatures.OrderBy(s => s.ID).ToList();
                yield return signer;
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
            signature.SetFeature(Svc2004.X, lines.Select(l => (double)l[0]).ToList());
            signature.SetFeature(Svc2004.Y, lines.Select(l => (double)l[1]).ToList());
            signature.SetFeature(Svc2004.T, lines.Select(l => (double)l[2]).ToList());
            signature.SetFeature(Svc2004.Button, lines.Select(l => (l[3] == 1)).ToList());
            

            if (lines[0].Length == 7) // Task2
            {
                List<int> azimuth = lines.Select(l => l[4]).ToList();
                List<int> altitude = lines.Select(l => l[5]).ToList();
                List<int> pressure = lines.Select(l => l[6]).ToList();
                
                if (standardFeatures)
                {
                    double azimuthmax = azimuth.Max();
                    double altitudemax = altitude.Max();
                    double pressuremax = pressure.Max();
                    signature.SetFeature(Svc2004.Azimuth, azimuth.Select(a => a / azimuthmax * 2 * Math.PI).ToList());
                    signature.SetFeature(Features.Altitude, altitude.Select(a => a / altitudemax).ToList());
                    signature.SetFeature(Features.Pressure, pressure.Select(a => a / pressuremax).ToList());
                }
  
            }
        }
    }
}
