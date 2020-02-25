using Newtonsoft.Json;
using SigStat.Common;
using SigStat.Common.Loaders;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;



namespace SigStat.FusionBenchmark.Loaders
{

    public static class BiosecureID
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
    public class BiosecureIDOnlineLoader : DataSetLoader
    {
        private struct SignatureFile
        {
            private static readonly Origin[] origins = new[]
            {
                Origin.Unknown,
                Origin.Genuine,
                Origin.Genuine,
                Origin.Forged,
                Origin.Forged,
                Origin.Forged,
                Origin.Genuine,
                Origin.Genuine
            };

            public string File { get; set; }
            public string SignerID { get; set; }
            public string SignatureID { get; set; }
            public Origin SignatureOrigin { get; set; }
            public SignatureFile(string file)
            {
                File = file;
                string name = file.Split(Path.PathSeparator).Last();//handle if file is in zip directory
                var parts = Path.GetFileNameWithoutExtension(name).Replace("u", "").Replace("_sg", "_").Replace("s", "_").Replace(".txt", "").Split("_");

                
                if (parts.Length != 3)
                {
                    throw new InvalidOperationException("Invalid file format. All signature files should be in 'u****s****_sg****f/g.txt' format");
                }
                int newSignerID = Int32.Parse(parts[0]) - 1000;
                int sessionID = Int32.Parse(parts[1]);
                int sigID = Int32.Parse(parts[2]);
                SignatureOrigin = Origin.Unknown;
                SignatureOrigin = origins[sigID];
                if (SignatureOrigin == Origin.Unknown)
                {
                    throw new InvalidOperationException("Can't decide origin");
                }
                SignerID = newSignerID.ToString().PadLeft(4, '0');
                SignatureID = sessionID.ToString().PadLeft(4, '0') + "_" + sigID.ToString().PadLeft(4, '0');
            }
        }

        public string DatabasePath { get; set; }

        public Predicate<Signer> SignerFilter { get; set; }

        public override int SamplingFrequency => throw new NotImplementedException();

        public BiosecureIDOnlineLoader(string databasePath, Predicate<Signer> signerFilter = null)
        {
            DatabasePath = databasePath;
            SignerFilter = signerFilter;
        }

        public override IEnumerable<Signer> EnumerateSigners(Predicate<Signer> signerFilter)
        {
            List<Signer> signers = new List<Signer>();
            this.LogInformation("BiosecureID Online - EnumerateSigners started");
            this.LogInformation(DatabasePath);


            var signatureGroups = Directory.GetFileSystemEntries(DatabasePath, "*.txt", SearchOption.AllDirectories).Select(f => new SignatureFile(f)).GroupBy(sf => sf.SignerID);

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

                    this.LoadOnlineSignature(signature, (signatureFile.File).GetPath());
                    signature.Origin = signatureFile.SignatureOrigin;
                    signer.Signatures.Add(signature);
                }
                signer.Signatures = signer.Signatures.OrderBy(s => s.ID).ToList();
                signers.Add(signer);
            }

            this.LogInformation("BioSecureID Online - EnumerateSigners finished");
            return signers;
        }
        public void LoadOnlineSignature(Signature signature, string file)
        {
            this.LogInformation(file.GetPath());
            string[] lines = File.ReadAllLines(file);
            int n = int.Parse(lines[0]);
            var xs = new double[n];
            var ys = new double[n];
            var ps = new double[n];
            var bs = new bool[n];
            var ts = new double[n];

            for (int i = 0; i < n; i++)
            {
                var parts = lines[i + 1].Replace(".", ",").Split(" ");
                if (parts.Length != 3)
                {
                    throw new Exception();
                }
                xs[i] = double.Parse(parts[0]);
                ys[i] = double.Parse(parts[1]);
                ps[i] = double.Parse(parts[2]);
                bs[i] = ps[i] == 0.0 ? false : true;
                ts[i] = (double)i;
            }

            signature.SetFeature<List<double>>(BiosecureID.X, xs.ToList());
            signature.SetFeature<List<double>>(BiosecureID.Y, ys.ToList());
            signature.SetFeature<List<double>>(BiosecureID.Pressure, ps.ToList());
            signature.SetFeature<List<bool>>(BiosecureID.Button, bs.ToList());
            signature.SetFeature<List<double>>(BiosecureID.T, ts.ToList());
        }
    }
}