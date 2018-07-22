using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Collections;
using System.Xml.Serialization;

namespace SigStat.Common
{

    //TODO: This class was directly copied from the AHR framework. Needs revision
    /// <summary>
    /// 
    /// </summary>
    public class Vector 
    {
        #region VectorPoint
        public enum VectorPointKind { Start, End }
        public class VectorPoint : IPoint
        {
            private Vector vector;
            private VectorPointKind kind;

            public VectorPoint(Vector vector, VectorPointKind kind)
            {
                this.vector = vector;
                this.kind = kind;
            }

            public int X
            {
                get { if (kind == VectorPointKind.Start) return vector.X; else return vector.X2; }
                set { if (kind == VectorPointKind.Start) vector.X = value; else vector.X2 = value; }
            }

            public int Y
            {
                get { if (kind == VectorPointKind.Start) return vector.Y; else return vector.Y2; }
                set { if (kind == VectorPointKind.Start) vector.Y = value; else vector.Y2 = value; }

            }

            public Point ToPoint()
            {
                return new Point(X, Y);
            }
        }
        #endregion

        public Vector()
        {
            x = 0;
            y = 0;
            vx = 0;
            vy = 0;
        }
        private int x;

        public int X
        {
            get { return x; }
            set { x = value; }
        }
        private int y;

        public int Y
        {
            get { return y; }
            set { y = value; }
        }
        private int vx;

        public int Vx
        {
            get { return vx; }
            set { vx = value; }
        }
        private int vy;

        public int Vy
        {
            get { return vy; }
            set { vy = value; }
        }

        [XmlIgnore]
        public int X2
        {
            get { return x + vx; }
            set { vx = value - x; }
        }

        [XmlIgnore]
        public int Y2
        {
            get { return y + vy; }
            set { vy = value - vy; }
        }

        [XmlIgnore]
        public Point Start
        {
            get { return new Point(x, y); }
        }

        [XmlIgnore]
        public Point End
        {
            get { return new Point(X2, Y2); }
        }

        public Vector(int x, int y, int vx, int vy)
        {
            this.x = x;
            this.y = y;
            this.vx = vx;
            this.vy = vy;
        }
        public Vector(int vx, int vy)
        {
            this.x = 0;
            this.y = 0;
            this.vx = vx;
            this.vy = vy;
        }

        public Vector(Point p1, Point p2)
        {
            this.x = p1.X;
            this.y = p1.Y;
            this.vx = p2.X - p1.X;
            this.vy = p2.Y - p1.Y;
        }

        public override string ToString()
        {
            return string.Format("Location ({0},{1}), Direction ({2},{3})", x, y, vx, vy);
        }

        /// <summary>
        /// Két vektor akkor egyenlõ, ha ugyanabból a pontból indulnak ki és ugyanabban az irányba
        /// mutatnak és hosszuk is megegyezik. 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            if (obj == null) return false;

            if (this.GetType() != obj.GetType()) return false;


            Vector other = (Vector)obj;

            if (!X.Equals(other.X)) return false;
            if (!Y.Equals(other.Y)) return false;
            if (!Vx.Equals(other.Vx)) return false;
            if (!Vy.Equals(other.Vy)) return false;

            return true;
        }

        public static bool operator ==(Vector v1, Vector v2)
        {
            if (object.ReferenceEquals(v1, null))
                return object.ReferenceEquals(v2, null);
            return v1.Equals(v2);
        }

        public static bool operator !=(Vector v1, Vector v2)
        {
            if (object.ReferenceEquals(v1, null))
                return !object.ReferenceEquals(v2, null);
            return !v1.Equals(v2);
        }


        public static Point operator +(Point point, Vector vector)
        {
            return new Point(point.X + vector.vx, point.Y + vector.vy);
        }

        public static Point operator -(Point point, Vector vector)
        {
            return new Point(point.X - vector.vx, point.Y - vector.vy);
        }


        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public double GetLength()
        {
            return (double)Math.Sqrt(Vx * Vx + Vy * Vy);
        }

        public double Angle
        {
            get
            {
                return Math.Atan2(Vx, Vy);
            }
        }

        public double Length
        {
            get { return Math.Sqrt(vx * vx + vy * vy); }
            set
            {
                // Ha függõleges a vektor
                if (double.IsInfinity(M))
                {
                    if (vy < 0) vy = -(int)Math.Abs(value);
                    else vy = (int)Math.Abs(value);
                    return;
                }
                double newVx = Math.Sqrt(value * value / (1 + M * M));
                double newVy = (M * newVx);
                // A gyökvonás miatt az elõjelek nem biztos, hogy stimmelnek
                if (Math.Sign(newVx) != Math.Sign(vx)) newVx = -newVx;
                if (Math.Sign(newVy) != Math.Sign(vy)) newVy = -newVy;
                vx = (int)newVx;
                vy = (int)newVy;
            }
        }

        /// <summary>
        /// Elofrgatja a vektort 90 fokkal a kezdõpontja körül az óramutató járásával megegyezõ irányba
        /// </summary>
        public Vector GetNormal()
        {
            return new Vector(x, y, vy, -vx);
        }

        /// <summary>
        /// Elofrgatja a vektort 90 fokkal a kezdõpontja körül az óramutató járásával megegyezõ irányba
        /// </summary>
        public Vector GetNormal(double length)
        {
            Vector normal = new Vector(x, y, vy, -vx);
            normal.Length = length;
            return normal;
        }

        public string ToMatlabString()
        {
            return string.Format("[{0},{1},{2},{3}]", x, y, vx, vy);
        }

        public Rectangle Bounds
        {
            get
            {
                return new Rectangle(Math.Min(X, X2), Math.Min(Y, Y2), Math.Abs(X - X2), Math.Abs(Y - Y2));
            }
        }
        public Point COG
        {
            get
            {
                return new Point((X + X2) / 2, (Y + Y2) / 2);
            }
        }

        public double M
        {
            get
            {
                return ((double)vy / (double)vx);
            }
        }

        public double B
        {
            get
            {
                return (double)y - M * x;
            }
        }

        int pointsAdded = 0;
        public void Add(Point p)
        {
            if (pointsAdded == 0)
            {
                X = p.X;
                Y = p.Y;
            }
            else if (pointsAdded == 1)
            {
                X2 = p.X;
                Y2 = p.Y;
            }
            else throw new InvalidOperationException("Csak két pont adható egy vektorhoz");

        }


        public IEnumerator<VectorPoint> GetEnumerator()
        {
            yield return new VectorPoint(this, VectorPointKind.Start);
            yield return new VectorPoint(this, VectorPointKind.End);
        }

        public Vector Clone()
        {
            return new Vector(x, y, vx, vy);
        }




        [XmlIgnore]
        public Point Location
        {
            get { return COG; }
        }

        [XmlIgnore]
        public Rectangle BoundingRectangle
        {
            get { return Bounds; }
        }

        
    }

}
