using GA;
using GeneticAlgorithm;
using GeneticAlgorithm.Entities;
using GeneticAlgorithmWEB.BLL.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GeneticAlgorithm.Entities.Requests;

namespace GeneticAlgorithmWEB.BLL
{
    public class Algorithm : IAlgorithm
    {
        private readonly IGraphBL _graphBL;
        // Фиксированные параметры для работы генетического алгоритма.
        private double _fixedPM = 0.4;
        private double _fixedPC = 0.4;
        private int _fixedPopSize = 30;
        // Число запусков при тестировании алгоритма с заданными параметрами.
        private int _testCount = 10;

        public Algorithm(IGraphBL graphBL)
        {
            _graphBL = graphBL;
        }
        // Поиск центральных вершин при помощи генетического алгоритма.
        // Алгоритм запускается с фиксированными параметрами мутации, скрещивания и размер популяции.
        public FindingVertexResponse FindCentralVertex(Graph graph)
        {
            // поиск центральных вершин при помощи генетического алгоритма с заданными параметрами
            GeneticAlgorithmCore ga = new GeneticAlgorithmCore(graph, _fixedPopSize, _fixedPM, _fixedPC);
            return ga.StartAlgorithm();
        }
        // Иссдедование алгоритма с переданными параметрами.
        public ResearchAlgorithmResponse ResearchAlgorithm(ResearchRequest param) {
            Graph graph = _graphBL.GetById(param.GraphId);
            double avgTime = 0.0;
            int error = 0;
            // Инициализация алгоритма с переданными параметрами.
            GeneticAlgorithmCore ga = new GeneticAlgorithmCore(graph, param.PopulationSize, param.Pm, param.Pc);
            // Многократный запуск алгоритма для оценки времени работы и процента ошибок.
            for (int i = 0; i < _testCount; i++) 
            {
                // Запуск алгоритма и получение результатов 
                FindingVertexResponse algResult = ga.StartAlgorithm();
                // проверка на верность найденного решения
                if (algResult.R != graph.R) {
                    error++;
                }
                // увеличение суммарного времени работы 
                avgTime += algResult.Time;
            }
            // возвращение результата в виде среднего 
            // значения времени работы и процента неверных ответов
            return new ResearchAlgorithmResponse() {
                AvgTime = avgTime / _testCount,
                Error = error / (double)_testCount * 100.0,
            };
        }
    }
}
