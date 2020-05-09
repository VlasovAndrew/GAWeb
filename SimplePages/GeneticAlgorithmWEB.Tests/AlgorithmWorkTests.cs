using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GeneticAlgorithm.Entities;
using GeneticAlgorithmWEB.BLL;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GeneticAlgorithmWEB.Tests
{
    [TestClass]
    public class AlgorithmWorkTests
    {
        Graph CreateSimpleGraph() {
            List<Tuple<int, int>> edges = new List<Tuple<int, int>>();
            edges.Add(Tuple.Create(0, 1));
            return new Graph
            {
                N = 2,
                M = 1,
                Edges = edges,
            };
        }

        public void FindCentralVertexTest() {
            AlgorithmWork algorithmWork = new AlgorithmWork();
            AlgorithmResult algorithmRes = algorithmWork.FindCentralVertex(CreateSimpleGraph());
            Assert.AreEqual(algorithmRes.R, 1);
        }
    }
}
