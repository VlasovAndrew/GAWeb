using GeneticAlgorithm.Entities.Requests;
using GeneticAlgorithm.Entities.Users;
using GeneticAlgorithmWEB.BLL;
using GeneticAlgorithmWEB.Encrypt;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net.Security;
using System.Text;
using System.Threading.Tasks;

namespace GeneticAlgorithmWEB.Dao
{
    class UserContextInitializer : CreateDatabaseIfNotExists<UserContext>
    {
        // Инициализация базы данных одним пользователем.
        protected override void Seed(UserContext context)
        {
            // Сохранение в базе данных пользователя 
            // с паролем admin и логином admin
            CreateUserRequest userRequest = new CreateUserRequest() { 
                Login = "admin",
                Password = "admin"
            };
            // Хеширование пароля пользователя
            Encryption encryption = new Encryption();
            User user = new User() { 
                Login = userRequest.Login,
                Password = encryption.CreatePassword(userRequest.Password),
            };
            // Сохранение графа
            context.Users.Add(user);
            context.SaveChanges();
        }
    }
}
