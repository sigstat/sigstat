using SigStat.Common;
using SigStat.Common.Loaders;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.IO.Compression;
using Microsoft.Extensions.Logging;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;


namespace SigStat.FusionBenchmark.Loaders
{
    public class Svc2004OfflineLoader : DataSetLoader
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
                var parts = Path.GetFileNameWithoutExtension(name).Split("_e_");
                SignatureOrigin = Origin.Genuine;
                if (parts.Length != 2)
                {
                    parts = Path.GetFileNameWithoutExtension(name).Split("_h_");
                    SignatureOrigin = Origin.Forged;
                    if (parts.Length != 2)
                    {
                        throw new InvalidOperationException("Invalid file format. All signature files should be in '***_e***.png' or '***_h_***.png' format");
                    }
                }
                SignerID = parts[0].PadLeft(3, '0');
                SignatureID = parts[1].PadLeft(3, '0');
            }
        }

        public string DatabasePath { get; set; }

        public Predicate<Signer> SignerFilter { get; set; }

        public override int SamplingFrequency => throw new NotImplementedException();

        public Svc2004OfflineLoader(string databasePath, Predicate<Signer> signerFilter = null)
        {
            DatabasePath = databasePath;
            SignerFilter = signerFilter;
        }

        public override IEnumerable<Signer> EnumerateSigners(Predicate<Signer> signerFilter)
        {
            List<Signer> signers = new List<Signer>();
            this.LogInformation("SVC2004OfflineLoader - EnumerateSigners started");
            this.LogInformation(DatabasePath);
            var signatureGroups = Directory.GetFileSystemEntries(DatabasePath, "*.png", SearchOption.AllDirectories).Select(f => new SignatureFile(f)).GroupBy(sf => sf.SignerID);

            this.LogTrace(signatureGroups.Count().ToString() + " signers found in database");
            foreach (var group in signatureGroups)
            {
                Signer signer = new Signer { ID = group.Key };

                if (signerFilter != null && !signerFilter(signer))
                {
                    this.LogInformation("SVC2004OfflineLoader - Predicate");
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
                    if (signature.Origin == Origin.Forged)
                    {
                        signature.ID = (int.Parse(signature.ID) + 20).ToString().PadLeft(3, '0');
                    }
                    signer.Signatures.Add(signature);
                }
                signer.Signatures = signer.Signatures.OrderBy(s => s.ID).ToList();
                signers.Add(signer);
            }
            
            this.LogInformation("SVC2004OfflineLoader - EnumerateSigners finished");
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
