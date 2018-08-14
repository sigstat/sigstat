using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Linq;
using SigStat.Common.Helpers;
using System.IO.Compression;

namespace SigStat.Common.Loaders
{

    public static class Svc2004
    {
        public static readonly FeatureDescriptor<List<int>> X = FeatureDescriptor<List<int>>.Descriptor("Svc2004.X");
        public static readonly FeatureDescriptor<List<int>> Y = FeatureDescriptor<List<int>>.Descriptor("Svc2004.Y");
        public static readonly FeatureDescriptor<List<int>> T = FeatureDescriptor<List<int>>.Descriptor("Svc2004.t");
        public static readonly FeatureDescriptor<List<int>> Button = FeatureDescriptor<List<int>>.Descriptor("Svc2004.Button");
        public static readonly FeatureDescriptor<List<int>> Azimuth = FeatureDescriptor<List<int>>.Descriptor("Svc2004.Azimuth");
        public static readonly FeatureDescriptor<List<int>> Altitude = FeatureDescriptor<List<int>>.Descriptor("Svc2004.Altitude");
        public static readonly FeatureDescriptor<List<int>> Pressure = FeatureDescriptor<List<int>>.Descriptor("Svc2004.Pressure");
    }
    public class Svc2004Loader : DataSetLoader
    {
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
                    throw new InvalidOperationException("Invalid file format. All signature files should be in 'U__S__.txt' format");
                SignerID = parts[0].PadLeft(2, '0');
                SignatureID = parts[1].PadLeft(2, '0');
            }
        }

        public string DatabasePath { get; set; }
        public bool StandardFeatures { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="databasePath">ZIP file</param>
        /// <param name="standardFeatures"></param>
        public Svc2004Loader(string databasePath, bool standardFeatures)
        {
            DatabasePath = databasePath;
            StandardFeatures = standardFeatures;
        }

        public override IEnumerable<Signer> EnumerateSigners(Predicate<string> signerFilter = null)
        {
            Log(LogLevel.Info, "Enumerating signers started.");
            using (ZipArchive zip = ZipFile.OpenRead(DatabasePath))
            {
                //cut names if the files are in directories
                var signatureGroups = zip.Entries.Where(f=>f.Name.EndsWith(".TXT")).Select(f => new SignatureFile(f.FullName)).GroupBy(sf => sf.SignerID);
                Log(LogLevel.Debug, signatureGroups.Count().ToString() + " signers found in database");
                foreach (var group in signatureGroups)
                {
                    if (signerFilter != null && !signerFilter(group.Key))
                        continue;
                    Signer signer = new Signer() { ID = group.Key };
                    foreach (var signatureFile in group)
                    {
                        Signature signature = new Signature()
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
                    yield return signer;
                }
            }
            Log(LogLevel.Info, "Enumerating signers finished.");
        }

        /*
        public override IEnumerable<Signer> EnumerateSigners(Predicate<string> signerFilter = null)
        {
            Log(LogLevel.Info, "Enumerating signers started.");
            var signatureGroups = Directory.EnumerateFiles(DatabasePath, "U*S*.txt")
                .Select(f => new SignatureFile(f))
                .GroupBy(sf => sf.SignerID);


            foreach (var group in signatureGroups)
            {
                if (signerFilter != null && !signerFilter(group.Key))
                    continue;
                Signer signer = new Signer() { ID = group.Key };
                foreach (var signatureFile in group)
                {
                    Signature signature = new Signature()
                    {
                        Signer = signer,
                        ID = signatureFile.SignatureID
                    };
                    LoadSignature(signature, signatureFile.File, StandardFeatures);
                    signature.Origin = int.Parse(signature.ID) < 21 ? Origin.Genuine : Origin.Forged;
                    signer.Signatures.Add(signature);
                }
                yield return signer;
            }
            Log(LogLevel.Info, "Enumerating signers finished.");
        }*/
        
        public static void LoadSignature(Signature signature, string path, bool standardFeatures)
        {
            ParseSignature(signature, File.ReadAllLines(path), standardFeatures);
        } 
             
        public static void LoadSignature(Signature signature, Stream stream, bool standardFeatures)
        {
            using (StreamReader sr = new StreamReader(stream))
            {
                ParseSignature(signature, sr.ReadToEnd().Split( new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None), standardFeatures);
            }
        }

        public static void ParseSignature(Signature signature, string[] linesArray, bool standardFeatures)
        {
            var lines = linesArray
                .Skip(1)
                .Where(l=>l!="")
                .Select(l => l.Split(' ').Select(s => int.Parse(s)).ToArray())
                .ToList();

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
                signature.SetFeature(Features.Button, lines.Select(l => (l[3]==1)).ToList());
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
                    double azimuthmax = azimuth.Max();
                    double altitudemax = altitude.Max();
                    double pressuremax = pressure.Max();
                    signature.SetFeature(Features.Azimuth, azimuth.Select(a => a / azimuthmax * 2 * Math.PI).ToList());
                    signature.SetFeature(Features.Altitude, altitude.Select(a => a / altitudemax).ToList());
                    signature.SetFeature(Features.Pressure, pressure.Select(a => a / pressuremax).ToList());
                }
            }
        }
    }
}
