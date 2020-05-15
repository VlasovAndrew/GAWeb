using GA;
using GeneticAlgorithm.Entities;
using GeneticAlgorithmWEB.BLL;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneticAlgorithmWEB.Tests
{
    [TestClass]
    public class GeneticAlgorithmTests
    {
        [TestMethod]
        public void GeneticAlgorithmTets() {
            string[] lines = File.ReadAllLines("BarabasiAlbertGraph1_M2.txt");
            GraphParser graphParser = new GraphParser();
            Graph graph = graphParser.ParseSimpleTxtFormat(lines);
            GeneticAlgorithmCore ga = new GeneticAlgorithmCore(graph, 20, 0.4, 0.4);
            FindingVertexResponse result = ga.StartAlgorithm();
            Console.WriteLine(result.R);
        }
    }
}
