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
        FindingVertexResponse FindCentralVertex(Graph graph);
        ResearchAlgorithmResponse ResearchAlgorithm(ResearchRequest param);
        void AddGraph(Graph graph);
    }
}
