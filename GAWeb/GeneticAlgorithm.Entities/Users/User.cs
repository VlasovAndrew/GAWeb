using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneticAlgorithm.Entities.Users
{
    public class User
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public byte[] Password { get; set; }
    }
}
