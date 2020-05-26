﻿namespace GeneticAlgorithmWEB.Dao
{
    public class UserContext : DbContext
    {
        // ����� �������� �� �������������� 
        // ������ � ��������� �������� �������������.
        static UserContext() {
            // ��������� ������, �� ���� �������� ���������� 
            // ��������� ������������� ���� ������ ��������������.
            Database.SetInitializer(new UserContextInitializer());
        }
        public UserContext() : base("DB") {}
        // ����� ������ �� ���� ������.
        public DbSet<User> Users { get; set; }
    }
}
