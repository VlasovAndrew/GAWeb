﻿namespace SimplePages.Controllers
{
    public class HomeController : Controller
    {
        // ����� ��� ��������� �������� ��������.
        public ActionResult Index()
        {
            return View("Index");
        }
    }
}
