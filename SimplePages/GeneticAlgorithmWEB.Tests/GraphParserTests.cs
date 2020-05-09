using System;
using GeneticAlgorithm.Entities;
using GeneticAlgorithmWEB.BLL;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GeneticAlgorithmWEB.Tests
{
    [TestClass]
    public class GraphParserTests
    {
        private GraphParser _parser = new GraphParser();

        [TestMethod]
        public void CorrectGraphTest1()
        {
            string[] textGraph = new string[] {
                "0 1",
                "1 2",
            };
            Graph graph = _parser.ParseSimpleTxtFormat(textGraph);
            Assert.AreEqual(graph.N, 3);
            Assert.AreEqual(graph.M, 2);
        }

        [TestMethod]
        public void CorrectGraphTest2()
        {
            string[] textGraph = new string[] {
                "0 1",
                "1 2",
                "0 2",
            };
            Graph graph = _parser.ParseSimpleTxtFormat(textGraph);
            Assert.AreEqual(graph.N, 3);
            Assert.AreEqual(graph.M, 3);
        }

        [TestMethod]
        [ExpectedException(typeof(FormatException))]
        public void InvalidIndexing() {
            string[] textGraph = new string[] { 
                "1 2",
                "2 3",
            };
            _parser.ParseSimpleTxtFormat(textGraph);
        }

        [TestMethod]
        [ExpectedException(typeof(FormatException))]
        public void InvalidValueInFileTest()
        {
            string[] textGraph = new string[] {
                "0 a",
                "1 2",
                "0 2",
            };
            Graph graph = _parser.ParseSimpleTxtFormat(textGraph);
        }

        [TestMethod]
        [ExpectedException(typeof(FormatException))]
        public void InvalidVertexNumberInLineTest1()
        {
            string[] textGraph = new string[] {
                "1 2",
                "2 3",
                "1 3 4",
            };
            Graph graph = _parser.ParseSimpleTxtFormat(textGraph);
        }

        [TestMethod]
        [ExpectedException(typeof(FormatException))]
        public void InvalidVertexNumberInLineTest2()
        {
            string[] textGraph = new string[] {
                "1 2",
                "2 3",
                "1 ",
            };
            Graph graph = _parser.ParseSimpleTxtFormat(textGraph);
        }

        [TestMethod]
        [ExpectedException(typeof(FormatException))]
        public void InvalidVertexLabelTest()
        {
            string[] textGraph = new string[] {
                "1 2",
                "2 3",
                "1 5",
            };
            Graph graph = _parser.ParseSimpleTxtFormat(textGraph);
        }


    }
}
