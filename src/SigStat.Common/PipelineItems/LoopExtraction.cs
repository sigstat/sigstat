//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Drawing;
//using System.Diagnostics;
//using SigStat.Common;

//namespace Alairas.Common
//{
//    public partial class LoopExtraction : ITransformation
//    {

//        public void Transform(Signature signature)
//        {
//            var img = signature.GetFeature(Features.Image);

//            using (var grayBmp = Grayscale.CommonAlgorithms.RMY.Apply(bmp))
//            {
//                SetProgress("Grayscale.RMY", grayBmp, 5);
//                // Fekete-fehérré alakítjuk
//                new IterativeThreshold(2, 128).ApplyInPlace(grayBmp);
//                SetProgress("IterativeThreshold", grayBmp, 10);

//                Bitmap grayBmp2 = (Bitmap)grayBmp.Clone();
//                // Feltöltjük a hátteret a toll színével megegyező színnel, így csak a belső elemek maradnak meg
//                new PointedColorFloodFill() { FillColor = Color.Black }.ApplyInPlace(grayBmp2);
//                SetProgress("PointedColorFloodFill", grayBmp2, 15);

//                // Lyukakat betömjük. Erre ritkán, de szükség van (lásd SVC20 001_e_001)
//                new FillHoles().ApplyInPlace(grayBmp2);
//                SetProgress("FillHoles", grayBmp2, 20);

//                //// invertáljuk, hogy a későbbi szűrők jól működjenek
//                new Invert().ApplyInPlace(grayBmp);
//                SetProgress("Invert", grayBmp, 25);

//                // Összekötjük a nagyon közeli komponenseket
//                new Dilatation().ApplyInPlace(grayBmp);
//                SetProgress("Dilatation", grayBmp, 30);
//                new Dilatation().ApplyInPlace(grayBmp);
//                SetProgress("Dilatation", grayBmp, 35);
//                new Erosion().ApplyInPlace(grayBmp);
//                SetProgress("Erosion", grayBmp, 40);
//                new Erosion().ApplyInPlace(grayBmp);
//                SetProgress("Erosion", grayBmp, 45);
//                new Invert().ApplyInPlace(grayBmp);
//                SetProgress("Invert", grayBmp, 50);

//                // Feltöltjük a hátteret a toll színével megegyező színnel, így csak a belső elemek maradnak meg
//                new PointedColorFloodFill() { FillColor = Color.Black }.ApplyInPlace(grayBmp);
//                SetProgress("PointedColorFloodFill", grayBmp, 55);

//                // Lyukakat betömjük. Erre ritkán, de szükség van (lásd SVC 001_e_001)
//                new FillHoles().ApplyInPlace(grayBmp);
//                SetProgress("FillHoles", grayBmp, 60);


//                // Összevonjuk az eredeti képen és az "összekötött" képen talált hurkokat
//                new Add(grayBmp2).ApplyInPlace(grayBmp);
//                SetProgress("Add", grayBmp, 65);

//                // Kis hurkok összekapcsolása a nagy szomszéddal <= ez nem jött be
//                //new Dilatation().ApplyInPlace(grayBmp);
//                //new Dilatation().ApplyInPlace(grayBmp);
//                //new Dilatation().ApplyInPlace(grayBmp);
//                //new Dilatation().ApplyInPlace(grayBmp);

//                // zajszűrés: 5*5 pixelnél kisebb elemeket kidobjuk. Ezek sokszor tévesen keletkeznek az "összezárás" miatt
//                new BlobsFiltering(5, 5, int.MaxValue, int.MaxValue).ApplyInPlace(grayBmp);
//                SetProgress("BlobsFiltering", grayBmp, 70);

//                ConnectedComponentsLabeling.ColorTable = ColorHelper.ColorTable;
//                // Beazonosítjuk az összefüggő komponenseket
//                using (var connectedBmp = new ConnectedComponentsLabeling().Apply(grayBmp))
//                {

//                    SetProgress("ConnectedComponentsLabeling", connectedBmp, 75);

//                    // Debug kép, hogy látszódjon, mennyire illeszkedik a kinyert görbe az eredetihez
//                    using (Bitmap debug = new Bitmap(bmp.Width, bmp.Height, PixelFormat.Format24bppRgb))
//                    {
//                        using (Graphics g = Graphics.FromImage(debug))
//                            g.DrawImage(bmp, 0, 0, bmp.Width, bmp.Height);
//                        new Subtract(connectedBmp).ApplyInPlace(debug);
//                        SetProgress("Add", debug, 80);

//                        sig.Loops = new List<Loop>();
//                        var components = connectedBmp.GetComponents();
//                        foreach (var component in components)
//                        {
//                            // HACK: itt valami bug van, Svc2011, 03_013-ban egy 2 pixeles komponenst találunk a kerület kinyerése során
//                            try
//                            {
//                                // Kinyerjük az összefüggő komponensek adatait
//                                var loop = FeatureExtractor.Extract(component);
//                                sig.Loops.Add(loop);
//                            }
//                            catch (Exception exc)
//                            {
//                                WriteLog(exc.Message);
//                            }

//                        }
//                    }

                  

//                }
//            }
//        }







//    }
//}
