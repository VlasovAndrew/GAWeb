namespace GeneticAlgorithmWEB.Dao
{
    class UserContextInitializer : CreateDatabaseIfNotExists<UserContext>
    {
        // ������������� ���� ������ ����� �������������.
        protected override void Seed(UserContext context)
        {
            CreateUserRequest userRequest = new CreateUserRequest() { 
                Login = "admin",
                Password = "admin"
            };
            // �������� ������ ������������
            Encryption encryption = new Encryption();
            User user = new User() { 
                Login = userRequest.Login,
                Password = encryption.CreatePassword(userRequest.Password),
            };
            context.Users.Add(user);
            context.SaveChanges();
        }
    }
}
