using GeneticAlgorithm.Entities;
using GeneticAlgorithmWEB.BLL;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneticAlgorithmWEB.Tests
{
    [TestClass]
    public class GraphBLTests
    {
        [TestMethod]
        public void SimpleTest() {
            GraphBL graphBL = new GraphBL();
            Graph graph = graphBL.GetById(1);
            Assert.AreEqual(graph.N, 3);
            Assert.AreEqual(graph.M, 3);
        }
    }
}
