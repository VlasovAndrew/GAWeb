using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GeneticAlgorithm.Entities.Requests
{
    public class ResearchRequest
    {
        public int GraphId { get; set; }
        public double Pm { get; set; }
        public double Pc { get; set; }
        public int PopulationSize { get; set; }
    }
}