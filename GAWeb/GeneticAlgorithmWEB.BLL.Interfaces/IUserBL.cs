using GeneticAlgorithm.Entities.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneticAlgorithmWEB.BLL.Interfaces
{
    public interface IUserBL
    {
        void Add(CreateUserRequest user);
        bool CheckPassword(LoginUserRequest user);
        bool UserExists(CreateUserRequest user);
    }
}
