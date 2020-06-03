using GeneticAlgorithmWEB.BLL;
using GeneticAlgorithmWEB.BLL.Interfaces;
using GeneticAlgorithmWEB.DAL.Interfaces;
using GeneticAlgorithmWEB.Dao;
using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneticAlgorithm.IoC
{
    public class NinjectRegistrations : NinjectModule
    {
        public override void Load()
        {
            // Связывание интерфейсов с их реализацией.
            Bind<IUserDao>().To<UserDao>();
            Bind<IGraphDao>().To<GraphDao>();
            Bind<IAlgorithm>().To<Algorithm>();
            Bind<IGraphBL>().To<GraphBL>();
            Bind<IUserBL>().To<UserBL>();
        }
    }
}
