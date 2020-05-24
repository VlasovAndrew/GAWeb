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
        private double _fixedPM = 0.4;
        private double _fixedPC = 0.4;
        private int _fixedPopSize = 30;
        private int _testCount = 10;

        public Algorithm(IGraphBL graphBL)
        {
            _graphBL = graphBL;
        }

        public FindingVertexResponse FindCentralVertex(Graph graph)
        {
            GeneticAlgorithmCore ga = new GeneticAlgorithmCore(graph, _fixedPopSize, _fixedPM, _fixedPC);
            return ga.StartAlgorithm();
        }

        public ResearchAlgorithmResponse ResearchAlgorithm(ResearchRequest param) {
            Graph graph = _graphBL.GetById(param.GraphId);
            double avgTime = 0.0;
            int error = 0;
            GeneticAlgorithmCore ga = new GeneticAlgorithmCore(graph, param.PopulationSize, param.Pm, param.Pc);
            for (int i = 0; i < _testCount; i++) 
            {
                FindingVertexResponse algResult = ga.StartAlgorithm();
                if (algResult.R != graph.R) {
                    error++;
                }
                avgTime += algResult.Time;
            }

            return new ResearchAlgorithmResponse() {
                AvgTime = avgTime / _testCount,
                Error = error / (double)_testCount * 100.0,
            };
        }
    }
}
