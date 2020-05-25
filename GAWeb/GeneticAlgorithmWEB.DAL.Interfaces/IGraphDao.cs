using GeneticAlgorithm.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneticAlgorithmWEB.DAL.Interfaces
{
    // Интерфейс, описывающий методы уровня доступа к графам.
    public interface IGraphDao
    {
        IEnumerable<GraphInfo> GetAllGraphInfo();
        Graph GetById(int id);
        Graph Add(Graph graph);
    }
}
