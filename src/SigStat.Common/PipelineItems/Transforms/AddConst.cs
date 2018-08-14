using System;
using System.Collections.Generic;
using System.Text;

namespace SigStat.Common.Transforms
{
    /// <summary>
    /// Default output: the input
    /// </summary>
    public class AddConst : PipelineBase, ITransformation
    {
        private readonly double addValue;

        public AddConst(double value)
        {
            this.addValue = value;
        }

        public void Transform(Signature signature)
        {
            //default output is the input
            if (OutputFeatures == null)
                OutputFeatures = InputFeatures;

            if (InputFeatures[0].IsCollection)//we must treat this separately
            {
                List<double> values = signature.GetFeature<List<double>>(InputFeatures[0]);
                for (int i = 0; i < values.Count; i++)
                {
                    values[i] = values[i] + addValue;
                    Progress += 100 / values.Count;
                }
                signature.SetFeature(OutputFeatures[0], values);
            }
            else
            {
                double value = signature.GetFeature<double>(InputFeatures[0]);
                value = value + addValue;
                signature.SetFeature(OutputFeatures[0], value);
            }

            Progress = 100;
        }
    }
}
