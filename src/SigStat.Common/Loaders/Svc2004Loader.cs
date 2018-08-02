using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Linq;

namespace SigStat.Common.Loaders
{

    public class Svc2004
    {
        public static readonly FeatureDescriptor X = new FeatureDescriptor("X(t)", "Svc2004.X", typeof(List<int>));
        public static readonly FeatureDescriptor Y = new FeatureDescriptor("Y(t)", "Svc2004.Y", typeof(List<int>));
        public static readonly FeatureDescriptor T = new FeatureDescriptor("t", "Svc2004.t", typeof(List<int>));
        public static readonly FeatureDescriptor Button = new FeatureDescriptor("Button(t)", "Svc2004.Button", typeof(List<int>));
        public static readonly FeatureDescriptor Azimuth = new FeatureDescriptor("Azimuth(t)", "Svc2004.Azimuth", typeof(List<int>));
        public static readonly FeatureDescriptor Altitude = new FeatureDescriptor("Altitude(t)", "Svc2004.Altitude", typeof(List<int>));
        public static readonly FeatureDescriptor Pressure = new FeatureDescriptor("Pressure(t)", "Svc2004.Pressure", typeof(List<int>));

        //TODO: ez nem igazan Feature, hanem csak a pipeline reszeredmenyeihez kell
        //-> vagy toroljuk oket a vegen, vagy a pipeline-nak is kellenek descriptorok
        public static readonly FeatureDescriptor[] Task1 = new FeatureDescriptor[] { X, Y, T, Button };
        public static readonly FeatureDescriptor[] Task2 = new FeatureDescriptor[] { X, Y, T, Button, Azimuth, Altitude, Pressure };

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
                var parts = Path.GetFileNameWithoutExtension(file).Replace("U", "").Split('S');
                if (parts.Length != 2)
                    throw new InvalidOperationException("Invalid file format. All signature files should be in 'U__S__.txt' format");
                SignerID = parts[0].PadLeft(2, '0');
                SignatureID = parts[1].PadLeft(2, '0');
            }
        }

        public string DatabasePath { get; set; }
        public bool StandardFeatures { get; }

        public Svc2004Loader(string databasePath, bool standardFeatures)
        {
            DatabasePath = databasePath;
            StandardFeatures = standardFeatures;
        }



        public override IEnumerable<Signer> EnumerateSigners(Predicate<string> signerFilter = null)
        {
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
                    LoadSignature(signature, signatureFile.File);
                    signature.Origin = int.Parse(signature.ID) < 21 ? Origin.Genuine : Origin.Forged;
                    signer.Signatures.Add(signature);
                }
                yield return signer;
            }
        }

        public void LoadSignature(Signature signature, string file)
        {
            var lines = File.ReadAllLines(file)
                .Skip(1)
                .Select(l => l.Split(' ').Select(s => int.Parse(s)).ToArray())
                .ToList();

            // Task1, Task2
            signature.SetFeature(Svc2004.X, lines.Select(l => l[0]).ToList());
            signature.SetFeature(Svc2004.Y, lines.Select(l => l[1]).ToList());
            signature.SetFeature(Svc2004.T, lines.Select(l => l[2]).ToList());
            signature.SetFeature(Svc2004.Button, lines.Select(l => l[3]).ToList());
            if (StandardFeatures)
            {
                signature.SetFeature(Features.X, lines.Select(l => (double)l[0]).ToList());
                signature.SetFeature(Features.Y, lines.Select(l => (double)l[1]).ToList());
                signature.SetFeature(Features.T, lines.Select(l => (double)l[2]).ToList());
                signature.SetFeature(Features.Button, lines.Select(l => (double)l[3]).ToList());
            }

            if (lines[0].Length == 7) // Task2
            {
                signature.SetFeature(Svc2004.Azimuth, lines.Select(l => l[4]).ToList());
                signature.SetFeature(Svc2004.Altitude, lines.Select(l => l[5]).ToList());
                signature.SetFeature(Svc2004.Pressure, lines.Select(l => l[6]).ToList());
                if (StandardFeatures)
                {
                    signature.SetFeature(Features.Azimuth, lines.Select(l => (double)l[4]).ToList());
                    signature.SetFeature(Features.Altitude, lines.Select(l => (double)l[5]).ToList());
                    signature.SetFeature(Features.Pressure, lines.Select(l => (double)l[6]).ToList());
                }
            }
        }
    }
}
