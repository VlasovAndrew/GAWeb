﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SimplePages.Controllers
{
    public class HomeController : Controller
    {
        // Метод для получения домашней страницы.
        public ActionResult Index()
        {
            return View("Index");
        }
    }
}
