﻿namespace GeneticAlgorithmWEB.Dao
{
    // ����� ����������� ������� ������� � ������.
    // � ������ ���������� �������� ������ ��� 
    // ��������� ������������� �� ���� ������.
    public class UserDao : IUserDao
    {
        // ��������� ������ ������������ � ���� ������.
        public User Add(User user)
        {
            using (UserContext context = new UserContext())
            {
                User res = context.Users.Add(user);
                context.SaveChanges();
                return res;
            }
        }
        // ��������� ������������ �� Id
        public User GetById(int id)
        {
            using (UserContext context = new UserContext())
            {
                return context.Users.Where(u => u.Id == id).FirstOrDefault();
            }
        }
        // ��������� ������������ �� ������
        public User GetByLogin(string name)
        {
            using (UserContext context = new UserContext())
            {
                return context.Users.Where(u => u.Login == name).FirstOrDefault();
            }
        }
    }
}
