using SigStat.Common;
using SigStat.Common.Loaders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SigStat.WpfSample.Common
{
    //TODO: xml kommentek, paraméter nélküli függvényeket még lehetne általánosítani az svc2004től különbözőkre
    public class FeatureExtractor
    {
        public int SpacingParameter { get; set; }

        public Signature Signature { get; set; }

        public FeatureExtractor(Signature baseSignature, int spacingParameter = Configuration.DefaultSpacingParameter)
        {
            Signature = baseSignature;
            SpacingParameter = spacingParameter;
        }

        public Signature GetAllDerivedSVC2004Features()
        {
            GetFirstOrderDifference(Features.X);
            GetFirstOrderDifference(Features.Y);
            GetFirstOrderDifference(Features.Azimuth);
            GetFirstOrderDifference(Features.Altitude);
            GetFirstOrderDifference(Features.Pressure);
            GetSecondOrderDifference(Features.X);
            GetSecondOrderDifference(Features.Y);
            GetSineMeasure();
            GetCosineMeasure();
            GetLengthBasedFeatureFirstOrder();
            GetLengthBasedFeatureSecondOrder();
            GetVelocities();
            GetAccelerations();

            return Signature;
        }

        //TODO: technikailag FODra, SODrea is meg lehet hívni ezeket a függvényeket
        public List<double> GetFirstOrderDifference(FeatureDescriptor<List<double>> baseFeatureDescriptor)
        {
            string fodFeatureKey = "FOD" + baseFeatureDescriptor.Key;

            if (Signature.HasFeature(fodFeatureKey))
                return Signature.GetFeature<List<double>>(fodFeatureKey);

            else
            {
                List<double> baseFeature = Signature.GetFeature(baseFeatureDescriptor);
                List<double> firstOrderDiff = new List<double>(baseFeature.Count);

                for (int i = 0; i < baseFeature.Count; i++)
                {
                    if (i < baseFeature.Count - SpacingParameter)
                        firstOrderDiff.Add(baseFeature[i + SpacingParameter] - baseFeature[i]);
                    //Lista kipótlása az utolsó számított értékkel
                    else
                        firstOrderDiff.Add(firstOrderDiff[baseFeature.Count - SpacingParameter - 1]);
                }


                FeatureDescriptor<List<double>> fodFeatureDescriptor = FeatureDescriptor<List<double>>.Descriptor(fodFeatureKey);
                Signature.SetFeature(fodFeatureDescriptor, firstOrderDiff);

                return firstOrderDiff;
            }
        }

        public List<double> GetSecondOrderDifference(FeatureDescriptor<List<double>> baseFeatureDescriptor)
        {
            string sodFeatureKey = "SOD" + baseFeatureDescriptor.Key;

            if (Signature.HasFeature(sodFeatureKey))
                return Signature.GetFeature<List<double>>(sodFeatureKey);

            else
            {
                List<double> firstOrderDiff = GetFirstOrderDifference(baseFeatureDescriptor);

                List<double> secondOrderDiff = new List<double>(firstOrderDiff.Count);

                for (int i = 0; i < firstOrderDiff.Count - 1; i++)
                {
                    secondOrderDiff.Add(firstOrderDiff[i + 1] - firstOrderDiff[i]);
                }

                //Lista kipótlása az utolsó számított értékkel
                secondOrderDiff.Add(secondOrderDiff[firstOrderDiff.Count - 2]);

                FeatureDescriptor<List<double>> sodFeatureDescriptor = FeatureDescriptor<List<double>>.Descriptor(sodFeatureKey);
                Signature.SetFeature(sodFeatureDescriptor, secondOrderDiff);

                return secondOrderDiff;
            }
        }

        public List<double> GetSineMeasure()
        {
            if (Signature.HasFeature(DerivedSvc2004Features.SineMeasure))
                return Signature.GetFeature(DerivedSvc2004Features.SineMeasure);

            else
            {
                List<double> fodX = GetFirstOrderDifference(DerivedSvc2004Features.FODX);
                List<double> fodY = GetFirstOrderDifference(DerivedSvc2004Features.FODY);

                List<double> sin = new List<double>(fodX.Count);

                for (int i = 0; i < fodX.Count; i++)
                {
                    double fodXiSquared = fodX[i] * fodX[i];
                    double fodYiSquared = fodY[i] * fodY[i];
                    double numerator = Math.Sqrt(fodXiSquared + fodYiSquared);

                    if (numerator == 0)
                        sin.Add(0);
                    else
                        sin.Add(fodY[i] / numerator);
                }

                Signature.SetFeature(DerivedSvc2004Features.SineMeasure, sin);
                return sin;
            }
        }

        public List<double> GetCosineMeasure()
        {
            if (Signature.HasFeature(DerivedSvc2004Features.CosineMeasure))
                return Signature.GetFeature(DerivedSvc2004Features.CosineMeasure);

            else
            {
                List<double> fodX = GetFirstOrderDifference(DerivedSvc2004Features.FODX);
                List<double> fodY = GetFirstOrderDifference(DerivedSvc2004Features.FODY);

                List<double> cos = new List<double>(fodX.Count);

                for (int i = 0; i < fodX.Count; i++)
                {
                    double fodXiSquared = fodX[i] * fodX[i];
                    double fodYiSquared = fodY[i] * fodY[i];
                    double numerator = Math.Sqrt(fodXiSquared + fodYiSquared);

                    if (numerator == 0)
                        cos.Add(0);
                    else
                        cos.Add(fodX[i] / numerator);
                }

                Signature.SetFeature(DerivedSvc2004Features.CosineMeasure, cos);
                return cos;
            }
        }

        public List<double> GetLengthBasedFeatureFirstOrder()
        {
            if (Signature.HasFeature(DerivedSvc2004Features.LengthBasedFO))
                return Signature.GetFeature(DerivedSvc2004Features.LengthBasedFO);

            else
            {
                var x = GetFirstOrderDifference(Features.X);
                var y = GetFirstOrderDifference(Features.Y);
                var lengthBasedFO = GetLengthBasedFeature(x, y);
                Signature.SetFeature(DerivedSvc2004Features.LengthBasedFO, lengthBasedFO);
                return lengthBasedFO;
            }
        }

        public List<double> GetLengthBasedFeatureSecondOrder()
        {
            if (Signature.HasFeature(DerivedSvc2004Features.LengthBasedSO))
                return Signature.GetFeature<List<double>>(DerivedSvc2004Features.LengthBasedSO);

            else
            {
                var x = GetSecondOrderDifference(Features.X);
                var y = GetSecondOrderDifference(Features.Y);
                var lengthBasedSO = GetLengthBasedFeature(x, y);
                Signature.SetFeature(DerivedSvc2004Features.LengthBasedSO, lengthBasedSO);
                return lengthBasedSO;
            }
        }

        private List<double> GetLengthBasedFeature(List<double> x, List<double> y)
        {
            if (x.Count != y.Count)
                throw new ArgumentException("Vectors are not same long");

            List<double> lengthBasedFeature = new List<double>(x.Count);

            for (int i = 0; i < x.Count; i++)
            {
                lengthBasedFeature.Add(Math.Sqrt(x[i] * x[i] + y[i] * y[i]));
            }

            return lengthBasedFeature;
        }

        public List<double> GetVelocities()
        {
            if (Signature.HasFeature(DerivedSvc2004Features.Velocity))
                return Signature.GetFeature(DerivedSvc2004Features.Velocity);

            else
            {
                var x = Signature.GetFeature(Features.X);
                var y = Signature.GetFeature(Features.Y);
                var t = Signature.GetFeature(Features.T);

                List<double> velocity = new List<double>(x.Count);

                for (int i = 0; i < x.Count; i++)
                {
                    if (i < x.Count - SpacingParameter)
                    {

                        double deltaXi = x[i + SpacingParameter] - x[i];
                        double deltaYi = y[i + SpacingParameter] - y[i];
                        double deltaTi = t[i + SpacingParameter] - t[i];
                        if (deltaTi == 0)
                        {
                            if (i > 0)
                                velocity.Add(velocity[i - 1]);
                            else
                                velocity.Add(0);
                        }
                        else
                            velocity.Add(Math.Sqrt(deltaXi * deltaXi + deltaYi * deltaYi) / deltaTi);
                    }
                    //Lista kipótlása az utolsó számított értékkel
                    else
                        velocity.Add(velocity[x.Count - SpacingParameter - 1]);
                }

                Signature.SetFeature(DerivedSvc2004Features.Velocity, velocity);
                return velocity;
            }
        }

        public List<double> GetAccelerations()
        {
            if (Signature.HasFeature(DerivedSvc2004Features.Acceleration))
                return Signature.GetFeature(DerivedSvc2004Features.Acceleration);

            else
            {
                var velocities = GetVelocities();
                var t = Signature.GetFeature(Features.T);

                List<double> acceleartion = new List<double>(velocities.Count);

                for (int i = 0; i < velocities.Count; i++)
                {
                    if (i < velocities.Count - SpacingParameter - 1)
                    {
                        double deltaV = velocities[i + 1] - velocities[i];
                        double deltaT = t[i + SpacingParameter + 1] - t[i + SpacingParameter];
                        if (deltaT == 0)
                        {
                            if (i > 0)
                                acceleartion.Add(acceleartion[i - 1]);
                            else
                                acceleartion.Add(0);
                        }
                        else
                            acceleartion.Add(deltaV / deltaT);
                    }
                    //Lista kipótlása az utolsó számított értékkel
                    else
                        acceleartion.Add(acceleartion[i - 1]);
                }



                Signature.SetFeature(DerivedSvc2004Features.Acceleration, acceleartion);
                return acceleartion;
            }
        }
    }
}

