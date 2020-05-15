using GeneticAlgorithm.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneticAlgorithmWEB.BLL.Interfaces
{
    public interface IGraphBL
    {
        IEnumerable<GraphInfo> GetAllGraphInfo();
        Graph GetById(int id);
    }
}
