using GeneticAlgorithm;
using GeneticAlgorithm.Entities;
using RandomModule;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace GA
{
    public class GeneticAlgorithmCore
    {
        private Graph _graph;
        private int _populationSize;
        private double _pc, _pm;
        private int _step;

        private Stopwatch _watch;
        private List<int> _population;
        private Random _rnd;
        private RandomWorker _rndWorker;

        public GeneticAlgorithmCore(Graph graph, int populationSize, double pm, double pc)
        {
            _graph = graph;
            _step = 20;
            _pm = pm;
            _pc = pc;

            _watch = new Stopwatch();
            _rnd = new Random();
            _rndWorker = new RandomWorker();
        }

        public double StartAlgorithm() {
            GraphContext graphContext = new GraphContext(_graph);
            InitPopulation();
            _watch.Start();
            for (int i = 0; i < _step; i++)
            {
                EvolutionStep(graphContext);
            }
            _watch.Stop();
            return _watch.ElapsedMilliseconds;
        }

        private void InitPopulation() {
            _population = new List<int>(_populationSize);
            for (int i = 0; i < _population.Count; i++) {
                _population[i] = _rnd.Next(_graph.N);
            }
        }

        private void EvolutionStep(GraphContext graphContext) {
            Crossing(graphContext);
            Mutation(graphContext);
            Selection(graphContext);
        }

        private void Mutation(GraphContext graphContext) {
            for (int i = 0; i < _population.Count; i++)
            {
                int v = _population[i];
                double condition = _rnd.NextDouble();
                if (condition < _pm) {
                    int[] neighbors = graphContext.GetNeighbors(v);
                    int index = _rnd.Next(neighbors.Length);
                    _population[i] = neighbors[index];
                }
            }
        }

        private void Crossing(GraphContext graphContext) {
            List<int> crossed = new List<int>();
            for (int i = 0; i < _population.Count; i++) {
                if (_rnd.NextDouble() <= _pc) {
                    int ind1 = _rnd.Next(_population.Count);
                    int ind2 = _rnd.Next(_population.Count);
                    int x = _population[ind1], y = _population[ind2];
                    int[] path = graphContext.GetPath(x, y);
                    crossed.Add(path[path.Length / 2]);
                }
            }
            _population.AddRange(crossed);
        }

        private void Selection(GraphContext graphContext)
        {
            List<int> e = new List<int>();
            foreach (var v in _population)
            {
                e.Add(graphContext.GetEccentricity(v));
            }
            double[] prob = _rndWorker.InvertProb(e.ToArray());
            List<int> selectedPopulation = new List<int>();
            while (selectedPopulation.Count < _populationSize) {
                selectedPopulation.Add(_rndWorker.Choice(_population.ToArray(), prob));
            }
            _population = selectedPopulation;   
        }
    }
}
