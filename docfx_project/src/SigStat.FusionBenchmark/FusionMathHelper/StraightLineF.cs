using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace SigStat.FusionBenchmark.FusionMathHelper
{
    public struct StraightLineF
    {
        public float A { get; set; }

        public float B { get; set; }

        public float C { get; set; }

        public StraightLineF(PointF pointF, VectorF normalVectorF)
        {
            //A*x + B*y = A * x_0 + B * y_0
            var unitVectorF = normalVectorF.UnitVector();
            A = unitVectorF.X;
            B = unitVectorF.Y;
            C = -(A * pointF.X + B * pointF.Y);
        }

        public StraightLineF(PointF from, PointF to)
        {
            var dirVectorF = new VectorF(from, to);
            var normalVectorF = dirVectorF.Rotate(Math.PI / 2.0);
            var unitVectorF = normalVectorF.UnitVector();
            A = unitVectorF.X;
            B = unitVectorF.Y;
            C = -(A * from.X + B * from.Y);
        }

        public VectorF NormalVector
        {
            get
            {
                return new VectorF(A, B);
            }
        }

        public VectorF DirVector
        {
            get
            {
                return this.NormalVector.Rotate(Math.PI / 2.0);
            }
        }

        public float M
        {
            get
            {
                var dirVector = this.DirVector;
                if (dirVector.X == 0F)
                {
                    return float.MaxValue;
                }
                dirVector = dirVector.X > 0F ? dirVector : dirVector.Multiply(-1); 
                return dirVector.Y / dirVector.X;
            }
        }

        public double Direction
        {
            get
            {
                var dirVector = this.DirVector;
                return Math.Atan2((double)dirVector.Y, (double)dirVector.X);
            }
        }
        
    }
}
