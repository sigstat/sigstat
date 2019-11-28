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
    [JsonObject(MemberSerialization.OptOut)]
    public class BiosecureIDOfflineLoader : DataSetLoader
    {
        private static readonly FeatureDescriptor<Image<Rgba32>> OutputImage = FusionFeatures.Image;

        private struct SignatureFile
        {
            public string File { get; set; }
            public string SignerID { get; set; }
            public string SignatureID { get; set; }
            public Origin SignatureOrigin { get; set; }
            public SignatureFile(string file)
            {
                File = file;
                string name = file.Split(Path.PathSeparator).Last();//handle if file is in zip directory
                var parts = Path.GetFileNameWithoutExtension(name).Replace("u", "").Replace("_sg", "_").Replace("s", "_").Replace(".png", "").Replace("g", "_g").Replace("f", "_f").Split("_");

                if (parts.Last() != "f" && parts.Last() != "g")
                {
                    throw new InvalidOperationException("Genuine / Forgery, cannot decide");
                }
                else
                {
                    SignatureOrigin = (parts.Last() == "g") ? Origin.Genuine : Origin.Forged;
                }
                if (parts.Length != 4)
                {
                    throw new InvalidOperationException("Invalid file format. All signature files should be in 'u****s****_sg****f/g.png' format");
                }
                int newSignerID = Int32.Parse(parts[0]);
                int sessionID = Int32.Parse(parts[1]);
                int sigID = Int32.Parse(parts[2]);
                SignerID = newSignerID.ToString().PadLeft(4, '0');
                SignatureID = sessionID.ToString().PadLeft(4, '0') + "_" + sigID.ToString().PadLeft(4, '0');
            }
        }

        public string DatabasePath { get; set; }

        public Predicate<Signer> SignerFilter { get; set; }


        public BiosecureIDOfflineLoader(string databasePath, Predicate<Signer> signerFilter = null)
        {
            DatabasePath = databasePath;
            SignerFilter = signerFilter;
        }

        public override IEnumerable<Signer> EnumerateSigners(Predicate<Signer> signerFilter)
        {
            List<Signer> signers = new List<Signer>();
            this.LogInformation("BiosecureID Offline - EnumerateSigners started");
            this.LogInformation(DatabasePath);


            var signatureGroups = Directory.GetFileSystemEntries(DatabasePath, "*.png", SearchOption.AllDirectories).Select(f => new SignatureFile(f)).GroupBy(sf => sf.SignerID);

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
                    this.LoadOfflineSignature(signature, (signatureFile.File).GetPath());
                    signature.Origin = signatureFile.SignatureOrigin;
                    signer.Signatures.Add(signature);
                }
                signer.Signatures = signer.Signatures.OrderBy(s => s.ID).ToList();
                signers.Add(signer);
            }
            
            this.LogInformation("BioSecureID Offline - EnumerateSigners finished");
            return signers;
        }
        public void LoadOfflineSignature(Signature signature, string file)
        {
            this.LogInformation(file.GetPath());
            Image<Rgba32> image = Image.Load(file);
            signature.SetFeature(OutputImage, image);
        }
    }
}

