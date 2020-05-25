using GeneticAlgorithm.Entities;
using GeneticAlgorithmWEB.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace GeneticAlgorithmWEB.Dao
{
    // Класс реализующий уровень доступа к данным.
    // В классе определены основные методы для 
    // получения графов из базы данных.
    public class GraphDao : IGraphDao
    {
        // Сохраняет новый граф в базе данных.
        public Graph Add(Graph graph)
        {
            using (GraphContext context = new GraphContext())
            {
                Graph res = context.Graphs.Add(graph);
                context.SaveChanges();
                return res;
            }
        }

        // Получение описательной информации обо всех графах в базе данных.
        public IEnumerable<GraphInfo> GetAllGraphInfo()
        {
            List<GraphInfo> res = new List<GraphInfo>();
            using (GraphContext context = new GraphContext())
            {
                foreach (var graph in context.Graphs)
                {
                    res.Add(new GraphInfo()
                    {
                        Id = graph.Id,
                        N = graph.N,
                        M = graph.M,
                        Name = graph.Name,
                    });
                }
                return res;
            }
        }

        // Получение графа по Id.
        // Выбрасывает исключение, 
        // если граф с переданным Id не удалось найти в БД.
        public Graph GetById(int id)
        {
            using (GraphContext context = new GraphContext())
            {
                // Поиск графа при помощи LINQ запроса к контексту с графами.
                Graph res = context.Graphs
                    .Where(g => g.Id == id)
                    .Include(g => g.Edges)
                    .FirstOrDefault();
                if (res == null)
                {
                    throw new ArgumentException($"Invalid graph id = {id}");
                }
                return res;
            }
        }
    }
}
