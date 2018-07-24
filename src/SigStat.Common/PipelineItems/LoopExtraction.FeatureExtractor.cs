//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Drawing;
//using System.Diagnostics;
//using Alairas.Common.HelperClasses;
//// TODO: az ebben a névtérben lévő 3 osztályt is ki kéne írtani innen
//using Alairas.Common.FeatureExtractions.FeatureSpecificMeasurements;
//using SigStat.Common;

//namespace Alairas.Common
//{

//    public partial class LoopExtraction
//    {
//        /// <summary>
//        /// A különböző shape descriptorok kinyerése
//        /// </summary>
//        public class FeatureExtractor
//        {


//            public static Loop Extract(IEnumerable<Point> points)
//            {
//                var fe = new FeatureExtractor(points);
//                fe.Fill();
//                return fe.Loop;
//            }


//            private FeatureExtractor(IEnumerable<Point> allPoints)
//            {
//                this.allPoints = allPoints.ToList();
//                this.perimeterPoints = WritingHelper.GetPerimeter(allPoints).ToList();
//                this.innerPoints = allPoints.Except(perimeterPoints, new PointEqualityComparer()).ToList();
//                Loop = new Loop();

//            }

//            private List<Point> allPoints; 
//            private List<Point> innerPoints;
//            private List<Point> perimeterPoints;
//            private List<Point> convexHull;

//            public Loop Loop { get; set; }


//            public void Fill()
//            {
//                InitPerimeter();
//                InitArea();
//                InitFormFactor();
//                InitMaximumDiameter();
//                InitRoundness();
//                InitBoundingBox();
//                InitInscribedDiameter();
//                InitModificationRatio();
//                InitExtent();
//                InitCompactness();
//                InitBoundingCircle();
//                InitCentroid();
//                InitMomentAxisAngle();
//                InitAspectRatio();
//                InitConvexity();
//                InitSolidity();
//            }

//            #region Shape Descriptors

//            public void InitPerimeter()
//            {
//                if (perimeterPoints.Count < 3)
//                    throw new Exception("At least 3 points are required for perimeter calculation");
                   
//                int orthogonal = 0;
//                int diagonal = 0;
//                for (int i = 0; i < perimeterPoints.Count - 1; i++)
//                {
//                    Point p1 = perimeterPoints[i];
//                    Point p2 = perimeterPoints[i + 1];
//                    int mult = (p1.X - p2.X) * (p1.X - p2.X) + (p1.Y - p2.Y) * (p1.Y - p2.Y);
//                    if (mult == 1) orthogonal++;
//                    else if (mult == 2) diagonal++;
//                    else throw new Exception("Invalid perimeter points");
//                }
//                Loop.Perimeter = 0.948 * orthogonal + 1.34 * diagonal;
//            }

//            private void InitArea()
//            {
//                // Három alternatív megoldást is választhatunk itt...
//                //Loop.Area = innerPoints.Count;
//                //Loop.Area = innerPoints.Count+ perimeterPoints.Count;
//                Loop.Area = Loop.Perimeter + innerPoints.Count;

//                if (Loop.Area <= 0)
//                    throw new Exception("Invalid area");
//            }



//            private void InitFormFactor()
//            {
//                Loop.Formfactor = (4 * Math.PI * Loop.Area) / (Loop.Perimeter * Loop.Perimeter);
//            }

//            private void InitMaximumDiameter()
//            {
//                double maxDistanceSquare = double.MinValue;
//                double distanceSquare = 0.0;

//                Point endPoint1 = Point.Empty;
//                Point endPoint2 = Point.Empty;

//                foreach (var p1 in perimeterPoints)
//                {
//                    foreach (var p2 in perimeterPoints)
//                    {
//                        if (p1.Equals(p2))
//                            continue;
//                        distanceSquare = WritingHelper.DistanceSquare(p1, p2);
//                        if (distanceSquare > maxDistanceSquare)
//                        {
//                            maxDistanceSquare = distanceSquare;
//                            endPoint1 = p1;
//                            endPoint2 = p2;
//                        }
//                    }
//                }
//                if (endPoint1.Equals(endPoint2))
//                    throw new Exception();

//                Loop.MaximumDiameter = Math.Sqrt(maxDistanceSquare);

//                if (endPoint2.X > endPoint1.X)
//                {
//                    Loop.MaximumDiameterAngle =
//                        Math.Atan2(endPoint1.Y - endPoint2.Y, endPoint2.X - endPoint1.X);
//                }
//                else if (endPoint2.X < endPoint1.X)
//                {
//                    Loop.MaximumDiameterAngle =
//                        Math.Atan2(endPoint2.Y - endPoint1.Y, endPoint1.X - endPoint2.X);
//                }
//                else // same
//                    Loop.MaximumDiameterAngle = Math.PI / 2;
//            }

//            private void InitRoundness()
//            {
//                Loop.Roundness = ((4 * Loop.Area) / (Math.PI *
//                    Loop.MaximumDiameter * Loop.MaximumDiameter));
//            }

//            private void InitCompactness()
//            {
//                Loop.Compactness = ((Math.Sqrt((4.0 / Math.PI) * (double)Loop.Area)
//                    / (Loop.MaximumDiameter)));
//            }

//            private void InitModificationRatio()
//            {
//                Loop.ModificationRatio = Loop.InscribedDiameter / Loop.MaximumDiameter;
//            }

//            private void InitInscribedDiameter()
//            {
//                var edm = new EDM();
//                edm.Points = innerPoints.Select(p=>new PicturePoint(p.X, p.Y)).ToList();
//                edm.DoEDM();

//                Loop.InscribedDiameter = 2.0 * edm.UepDistance;
//            }

//            private void InitExtent()
//            {
//                Loop.Extent = ((double)Loop.Area /
//                    (double)(Loop.BoundingBox.Width * Loop.BoundingBox.Height));
//            }

//            // CenterPoint and Radius
//            private void InitBoundingCircle()
//            {
//                var finder = new BoundingCircleFinder();
//                finder.Points = perimeterPoints.Select(p=>new PicturePoint(p.X, p.Y)).ToList();
//                finder.GetBoundingCircle();

//                var boundingCircle = finder.BoundingCircle;
//                int gX = (int)boundingCircle.CenterPoint.X;
//                int gY = (int)boundingCircle.CenterPoint.Y;

//                Loop.BoundingCircleRadius = boundingCircle.Radius;
//            }

//            // Russ: The Image Processing Handbook (5th), 10.6 and 10.7
//            private void InitMomentAxisAngle()
//            {
//                long sX = 0L;
//                long sY = 0L;
//                long sXX = 0L;
//                long sYY = 0L;
//                long sXY = 0L;

//                foreach (var p in this.allPoints)
//                {
//                    sX += p.X;
//                    sY += p.Y;
//                    sXX += p.X * p.X;
//                    sYY += p.Y * p.Y;
//                    sXY += p.X * p.Y;
//                }

//                double mXX1 = sXX - (sX * sX / Loop.Area);
//                double mYY1 = sYY - (sY * sY / Loop.Area);
//                double mXY1 = sXY - (sX * sY / Loop.Area);

//                double expr1 = (mXX1 - mYY1 + Math.Sqrt((mXX1 - mYY1) * (mXX1 - mYY1) + 4 * mXY1 * mXY1)) /
//                    (2 * mXY1);

//                Loop.MomentAxisAngle = Math.Atan(expr1);

//            }

//            private void InitAspectRatio()
//            {
//                Loop.AspectRatio = Loop.MaximumDiameter / Loop.InscribedDiameter;
//            }

//            // nem kell meglepődni, hogy nagyobb lesz mint 1, hiszen az 
//            // Area-ba a beleszámítjuk a convexen kívüli területet is
//            private void InitSolidity()
//            {
//                double convexArea = 0.0;

//                for (int i = 1; i < convexHull.Count - 1; i++)
//                {
//                    convexArea += GetArea(i, i + 1);
//                }

//                Loop.Solidity = Loop.Area / convexArea;
//            }

//            // a 0. ponttal képzett háromszög területe
//            private double GetArea(int f, int s)
//            {
//                var p0 = convexHull[0];
//                var p1 = convexHull[f];
//                var p2 = convexHull[s];
//                double angle1 = Math.Atan2(p0.Y - p1.Y, p1.X - p0.X);
//                double angle2 = Math.Atan2(p0.Y - p2.Y, p2.X - p0.X);

//                double diff = Math.Abs(angle1 - angle2);
//                if (diff > Math.PI)
//                    diff = (2 * Math.PI) - diff;

//                var l1 = Math.Sqrt(WritingHelper.DistanceSquare(p1, p0));
//                var l2 = Math.Sqrt(WritingHelper.DistanceSquare(p2, p0));

//                double area = (l1 * l2 * Math.Sin(diff)) / 2;
//                return area;
//            }

//            private void InitConvexity()
//            {
//                var finder = new ConvexHullFinder();
//                finder.Points = perimeterPoints.Select(p=>new PicturePoint(p.X, p.Y)).ToList();
//                convexHull = finder.GetConvexHull().Select(p=>new Point(p.X, p.Y)).ToList();
//                double convexPerimeter = 0.0;

//                for (int i = 0; i < convexHull.Count; i++)
//                {
//                    var first = convexHull[i];
//                    var second = convexHull[(i + 1) % convexHull.Count];

//                    // ez nagyobb lesz, mint a tényleges távolság, de talán mindegy, hogy súlyozzuk-e...
//                    convexPerimeter += Math.Sqrt(WritingHelper.DistanceSquare(first, second));
//                }

//                Loop.Convexity = convexPerimeter / Loop.Perimeter;
//            }

//            #endregion

//            #region Other parameters

//            private void InitBoundingBox()
//            {
//                Loop.BoundingBox = perimeterPoints.GetBounds();
//            }

//            private void InitCentroid()
//            {

//                long sumX = 0L;
//                long sumY = 0L;

//                foreach (var p in this.allPoints)
//                {
//                    sumX += p.X;
//                    sumY += p.Y;
//                }

//                int cX = (int)(sumX / this.allPoints.Count);
//                int cY = (int)(sumY / this.allPoints.Count);
//                Loop.Centroid = new Point(cX, cY);
//            }

//            #endregion

//            //#region Helper methods

//            //public DataRectangle ToDataRectangle(Rectangle original)
//            //{
//            //    double x = (double)original.X / signature.OriginalBounds.Width;
//            //    double y = (double)original.Y / signature.OriginalBounds.Height;
//            //    double w = (double)original.Width / signature.OriginalBounds.Width;
//            //    double h = (double)original.Height / signature.OriginalBounds.Height;

//            //    return new DataRectangle(x, y, w, h);
//            //}

//            //public DataPoint ToDataPoint(PicturePoint picturePoint)
//            //{
//            //    double x = (double)picturePoint.X / signature.OriginalBounds.Width;
//            //    double y = (double)picturePoint.Y / signature.OriginalBounds.Height;

//            //    return new DataPoint(x, y);
//            //}

//            //#endregion
//        }
//    }
//}
