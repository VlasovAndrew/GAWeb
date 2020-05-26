﻿namespace GeneticAlgorithm.IoC
{
    public class NinjectRegistrations : NinjectModule
    {
        public override void Load()
        {
            // ���������� ����������� � �� ������������.
            Bind<IUserDao>().To<UserDao>();
            Bind<IGraphDao>().To<GraphDao>();
            Bind<IAlgorithm>().To<Algorithm>();
            Bind<IGraphBL>().To<GraphBL>();
            Bind<IUserBL>().To<UserBL>();
        }
    }
}
