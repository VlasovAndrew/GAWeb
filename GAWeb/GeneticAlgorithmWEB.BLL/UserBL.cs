using GeneticAlgorithm.Entities.Users;
using GeneticAlgorithmWEB.BLL.Interfaces;
using GeneticAlgorithmWEB.DAL.Interfaces;
using GeneticAlgorithm.Entities.Requests;

namespace GeneticAlgorithmWEB.BLL
{
    // Уровень бизнес-логики.
    // Реализут работу с пользователями.
    public class UserBL : IUserBL
    {
        private readonly IUserDao _userDao;
        private readonly Encryption _encryption;
        
        public UserBL(IUserDao userDao)
        {
            _userDao = userDao;
            _encryption = new Encryption();
        }
        // Добавление нового пользователя с шифрацией пароля
        public void Add(CreateUserRequest user)
        {
            User createdUser = new User() { 
                Login = user.Login,
                Password = _encryption.CreatePassword(user.Password),
            };
            _userDao.Add(createdUser);
        }

        // Проверка пароля по логину пользователя
        public bool CheckPassword(LoginUserRequest user)
        {
            User realUser = _userDao.GetByLogin(user.Login);
            if (realUser == null)
            {
                return false;
            }
            else 
            {
                return _encryption.CheckPassword(realUser.Password, user.Password);
            }
        }
        // Проверка существует ли пользователь с переданным логином
        public bool UserExists(CreateUserRequest user) {
            return _userDao.GetByLogin(user.Login) != null;
        }
    }
}
