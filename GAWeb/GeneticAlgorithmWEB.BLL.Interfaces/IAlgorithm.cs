using GeneticAlgorithm.Entities;
using GeneticAlgorithm.Entities.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneticAlgorithmWEB.BLL.Interfaces
{
    // Интерфейс, описывающий методы для работы с 
    // генетическим алгоритмом на уровне бизнес-логики.
    public interface IAlgorithm
    {
        FindingVertexResponse FindCentralVertex(Graph graph);
        ResearchAlgorithmResponse ResearchAlgorithm(ResearchRequest param);
    }
}
