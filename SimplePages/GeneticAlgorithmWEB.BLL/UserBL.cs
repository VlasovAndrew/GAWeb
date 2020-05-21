using GeneticAlgorithm.Entities.Users;
using GeneticAlgorithmWEB.BLL.Interfaces;
using GeneticAlgorithmWEB.Dao;
using System.Linq;
using System.Web.UI;

namespace GeneticAlgorithmWEB.BLL
{
    public class UserBL : IUserBL
    {
        public User Add(User user)
        {
            using (UserContext context = new UserContext()) {
                User res = context.Users.Add(user);
                context.SaveChanges();
                return res;
            }
        }

        public User GetById(int id)
        {
            using (UserContext context = new UserContext()) {
                return context.Users.Where(u => u.Id == id).FirstOrDefault();
            }
        }

        public User GetByName(string name)
        {
            using (UserContext context = new UserContext()) {
                return context.Users.Where(u => u.Login == name).FirstOrDefault();
            }
        }
    }
}
