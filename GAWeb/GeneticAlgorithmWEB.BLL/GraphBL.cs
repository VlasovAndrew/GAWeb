using GA;
using GeneticAlgorithm;
using GeneticAlgorithm.Entities;
using GeneticAlgorithmWEB.BLL.Interfaces;
using GeneticAlgorithmWEB.DAL.Interfaces;
using System;
using System.Collections.Generic;

namespace GeneticAlgorithmWEB.BLL
{
    // Уровень бизнес-логики.
    // Реализут работу с графами.
    public class GraphBL : IGraphBL
    {
        private readonly IGraphDao _graphDao;
        private readonly int _maxN = 2500;

        public GraphBL(IGraphDao graphDao)
        {
            _graphDao = graphDao;
        }

        // Добавление графа в базу данных.
        // Выбрасывается исключение, если граф превышает максимальные размеры или
        // не является связным.
        public Graph Add(Graph graph)
        {
            // Проверка графа на максимальный размер.
            if (graph.N > _maxN) {
                throw new FormatException($"Количество вершин в графе должно быть меньше, чем {_maxN}");
            }            
            GraphContext context = new GraphContext(graph);
            // Проверка на связность.
            if (!context.CheckConnectivity())
            {
                throw new FormatException("Граф должен быть связаным");
            }
            ExactAlgorithmCore exactAlgorithm = new ExactAlgorithmCore();
            int R = exactAlgorithm.FindRadius(context);
            graph.R = R;
            return _graphDao.Add(graph);
        }

        // Получение описания всей информации о графах. 
        public IEnumerable<GraphInfo> GetAllGraphInfo()
        {
            return _graphDao.GetAllGraphInfo();
        }

        public Graph GetById(int id) {
            return _graphDao.GetById(id);
        }
    }
}
