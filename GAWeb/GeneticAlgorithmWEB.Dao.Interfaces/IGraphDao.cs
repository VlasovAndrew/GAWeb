using GeneticAlgorithm.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneticAlgorithmWEB.Dao.Interfaces
{
    public interface IGraphDao
    {
        Gra GetById(int id);
    }
}
