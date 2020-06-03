using GeneticAlgorithm.Entities.Requests;
using GeneticAlgorithmWEB.BLL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace SimplePages.Controllers
{
    public class LoginController : Controller
    {
        // ссылка на модуль с бизнес-логикой для доступа к пользователям
        private readonly IUserBL _userBL;
        // имена представлений
        private readonly string signUpViewName = "SignUp";
        private readonly string signInViewName = "SignIn";
        private readonly string successSignUpViewName = "SignUpSuccess";

        public LoginController(IUserBL userBL)
        {
            _userBL = userBL;
        }
        // Страница для входа в систему.
        [HttpGet]
        public ActionResult Index()
        {
            return View(signInViewName, new LoginUserRequest());
        }
        // Метод для входа в систему.
        [HttpPost]
        public ActionResult SignIn(LoginUserRequest user) {
            // проверка валидности входных данных 
            if (!ModelState.IsValid) {
                return View(signInViewName, user);
            }
            // проверка корректности пароля
            if (_userBL.CheckPassword(user))
            {
                // выделение cookie для аутентификации
                FormsAuthentication.SetAuthCookie(user.Login, true);
                // перенаправление на главную страницу
                return Redirect("/");
            }
            else {
                // если пароль не верный, то пользователь увидит сообщение об ошибке
                // данные не удовлетворяют требованиям 
                ModelState.AddModelError("LOGIN_PASSWORD", "Неверный логин или пароль");
            }
            // возвращение представления с сообщением об ошибке
            return View(signInViewName, user);
        }
        // Страница для регистрации нового пользователя.
        [HttpGet]
        [Authorize]
        public ActionResult SignUp() {
            // представление для добавления нового пользователя
            return View(signUpViewName);
        }
        // Метод для добавления нового пользователя.
        [HttpPost]
        [Authorize]
        public ActionResult SignUp(CreateUserRequest creatingUser) {
            // проверка на валидность переданных данных
            if (!ModelState.IsValid) {
                // возврат представления при неверных данных
                return View(signUpViewName, creatingUser);
            }
            // проверка на существование пользователя с заданным логином
            if (!_userBL.UserExists(creatingUser)) {
                // если пользователь не существует, 
                // то новый пользователь добавляется
                _userBL.Add(creatingUser);
                // возврат представления с сообщением об успешной регистрации
                return View(successSignUpViewName, creatingUser);
            }
            else {
                // добавление сообщения об ошибке
                ModelState.AddModelError("LOGIN_PASSWORD", "Пользователь с таким логином уже существует");
            }
            // возврат представления с сообщением об ошибке
            return View(signUpViewName, creatingUser);
        }
        // Метод для выхода из системы.
        [HttpGet]
        public ActionResult SignOut() {
            // выход из аккаунта
            FormsAuthentication.SignOut();
            // перенаправление на главную страницу
            return Redirect("/");
        }
    }
}