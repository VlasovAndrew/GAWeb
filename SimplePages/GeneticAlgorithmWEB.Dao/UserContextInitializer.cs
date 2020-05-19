using GeneticAlgorithm.Entities.Users;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneticAlgorithmWEB.Dao
{
    class UserContextInitializer : CreateDatabaseIfNotExists<UserContext>
    {
        protected override void Seed(UserContext context)
        {
            User user = new User() { 
                Login = "admin",
                Password = "admin"
            };
            context.Users.Add(user);
            context.SaveChanges();
        }
    }
}
