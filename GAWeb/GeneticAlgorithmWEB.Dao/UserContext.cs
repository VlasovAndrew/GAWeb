using GeneticAlgorithm.Entities.Users;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneticAlgorithmWEB.Dao
{
    public class UserContext : DbContext
    {
        static UserContext() {
            Database.SetInitializer(new UserContextInitializer());
        }

        public UserContext() : base("DB") {}

        public DbSet<User> Users { get; set; }
    }
}
