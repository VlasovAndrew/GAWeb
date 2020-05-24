using GeneticAlgorithm.Entities.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneticAlgorithm.Entities.Response
{
    public class AlgorithmResultResponse
    {
        public ResearchAlgorithmResponse AlgorithmResponse { get; set; }
        public ResearchRequest ResearchRequest { get; set; }
    }
}
