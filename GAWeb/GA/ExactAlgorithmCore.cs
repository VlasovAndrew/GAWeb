using GeneticAlgorithm;
using GeneticAlgorithm.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GA
{
    public class ExactAlgorithmCore
    {
        public int FindRadius(GraphContext graphContext) {
            int R = int.MaxValue;
            for (int v = 0; v < graphContext.N; v++) {
                R = Math.Min(R, graphContext.GetEccentricity(v));
            }
            return R;
        }
    }
}
