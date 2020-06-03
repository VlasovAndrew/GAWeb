using GeneticAlgorithm.Entities.Users;
using GeneticAlgorithmWEB.BLL.Interfaces;
using GeneticAlgorithmWEB.DAL.Interfaces;
using GeneticAlgorithm.Entities.Requests;
using GeneticAlgorithmWEB.Encrypt;

namespace GeneticAlgorithmWEB.BLL
{
    // ������� ������-������.
    // �������� ������ � ��������������.
    public class UserBL : IUserBL
    {
        // ������ �� ������ ��� ������ � �������������
        private readonly IUserDao _userDao;
        // ������ �� ������ ��� ������ � ����������� �������
        private readonly Encryption _encryption;
        
        public UserBL(IUserDao userDao)
        {
            _userDao = userDao;
            _encryption = new Encryption();
        }

        // ���������� ������ ������������ � ��������� ������
        public void Add(CreateUserRequest user)
        {
            // �������� �������
            User createdUser = new User() { 
                Login = user.Login,
                Password = _encryption.CreatePassword(user.Password),
            };
            // ���������� ������ ������������
            _userDao.Add(createdUser);
        }

        // �������� ������ �� ������ ������������
        public bool CheckPassword(LoginUserRequest user)
        {
            // ����� ������������ �� ������
            User realUser = _userDao.GetByLogin(user.Login);
            // �������� ������ �� ������������ 
            // � ������������� �� ��� ��������� ������
            return realUser != null && _encryption.CheckPassword(realUser.Password, user.Password);
        }

        // �������� ���������� �� ������������ � ���������� �������
        public bool UserExists(CreateUserRequest user) 
        {
            return _userDao.GetByLogin(user.Login) != null;
        }
    }
}
