using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneticAlgorithm.Entities
{
    public class Graph
    {
        public int N { get; set; }
        public int M { get; set; }
        public List<Tuple<int, int>> Edges { get; set; }
    }
}
