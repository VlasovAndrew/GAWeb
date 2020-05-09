using GeneticAlgorithm.Entities;
using GeneticAlgorithmWEB.BLL.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace GeneticAlgorithmWEB.BLL
{
    public class AlgorithmWork : IAlgorithmWork
    {
        [DllImport(@"GeneticCore.dll")]
        public static extern AlgorithmRes startGeneticAlgorithm(int n, int m, IntPtr e, int eSize);

        [DllImport(@"GeneticCore.dll")]
        public static extern IntPtr createArray(int n);

        [StructLayout(LayoutKind.Sequential)]
        unsafe public struct AlgorithmRes
        {
            public int r;
            public double time;
            public int center;
        };

        public AlgorithmResult FindCentralVertex(Graph graph)
        {
            List<int> edgesInLine = new List<int>();
            foreach (var e in graph.Edges)
            {
                edgesInLine.Add(e.Item1);
                edgesInLine.Add(e.Item2);
            }
            
            IntPtr edges = createArray(edgesInLine.Count);  
            Marshal.Copy(edgesInLine.ToArray(), 0, edges, edgesInLine.Count);
            AlgorithmRes res = startGeneticAlgorithm(graph.N, graph.M, edges, edgesInLine.Count);

            return new AlgorithmResult {
                R = res.r,
                Time = res.time,
                Center = res.center,
            };
        }

        public string WorkDir() {
            return Directory.GetCurrentDirectory();
        }
    }
}
