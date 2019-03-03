using SigStat.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SigStat.WpfSample.Transformation
{
    //Fanni-féle megvalósítás
    internal class CenteringTransform : PipelineBase, ITransformation
    {
        public void Transform(Signature signature)
        {

            double centerX = 0;
            double centerY = 0;
            var xCoordinates = signature.GetFeature(Features.X);
            var yCoordinates = signature.GetFeature(Features.Y);

            for (int i = 1; i < xCoordinates.Count; i++)
            {
                centerX += xCoordinates[i];
                centerY += yCoordinates[i];
            }

            centerX /= xCoordinates.Count - 1;
            centerY /= yCoordinates.Count - 1;

            List<double> newXCoordinates = new List<double>(xCoordinates.Count);
            List<double> newYCoordinates = new List<double>(yCoordinates.Count);

            for (int i = 1; i < xCoordinates.Count; i++)
            {
                newXCoordinates.Add(xCoordinates[i] - centerX + 5000);
                newYCoordinates.Add(yCoordinates[i] - centerY + 5000);
            }

            signature.SetFeature(Features.X, newXCoordinates);
            signature.SetFeature(Features.Y, newYCoordinates);

        }
    }
}
