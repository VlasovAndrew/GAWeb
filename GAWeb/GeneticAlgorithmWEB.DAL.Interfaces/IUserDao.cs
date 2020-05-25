using GeneticAlgorithm.Entities.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneticAlgorithmWEB.DAL.Interfaces
{
    // Интерфейс, описывающий методы 
    // уровня доступа к пользователям.
    public interface IUserDao
    {
        User GetById(int id);
        User GetByLogin(string name);
        User Add(User user);
    }
}
