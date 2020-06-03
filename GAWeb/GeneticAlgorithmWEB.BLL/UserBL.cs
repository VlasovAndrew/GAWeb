using GeneticAlgorithm.Entities.Users;
using GeneticAlgorithmWEB.BLL.Interfaces;
using GeneticAlgorithmWEB.DAL.Interfaces;
using GeneticAlgorithm.Entities.Requests;
using GeneticAlgorithmWEB.Encrypt;

namespace GeneticAlgorithmWEB.BLL
{
    // Уровень бизнес-логики.
    // Реализут работу с пользователями.
    public class UserBL : IUserBL
    {
        // ссылка на модуль для работы с пользователем
        private readonly IUserDao _userDao;
        // ссылка на модуль для работы с хешированим паролей
        private readonly Encryption _encryption;
        
        public UserBL(IUserDao userDao)
        {
            _userDao = userDao;
            _encryption = new Encryption();
        }

        // Добавление нового пользователя с шифрацией пароля
        public void Add(CreateUserRequest user)
        {
            // создание паролей
            User createdUser = new User() { 
                Login = user.Login,
                Password = _encryption.CreatePassword(user.Password),
            };
            // добавление нового пользователя
            _userDao.Add(createdUser);
        }

        // Проверка пароля по логину пользователя
        public bool CheckPassword(LoginUserRequest user)
        {
            // поиск пользователя по логину
            User realUser = _userDao.GetByLogin(user.Login);
            // проверка найден ли пользователь 
            // и соответствует ли ему введенный пароль
            return realUser != null && _encryption.CheckPassword(realUser.Password, user.Password);
        }

        // Проверка существует ли пользователь с переданным логином
        public bool UserExists(CreateUserRequest user) 
        {
            return _userDao.GetByLogin(user.Login) != null;
        }
    }
}
