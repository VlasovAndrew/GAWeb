﻿using GeneticAlgorithm.Entities.Requests;
using GeneticAlgorithm.Entities.Users;
using GeneticAlgorithmWEB.BLL;
using GeneticAlgorithmWEB.Encrypt;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net.Security;
using System.Text;
using System.Threading.Tasks;

namespace GeneticAlgorithmWEB.Dao
{
    class UserContextInitializer : CreateDatabaseIfNotExists<UserContext>
    {
        // ������������� ���� ������ ����� �������������.
        protected override void Seed(UserContext context)
        {
            // ���������� � ���� ������ ������������ 
            // � ������� admin � ������� admin
            CreateUserRequest userRequest = new CreateUserRequest() { 
                Login = "admin",
                Password = "admin"
            };
            // ����������� ������ ������������
            Encryption encryption = new Encryption();
            User user = new User() { 
                Login = userRequest.Login,
                Password = encryption.CreatePassword(userRequest.Password),
            };
            // ���������� �����
            context.Users.Add(user);
            context.SaveChanges();
        }
    }
}
