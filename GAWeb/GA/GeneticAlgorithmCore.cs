﻿using GeneticAlgorithm;
using GeneticAlgorithm.Entities;
using RandomModule;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace GA
{
    // Основной класс с реализацией генетического алгоритма.
    public class GeneticAlgorithmCore
    {
        // Ссылка на объект с графом.
        private Graph _graph;
        // Размер популяции.
        private int _populationSize;
        // Параметры для вероятности скрещивания и мутации.
        private double _pc, _pm;
        // Число итераций генетического алгоритма.
        private int _step;
        
        private Stopwatch _watch;

        private List<int> _population;
        // Обертка над графом в виде контектса.
        private GraphContext _graphContext;
        // Объект-декоратор для работы с генирацией случайных значений.
        private RandomWorker _rndWorker;

        public GeneticAlgorithmCore(Graph graph, int populationSize, double pm, double pc)
        {
            _step = 20;
            _graph = graph;
            _pm = pm;
            _pc = pc;
            _populationSize = populationSize;

            _watch = new Stopwatch();
            _rndWorker = new RandomWorker();
        }
        // Метод для старта алгоритма.
        public FindingVertexResponse StartAlgorithm() {
            // Начальная инициализация
            Init();
            // Запуск измерения времени работы.
            _watch.Start();
            for (int i = 0; i < _step; i++)
            {
                EvolutionStep();
            }
            // Окончание измерений.
            _watch.Stop();

            FindingVertexResponse res = new FindingVertexResponse() {
                Time = _watch.ElapsedMilliseconds / (double)1000,  
            };
            GetBestResult(res);
            return res;
        }
        // Начальная инициализация параметров перед стартом алгоритма.
        private void Init() {
            _graphContext = new GraphContext(_graph);
            _population = new List<int>();
            for (int i = 0; i < _populationSize; i++) {
                _population.Add(_rndWorker.NextInt(_graph.N));
            }
        }

        private void EvolutionStep() {
            Crossing();
            Mutation();
            Selection();
        }
        // Этап мутации.
        private void Mutation() {
            for (int i = 0; i < _population.Count; i++)
            {
                int v = _population[i];
                double condition = _rndWorker.NextDouble();
                if (condition < _pm) {
                    int[] neighbors = _graphContext.GetNeighbors(v);
                    int index = _rndWorker.NextInt(neighbors.Length);
                    _population[i] = neighbors[index];
                }
            }
        }
        // Этап скрещивания.
        private void Crossing() {
            List<int> crossed = new List<int>();
            for (int i = 0; i < _population.Count; i++) {
                if (_rndWorker.NextDouble() <= _pc) {
                    int ind1 = _rndWorker.NextInt(_population.Count);
                    int ind2 = _rndWorker.NextInt(_population.Count);
                    int x = _population[ind1], y = _population[ind2];
                    int[] path = _graphContext.GetPath(x, y);
                    crossed.Add(path[path.Length / 2]);
                }
            }
            _population.AddRange(crossed);
        }
        // Этап естественного отбора.
        private void Selection()
        {
            List<int> e = new List<int>();
            foreach (var v in _population)
            {
                e.Add(_graphContext.GetEccentricity(v));
            }
            double[] prob = _rndWorker.InvertProb(e.ToArray());
            List<int> selectedPopulation = new List<int>();
            while (selectedPopulation.Count < _populationSize) {
                selectedPopulation.Add(_rndWorker.Choice(_population.ToArray(), prob));
            }
            _population = selectedPopulation;   
        }
        // Поиск минимального эксцентриситета - центральных вершин, 
        // и радиуса графа.
        private void GetBestResult(FindingVertexResponse res) {
            List<int> e = new List<int>();
            foreach (var v in _population)
            {
                e.Add(_graphContext.GetEccentricity(v));
            }
            int R = e.Min();
            HashSet<int> resVertex = new HashSet<int>();
            for (int i = 0; i < e.Count; i++) {
                if (e[i] == R) {
                    resVertex.Add(_population[i]);
                }
            }
            res.Center = resVertex.ToArray();
            res.R = R;
        }
    }
}
