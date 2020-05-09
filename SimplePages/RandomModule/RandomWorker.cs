using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RandomModule
{
    public class RandomWorker
    {
        private Random _rnd;
        public RandomWorker()
        {
            _rnd = new Random();
        }

        public T Choice<T>(T[] items, double[] prob) {
            double randomValue = _rnd.NextDouble();
            double probSum = 0.0;
            for (int i = 0; i < prob.Length; i++)
            {
                probSum += prob[i];
                if (probSum >= randomValue)
                {
                    return items[i];
                }
            }
            return items[items.Length - 1];
        }

        public double[] InvertProb(int[] items) {
            int maxValue = items.Max();
            List<double> prob = new List<double>();
            List<double> maxPersent = new List<double>(items.Select(x => (double)maxValue / x));
            double sum = maxPersent.Sum();
            foreach (var item in maxPersent)
            {
                prob.Add(item / sum);
            }
            return prob.ToArray();
        }
    }
}
