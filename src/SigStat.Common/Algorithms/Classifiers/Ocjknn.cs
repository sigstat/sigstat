using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace SigStat.Common.Algorithms.Classifiers
{
    /// <summary>
    /// One Class JKNN classifier based on: Khan, Shehroz Saeed. "Kernels for one-class nearest neighbour classification and comparison of chemical spectral data." College of Engineering and Informatics, National University of Ireland (2010).
    /// https://cs.uwaterloo.ca/~s255khan/files/Kernels_for_One-Class_Nearest_Neighbour_Classification_and_Comparison_of_Chemical_Spectral_Data-libre.pdf
    /// </summary>
    public static class Ocjknn
    {
        private struct Neighbor<Key> 
        {
            public Key Label;
            public int Index;
            public double Distance;
            public double ReferenceDistance;
          

        }

        /// <summary>
        /// Step 1: find the <paramref name="j"/> nearest neighbors of <paramref name="testItem"/> in the set of <paramref name="targetItems"/>. 
        /// Step 2: for each neighbor, if (distance from test) / (average distance from <paramref name="k"/> nearest neighbors) &lt; <paramref name="threshold"/> accept++
        /// Steo 3: return accept / <paramref name="j"/>
        /// </summary>
        /// <typeparam name="T">Item type (typically a vector or a label, that  <paramref name="distanceFunction"/> can work with)</typeparam>
        /// <param name="testItem">The item, that we want to classify</param>
        /// <param name="targetItems">Items belonging to the target class</param>
        /// <param name="j">See algorithm description for details</param>
        /// <param name="k">See algorithm description for details</param>
        /// <param name="threshold">See algorithm description for details</param>
        /// <param name="distanceFunction">Calculates the distance between two items of type <typeparamref name="T"/></param>
        /// <returns>If the result is 0.5 or greater, then <paramref name="testItem"/> should be accepted as a member of target class</returns>
        public static double Test<T>(T testItem, IEnumerable<T> targetItems, int j, int k, double threshold, Func<T, T, double> distanceFunction)
        {

            var trainingSet = targetItems.ToArray();
            if (j > trainingSet.Length)
                throw new ArgumentException($"{nameof(j)} must not be greater than {nameof(targetItems)}.Count()", nameof(j));

            if (k > trainingSet.Length - 1)
                throw new ArgumentException($"{nameof(k)} must not be greater than {nameof(targetItems)}.Count()-1", nameof(k));

            var neighborsOfA = new Neighbor<T>[trainingSet.Length];
            for (int i = 0; i < trainingSet.Length; i++)
            {
                neighborsOfA[i] = new Neighbor<T> { Label = trainingSet[i], Index = i, Distance = distanceFunction(testItem, trainingSet[i]) };           
            }

            var jNearestNeighborsOfA = neighborsOfA.OrderBy(g => g.Distance).Take(j).ToArray();

            for (int i = 0; i < jNearestNeighborsOfA.Length; i++)
            {
                var neighborsOfB = trainingSet.Where((t,ind) => ind!=jNearestNeighborsOfA[i].Index);
                var kNearestNeighborsOfB = neighborsOfB.Select(b => distanceFunction(b, jNearestNeighborsOfA[i].Label)).OrderBy(d => d).Take(k);
                jNearestNeighborsOfA[i].ReferenceDistance = kNearestNeighborsOfB.Average();
            }

            double accept = 0;

            foreach (var b in jNearestNeighborsOfA)
            {
                if (b.Distance / b.ReferenceDistance < threshold)
                    accept += 1;
            }
            return accept / j;
        }
    }
}
