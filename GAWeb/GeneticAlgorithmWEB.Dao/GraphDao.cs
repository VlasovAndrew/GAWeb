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
                // добавление нового графа с помощью контекста
                Graph res = context.Graphs.Add(graph);
                // сохранение изменений
                context.SaveChanges();
                // возврат графа с заполненным Id
                return res;
            }
        }

        // Получение описательной информации обо всех графах в базе данных.
        public IEnumerable<GraphInfo> GetAllGraphInfo()
        {
            // Список для информации о графах
            List<GraphInfo> res = new List<GraphInfo>();
            using (GraphContext context = new GraphContext())
            {
                // Получение основной информации о графах
                foreach (var graph in context.Graphs)
                {
                    // добавление нового графа
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
                // поиск по совапдающему Id, после чего явно включается набор вершин в граф
                Graph res = context.Graphs
                    .Where(g => g.Id == id)
                    .Include(g => g.Edges)
                    .FirstOrDefault();
                // если не найден граф, то выбрасывается исключение 
                if (res == null)
                {
                    throw new ArgumentException($"Invalid graph id = {id}");
                }
                return res;
            }
        }
    }
}
