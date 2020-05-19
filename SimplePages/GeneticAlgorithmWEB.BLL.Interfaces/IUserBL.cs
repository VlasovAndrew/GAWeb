using GeneticAlgorithm.Entities.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneticAlgorithmWEB.BLL.Interfaces
{
    public interface IUserBL
    {
        User GetById(int id);
        User GetByName(string name);
        User Add(User user);
    }
}
