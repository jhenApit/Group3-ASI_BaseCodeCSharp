using Basecode.Services.Interfaces;
using Basecode.WebApp.Controllers;
using Basecode.WebApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace Basecode.WebApp.Controllers
{
    public class LoginController : Controller
    {
        private readonly IHrEmployeeService _service;

        public LoginController(IHrEmployeeService service)
        {
            _service = service;
        }
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Login([Bind("Email, Password")] LoginModel loginModel)
        {
            return RedirectToAction("AdminDashboard", "Admin", loginModel);
        }

        [HttpPost]
        public IActionResult AdminDashboard(string Email)
        {
            var data = _service.GetByEmail(Email);
            return View(data);
        }
            public IActionResult ForgotPassword()
        {
            return View();
        }

        public IActionResult ResetPassword()
        {
            return View();
        }
    }
}
