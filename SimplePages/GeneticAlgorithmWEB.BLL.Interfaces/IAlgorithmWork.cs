using GeneticAlgorithm.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneticAlgorithmWEB.BLL.Interfaces
{
    public interface IAlgorithmWork
    {
        AlgorithmResult FindCentralVertex(Graph graph);
        string WorkDir();
    }
}
