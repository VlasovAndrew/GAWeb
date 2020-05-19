using GeneticAlgorithm.Entities;
using GeneticAlgorithmWEB.BLL.Interfaces;
using GeneticAlgorithmWEB.Dao;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net.Http.Headers;

namespace GeneticAlgorithmWEB.BLL
{
    public class GraphBL : IGraphBL
    {
        public Graph Add(Graph graph)
        {
            using (GraphContext context = new GraphContext()) {
                Graph res = context.Graphs.Add(graph);
                context.SaveChanges();
                return res;
            }
        }

        public IEnumerable<GraphInfo> GetAllGraphInfo()
        {
            List<GraphInfo> res = new List<GraphInfo>();
            using (GraphContext context = new GraphContext()) {
                foreach (var graph in context.Graphs)
                {
                    res.Add(new GraphInfo() { 
                        Id = graph.Id,
                        N = graph.N,
                        M = graph.M,
                        Name = graph.Name,
                    });
                }
                return res;
            }
        }

        public Graph GetById(int id) {
            using (GraphContext context = new GraphContext()) {
                Graph res = context.Graphs
                    .Where(g => g.Id == id)
                    .Include(g => g.Edges)
                    .FirstOrDefault();
                if (res == null) {
                    throw new ArgumentException($"Invalid graph id = {id}");
                }
                return res;
            }    
        }
    }
}
