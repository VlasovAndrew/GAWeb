using GeneticAlgorithm.Entities.Users;
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
        private readonly IUserBL _userBL;
        private string signUpViewName = "SignUp";
        private string signInViewName = "SignIn";
        
        public LoginController(IUserBL userBL)
        {
            _userBL = userBL;
        }

        [HttpGet]
        public ActionResult Index()
        {
            return View(signInViewName, new User());
        }

        [HttpPost]
        public ActionResult SignIn(User user) {
            if (!ModelState.IsValid) {
                return View(signInViewName, user);
            }
            User currentUser = _userBL.GetByName(user.Login);
            if (currentUser != null && currentUser.Password == user.Password)
            {
                FormsAuthentication.SetAuthCookie(user.Login, true);
                return Redirect("/");
            }
            else 
            {
                ModelState.AddModelError("LOGIN_PASSWORD", "Неверный логин или пароль");    
            }
            return View(signInViewName, user);
        }

        [HttpGet]
        [Authorize]
        public ActionResult SignUp() {
            return View(signUpViewName);
        }

        [HttpPost]
        [Authorize]
        public ActionResult SignUp(CreateUserRequest creatingUser) {
            if (!ModelState.IsValid) {
                return View(signUpViewName, creatingUser);
            }
            if (_userBL.GetByName(creatingUser.Login) == null)
            {
                User user = _userBL.Add(new User()
                {
                    Login = creatingUser.Login,
                    Password = creatingUser.Password,
                });
                return SignIn(user);
            }
            else {
                ModelState.AddModelError("LOGIN_PASSWORD", "Пользователь с таким логином уже существует");            
            }
            return View(signUpViewName, creatingUser);
        }

        [HttpGet]
        public ActionResult SignOut() {
            FormsAuthentication.SignOut();
            return Redirect("/");
        }
    }
}