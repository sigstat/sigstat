using SigStat.Common.Helpers;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace SigStat.Common.Loaders
{
    /// <summary>
    /// DataSetLoader for Image type databases.
    /// Similar format to Svc2004Loader, but finds png images.
    /// </summary>
    public class ImageLoader : DataSetLoader
    {
        private readonly string DatabasePath;

        /// <summary>
        /// Initializes a new instance of the <see cref="ImageLoader"/> class with specified database.
        /// </summary>
        /// <param name="databasePath">File path to the database.</param>
        public ImageLoader(string databasePath)
        {
            DatabasePath = databasePath;
        }

        private struct SignatureFile
        {
            public string File { get; set; }
            public string SignerID { get; set; }
            public string SignatureID { get; set; }

            public SignatureFile(string file)
            {
                File = file;
                var parts = Path.GetFileNameWithoutExtension(file).Replace("U", "").Split('S');
                if (parts.Length != 2)
                {
                    throw new InvalidOperationException("Invalid file format. All signature files should be in 'U__S__.png' format");
                }
                SignerID = parts[0].PadLeft(2, '0');
                SignatureID = parts[1].PadLeft(2, '0');
            }
        }

        /// <inheritdoc/>
        public override IEnumerable<Signer> EnumerateSigners(Predicate<Signer> signerFilter)
        {
            Log(LogLevel.Info, "Enumerating signers started.");
            var signatureGroups = Directory.EnumerateFiles(DatabasePath, "U*S*.png")
                .Select(f => new SignatureFile(f))
                .GroupBy(sf => sf.SignerID);


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
                    LoadImage(signature, signatureFile.File);
                    signature.Origin = int.Parse(signature.ID) < 21 ? Origin.Genuine : Origin.Forged;
                    signer.Signatures.Add(signature);
                }
                yield return signer;
            }
            Log(LogLevel.Info, "Enumerating signers finished.");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        public static Signature LoadSignature(string file)
        {
            Signature signature = new Signature()
            {
                ID = Path.GetFileNameWithoutExtension(file),
                Origin = Origin.Unknown
            };
            LoadImage(signature, file);
            return signature;

        }

        /// <summary>
        /// Load one image.
        /// </summary>
        /// <param name="signature">The signature that receives the new <see cref="Features.Image"/></param>
        /// <param name="file">File path to the image to be loaded.</param>
        protected static void LoadImage(Signature signature, string file)
        {
            Image<Rgba32> image = Image.Load(file);
            signature.SetFeature(Features.Image, image);
        }

    }
}
