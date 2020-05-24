using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace GeneticAlgorithm.Entities.Requests
{
    public class AddGraphRequest
    {
        public string Name { get; set; }
        public HttpPostedFileBase Upload { get; set; }
    }
}
