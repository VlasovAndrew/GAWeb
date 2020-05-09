using GeneticAlgorithm;
using GeneticAlgorithm.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace GeneticAlgorithmWEB.Tests
{
    [TestClass]
    public class GraphContextTests
    {
        private Graph CreateChain(int n) {
            List<Tuple<int, int>> edges = new List<Tuple<int, int>>();
            for (int i = 0; i + 1 < n; i++) {
                edges.Add(Tuple.Create(i, i + 1));
            }
            return new Graph {
                N = n,
                M = edges.Count,
                Edges = edges,
            };
        }

        private Graph CreateFullGraph(int n) {
            List<Tuple<int, int>> edges = new List<Tuple<int, int>>();
            for (int i = 0; i < n; i++) {
                for (int j = i + 1; j < n; j++) {
                    edges.Add(Tuple.Create(i, j));
                }
            }
            return new Graph() {
                N = n,
                M = edges.Count,
                Edges = edges,
            };
        }

        [TestMethod]
        public void ChainTest() {
            int n = 10;
            int m = n - 1;
            int start = 0, end = n - 1;
            GraphContext graphContext = new GraphContext(CreateChain(n));
            Assert.AreEqual(graphContext.Distance(start, end), m);
            Assert.AreEqual(graphContext.Distance(n - 1, start), m);
        }

        [TestMethod]
        public void FullGraphTest() {
            int n = 10;
            GraphContext graphContext = new GraphContext(CreateFullGraph(n));
            for (int i = 0; i < n; i++) {
                for (int j = 0; j < n; j++) {
                    if (i == j)
                    {
                        Assert.AreEqual(graphContext.Distance(i, j), 0);
                    }
                    else 
                    {
                        Assert.AreEqual(graphContext.Distance(i, j), 1);
                    }
                }
            }
        }

        [TestMethod]
        public void EccentricityTest() {
            int n = 100;
            GraphContext graphContext = new GraphContext(CreateFullGraph(n));
            for (int i = 0; i < n; i++) {
                Assert.AreEqual(graphContext.GetEccentricity(i), 1);
            }
        }
    }
}
