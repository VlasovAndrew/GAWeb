using GeneticAlgorithm.Entities;
using GeneticAlgorithm.Entities.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneticAlgorithmWEB.BLL.Interfaces
{
    public interface IAlgorithm
    {
        FindingVertexResponse FindCentralVertex(Graph graph);
        ResearchAlgorithmResponse ResearchAlgorithm(ResearchRequest param);
    }
}
