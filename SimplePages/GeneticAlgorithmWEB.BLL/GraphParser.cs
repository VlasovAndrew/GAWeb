using GeneticAlgorithm.Entities;
using GeneticAlgorithmWEB.BLL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneticAlgorithmWEB.BLL
{
    public class GraphParser : IGraphParser
    {
        public Graph ParseSimpleTxtFormat(string[] lines)
        {
            int n = 0;
            List<Tuple<int, int>> edges = new List<Tuple<int, int>>();
            foreach (var line in lines)
            {
                int[] vertices = line.Split().Select(v => int.Parse(v)).ToArray();
                if (vertices.Length != 2)
                {
                    throw new FormatException("The edge should be written as a pair of vertices. " +
                        $"The line contains {vertices.Length} items.");
                }
                Tuple<int, int> edge = Tuple.Create(vertices[0], vertices[1]);
                if (edge.Item1 < 0 || edge.Item2 < 0)
                {
                    throw new FormatException("The vertex label must be positive number. " +
                        $"Edge ({edge.Item1}, {edge.Item2}) isnot correct.");
                }
                edges.Add(edge);
            }
            n = CountVerteces(edges);
            return new Graph
            {
                N = n,
                M = edges.Count,
                Edges = edges,
            };
        }

        private int CountVerteces(List<Tuple<int, int>> edges)
        {
            HashSet<int> uniqueLabel = new HashSet<int>();
            foreach (var edge in edges)
            {
                uniqueLabel.Add(edge.Item1);
                uniqueLabel.Add(edge.Item2);
            }
            int minValue = uniqueLabel.Min();
            if (minValue != 0) {
                throw new FormatException("Vertex indexing should start at zero");
            }
            int n = uniqueLabel.Max() + 1;
            if (n != uniqueLabel.Count) {
                throw new FormatException("Vertex labels must go in a row");
            }
            return n;
        }
    }
}
