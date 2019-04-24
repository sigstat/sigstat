using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;

namespace SigStat.Common.Loaders
{
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

        private struct MCYTSignatureFile
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
        }

        public MCYTLoader(string databasePath, bool standardFeatures)
        {
            DatabasePath = databasePath;
            StandardFeatures = standardFeatures;
        }

        public string DatabasePath { get; set; }
        public bool StandardFeatures { get; set; }

        public override IEnumerable<Signer> EnumerateSigners(Predicate<Signer> signerFilter)
        {
            //TODO: EnumerateSigners should ba able to operate with a directory path, not just a zip file
            //signerFilter = signerFilter ?? SignerFilter;

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
            var fpg = reader.ReadChars(4); // FPG 
            var hsize = reader.ReadUInt16(); // Header size
            int ver = 1;
            if ((hsize == 48) || (hsize == 60))
            {
                ver = 2;
            }
            ushort format = reader.ReadUInt16(); // should be '4'

            var m = reader.ReadUInt16();
            var can = reader.ReadUInt16();
            var Ts = reader.ReadUInt32();
            var res = reader.ReadUInt16();
            var ignore = reader.ReadBytes(4);
            var coef = reader.ReadUInt32();
            var mvector = reader.ReadUInt32(); // length of vector
            var nvectores = reader.ReadUInt32(); // number of vectors
            var nc = reader.ReadUInt16();
            uint Fs = 0, mventana = 0, msolapadas = 0;
            if (ver == 2)
            {
                Fs = reader.ReadUInt32();
                mventana = reader.ReadUInt32();
                msolapadas = reader.ReadUInt32();
            }
            stream.Seek(hsize - 12, SeekOrigin.Begin);
            var datos = reader.ReadUInt32();
            var delta = reader.ReadUInt32();
            var ddelta = reader.ReadUInt32();

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
            if(standardFeatures)
            {
                signature.SetFeature(Features.X, X);
                signature.SetFeature(Features.Y, Y);
                signature.SetFeature(Features.Pressure, Pressure);
                signature.SetFeature(Features.Azimuth, Azimuth);
                signature.SetFeature(Features.Altitude, Altitude);
                //Time nincs MCYT-ben, de tudjuk, hogy 100 samples / sec
                //nem kell svc-hez ragaszkodni.. pl adjuk meg millisec-ben, 0 tol kezdve
                List<double> Time = new List<double>();
                for (int i = 0; i < X.Count; i++)
                {
                    Time.Add(i * 10);//ms
                }
                signature.SetFeature(Features.T, Time);
                //Pendown sincs MCYT-ben, mindig lent van? Pressure alapjan lehetne egy threshold
                signature.SetFeature(Features.Button, Enumerable.Repeat(true, X.Count).ToList());
            }


        }

    }
}
