using GeneticAlgorithm.Entities.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneticAlgorithmWEB.BLL.Interfaces
{
    // Интерфейс, описывающий методы для работы с 
    // пользователями на уровне бизнес-логики.
    public interface IUserBL
    {
        void Add(CreateUserRequest user);
        bool CheckPassword(LoginUserRequest user);
        bool UserExists(CreateUserRequest user);
    }
}
