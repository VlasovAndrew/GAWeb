using GeneticAlgorithm.Entities.Users;
using GeneticAlgorithmWEB.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneticAlgorithmWEB.Dao
{
    // Класс реализующий уровень доступа к данным.
    // В классе определены основные методы для 
    // получения пользователей из базы данных.
    public class UserDao : IUserDao
    {
        // Сохраняет нового пользователя в базе данных.
        public User Add(User user)
        {
            using (UserContext context = new UserContext())
            {
                User res = context.Users.Add(user);
                context.SaveChanges();
                return res;
            }
        }
        // Получение пользователя по Id
        public User GetById(int id)
        {
            using (UserContext context = new UserContext())
            {
                return context.Users.Where(u => u.Id == id).FirstOrDefault();
            }
        }
        // Получение пользователя по логину
        public User GetByLogin(string name)
        {
            using (UserContext context = new UserContext())
            {
                return context.Users.Where(u => u.Login == name).FirstOrDefault();
            }
        }
    }
}
