using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneticAlgorithm.Entities
{
    public class Graph : GraphInfo
    {
        public ICollection<Edge> Edges { get; set; }
        public Graph()
        {
            Edges = new List<Edge>();
        }
    }
}
