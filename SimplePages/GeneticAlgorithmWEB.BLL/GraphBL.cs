using GeneticAlgorithm.Entities;
using GeneticAlgorithmWEB.BLL.Interfaces;
using GeneticAlgorithmWEB.Dao;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace GeneticAlgorithmWEB.BLL
{
    public class GraphBL : IGraphBL
    {
        public IEnumerable<GraphInfo> GetAllGraphInfo()
        {
            List<GraphInfo> res = new List<GraphInfo>();
            using (GraphContextDB context = new GraphContextDB()) {
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
            using (GraphContextDB context = new GraphContextDB()) {
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
