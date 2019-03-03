using SigStat.Common.Pipeline;
using SigStat.Common.Transforms;
using System;
using System.Collections.Generic;
using System.Text;

namespace SigStat.Common.Transforms
{
    /// <summary>
    /// Sequential pipeline to reset time values to begin at 0.
    /// The following Transforms are called: Extrema, Multiply, AddVector.
    /// <para>Default Pipeline Input: <see cref="Features.T"/></para>
    /// <para>Default Pipeline Output: <see cref="Features.T"/></para>
    /// </summary>
    public class TimeReset : SequentialTransformPipeline
    {
        [Input]
        FeatureDescriptor<List<double>> Input = Features.T;

        [Output("T")]
        FeatureDescriptor<List<double>> Output = Features.T;

        /// <summary>Initializes a new instance of the <see cref="TimeReset"/> class.</summary>
        public TimeReset()
        {
            var negMin = FeatureDescriptor.Get<List<double>>("NegMin");//TODO: ideiglenes dolgokat vhogy torolni
            Items = new List<ITransformation>
            {
                new Extrema(),//find minimum
                new Multiply(-1.0) { Output = negMin },//negate
                new AddVector(negMin) { Inputs =  { Input }  },//add the negated value
            };
        }

    }
}
