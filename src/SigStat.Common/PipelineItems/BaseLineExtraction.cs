using System;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using SigStat.Common;
using SixLabors.ImageSharp.Processing;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using SigStat.Common.Helpers;

namespace Alairas.Common
{
    //TODO: rendberak
    public class BaseLineExtraction : PipelineBase, ITransformation
    {
        public void Transform(Signature signature)
        {
            var image = signature.GetFeature(Features.Image);
            using (var tmp = image.Clone())
            {
                tmp.Mutate(ctx =>
                {
                    // TODO: use R-Y algorithm for binarization: http://www.aforgenet.com/framework/docs/html/2d3c83ea-f15d-6905-b790-dcc6c15f6ca3.htm
                    ctx.BlackWhite();
                    // TODO:
                    //ctx.Invert()
                    //ctx.Dilatation()
                    //ctx.Dilatation()
                    //ctx.Erosion()
                    //ctx.Erosion()
                    //ctx.LabelConnectedComponents()
                });
                var envelopes = GetComponentLowerEnvelopes(tmp);
                RemoveOverlapingEnvelopes(envelopes);
            }
        }




        /// <summary>
        /// Extracts lower envelope for each component
        /// </summary>
        /// <param name="bmp"></param>
        /// <returns></returns>
        public static List<List<Point>> GetComponentLowerEnvelopes(Image<Rgba32> image)
        {
            // TODO: optimalizálni
            List<List<Point>> result = new List<List<Point>>();
            Dictionary<int, byte> colors = new Dictionary<int, byte>
            {
                {0,0}
            };
            byte[,] img = new byte[image.Width, image.Height];
            for (int i = 0; i < image.Width; i++)
            {
                for (int j = 0; j < image.Height; j++)
                {
                    if (colors.TryGetValue(image[i, j].R + 256 * image[i, j].G + 256 * 256 * image[i, j].B, out var component))
                    {
                        img[i, j] = component;
                    }
                    else
                    {
                        colors.Add(image[i, j].R + 256 * image[i, j].G + 256 * 256 * image[i, j].B, (byte)colors.Count);
                        img[i, j] = (byte)(colors.Count - 1);
                    }
                }
            }

            int index = 1;
            while (index == 1 || result.Last().Count != 0)
            {
                var component = new List<Point>();
                result.Add(component);
                // lentről felfelé és balról jobbra haladva feltérképezzük a görbéket
                for (int x = 0; x < img.GetLength(0); x++)
                {
                    for (int y = img.GetLength(1) - 1; y >= 0; y--)
                    {
                        if (img[x, y] == index)
                        {
                            component.Add(new Point(x, y));
                            break;
                        }
                    }

                }
                index++;
            }
            result.RemoveAt(result.Count - 1);
            return result;

        }

        /// <summary>
        /// Eltávolítja az egymás fölött lévő komponensek közül a felsőket, amennyiben az átfedés mértéke legalább 50%
        /// </summary>
        /// <param name="envelopes"></param>
        /// <remarks>Az eltávolított komponensek függhetnek a feldolgozás sorrendjétől, így több, egymást kölcsönösen átfedő komponens esetén előfordulhat, hogy hibás eredményt kapunk.</remarks>
        private void RemoveOverlapingEnvelopes(List<List<Point>> envelopes)
        {
            // TODO: optimalizálni

            var pairs = (from e1 in envelopes
                         from e2 in envelopes
                         select new { E1 = e1, E2 = e2 })
                        .Where(p => p.E1 != p.E2)
                        .ToList();
            // optimalizálás: jelenleg minden pár kétszer kerül be a válogatásba

            foreach (var pair in pairs)
            {
                if (!envelopes.Contains(pair.E1) || !envelopes.Contains(pair.E2))
                {
                    continue;
                }

                var intersect = pair.E1.Select(p => p.X).Intersect(pair.E2.Select(p => p.X)).ToList();
                if (intersect.Count == 0)
                {
                    continue;
                }          
                var intersectCount = intersect.Count;

                // 50% fölötti átfedésre lövünk csak
                if (intersectCount < pair.E1.Count / 2 && intersectCount < pair.E2.Count / 2)
                {
                    continue;
                }

                // A felsőt töröljük ===> Ez az ékezeteket akarta törölni, de néhány szüvegben vannak alsó ékezetek is
                
                int length1 = Math.Abs(pair.E1.Last().X - pair.E1.First().X);
                int length2 = Math.Abs(pair.E2.Last().X - pair.E2.First().X);
                if (length1 > length2)
                {
                    envelopes.Remove(pair.E2);
                }
                else
                {
                    envelopes.Remove(pair.E1);
                }


            }

        }

        /// <summary>
        /// Megkeresi a megadott pontokra legjobban illeszkedő egyenest. Képes még ennek az egyenesnek különböző 
        /// hibamértékeinek kiszámítására, azonban jelenleg ezzel nem foglalkozom, hiszen ebben a speciális esetben
        /// szinte biztosan elég jól illeszkedő egyenest kapunk eredményül.
        /// Az algorimus kimenete nem egy egyenes, hanem egy egy vektor, mely az egyenes egy szakasza. Felteszem,
        /// hogy a pontok X koordináta szerint rendezettek, így az első és utolsó pont X koordinátája közötti
        /// szakaszt adom vissza az egyenesből.
        /// Azaz:
        /// Paraméterként egy előzőleg megtalált komponenst kap, kimenete pedig az adott komponens alapvonala.
        /// </summary>
        /// <param name="points">A pontok, melyre az egyenest illeszteni szeretnénk.</param>
        public static Baseline GetLineOfBestFit(List<Point> points)
        {
            int numPoints = points.Count;
            // Ha nincs elég pont az egyenes illestéshez akkor null-t adunk vissza. <-- FONTOS
            if (numPoints < 2)
            {
                return null;
            }

            double sumX, sumY, sumXX, sumYY, sumXY;
            double a, b;        // az featureAndOriginaties(x) = a + b*x egyenes együtthatói

            sumX = 0;
            sumY = 0;
            sumXX = 0;
            sumYY = 0;
            sumXY = 0;

            foreach (Point p in points)
            {
                sumX += p.X;
                sumY += p.Y;
                sumXX += p.X * p.X;
                sumYY += p.Y * p.Y;
                sumXY += p.X * p.Y;
            }

            if (Math.Abs((double)numPoints * sumXX - sumX * sumX) > Double.Epsilon)
            {
                b = ((double)numPoints * sumXY - sumY * sumX) /
                    ((double)numPoints * sumXX - sumX * sumX);
                a = (sumY - b * sumX) / (double)numPoints;
            }
            else
            {
                a = 0.0;
                b = 0.0;
            }

            return new Baseline(points[0].X, (int)(a + b * points[0].X), points[numPoints - 1].X - points[0].X, (int)(a + b * points[numPoints - 1].X) - (int)(a + b * points[0].X));

        }

        public void Run(Signature input)
        {
            Transform(input);
        }
    }
}
