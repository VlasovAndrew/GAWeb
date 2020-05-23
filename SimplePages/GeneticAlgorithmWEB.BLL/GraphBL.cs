using GA;
using GeneticAlgorithm;
using GeneticAlgorithm.Entities;
using GeneticAlgorithmWEB.BLL.Interfaces;
using GeneticAlgorithmWEB.DAL.Interfaces;
using System;
using System.Collections.Generic;

namespace GeneticAlgorithmWEB.BLL
{
    public class GraphBL : IGraphBL
    {
        private readonly IGraphDao _graphDao;
        private readonly int _maxN = 2500;

        public GraphBL(IGraphDao graphDao)
        {
            _graphDao = graphDao;
        }

        public Graph Add(Graph graph)
        {
            if (graph.N > _maxN) {
                throw new FormatException($"Количество вершин в графе должно быть меньше, чем {_maxN}");
            }            
            GraphContext context = new GraphContext(graph);
            if (!context.CheckConnectivity())
            {
                throw new FormatException("Граф должен быть связаным");
            }
            ExactAlgorithmCore exactAlgorithm = new ExactAlgorithmCore();
            int R = exactAlgorithm.FindRadius(context);
            graph.R = R;
            return _graphDao.Add(graph);
        }

        public IEnumerable<GraphInfo> GetAllGraphInfo()
        {
            return _graphDao.GetAllGraphInfo();
        }

        public Graph GetById(int id) {
            return _graphDao.GetById(id);
        }
    }
}
