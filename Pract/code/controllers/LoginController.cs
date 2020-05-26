﻿namespace SimplePages.Controllers
{
    public class LoginController : Controller
    {
        private readonly IUserBL _userBL;
        private readonly string signUpViewName = "SignUp";
        private readonly string signInViewName = "SignIn";
        private readonly string successSignUpViewName = "SignUpSuccess";

        public LoginController(IUserBL userBL)
        {
            _userBL = userBL;
        }
        // �������� ��� ����� � �������.
        [HttpGet]
        public ActionResult Index()
        {
            return View(signInViewName, new LoginUserRequest());
        }
        // ����� ��� ����� � �������.
        [HttpPost]
        public ActionResult SignIn(LoginUserRequest user) {
            if (!ModelState.IsValid) {
                return View(signInViewName, user);
            }
            if (_userBL.CheckPassword(user))
            {
                FormsAuthentication.SetAuthCookie(user.Login, true);
                return Redirect("/");
            }
            else {
                ModelState.AddModelError("LOGIN_PASSWORD", "�������� ����� ��� ������");
            }
            return View(signInViewName, user);
        }
        // �������� ��� ����������� ������ ������������.
        [HttpGet]
        [Authorize]
        public ActionResult SignUp() {
            return View(signUpViewName);
        }
        // ����� ��� ���������� ������ ������������.
        [HttpPost]
        [Authorize]
        public ActionResult SignUp(CreateUserRequest creatingUser) {
            if (!ModelState.IsValid) {
                return View(signUpViewName, creatingUser);
            }
            
            if (!_userBL.UserExists(creatingUser)) {
                _userBL.Add(creatingUser);
                return View(successSignUpViewName, creatingUser);
            }
            else {
                ModelState.AddModelError("LOGIN_PASSWORD", "������������ � ����� ������� ��� ����������");
            }
            return View(signUpViewName, creatingUser);
        }
        // ����� ��� ������ �� �������.
        [HttpGet]
        public ActionResult SignOut() {
            FormsAuthentication.SignOut();
            return Redirect("/");
        }
    }
}