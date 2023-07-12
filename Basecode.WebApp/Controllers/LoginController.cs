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
        //for authorizing login with password
        public async Task<IActionResult> LoginAuth([Bind("Email, Password")] LoginModel loginModel)
        {
            return RedirectToAction("AdminDashboard", "Admin", loginModel);
        }
        //temporary login using email
        [HttpPost]
        public IActionResult Login(string Email)
        {
            var data = _service.GetByEmail(Email);
            return RedirectToAction("AdminDashboard", "Admin", data);
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
