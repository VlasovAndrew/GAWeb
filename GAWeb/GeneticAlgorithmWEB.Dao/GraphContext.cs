using GeneticAlgorithm.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneticAlgorithmWEB.Dao
{
    public class GraphContext : DbContext
    {
        static GraphContext() {
            Database.SetInitializer(new GraphContextInitializer());
        }

        public GraphContext() : base("DB") { }

        public DbSet<Graph> Graphs { get; set; }
    }
}
