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
    public class GraphContextDB : DbContext
    {
        static GraphContextDB() {
            Database.SetInitializer(new ContextInitializer());
        }

        public GraphContextDB() : base("GraphConnectionDB") { }

        public DbSet<Graph> Graphs { get; set; }
    }
}
