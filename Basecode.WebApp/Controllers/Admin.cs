﻿using Microsoft.AspNetCore.Mvc;

namespace Basecode.WebApp.Controllers
{
    public class Admin : Controller
    {
        public IActionResult AdminDashboard()
        {
            return View();
        }
        public IActionResult CreateHrAccount()
        {
            return View();
        }

        public IActionResult EditHrAccount()
        {
            return View();
        }

        public IActionResult HrList()
        {
            return View();
        }
    }
}
