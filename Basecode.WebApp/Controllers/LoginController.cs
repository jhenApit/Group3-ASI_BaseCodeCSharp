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

        /// <summary>
        /// The main entry point of the application. Renders the default view.
        /// </summary>
        /// <returns>The IActionResult representing the view.</returns>
        public IActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Authorizes login using email and password.
        /// </summary>
        /// <param name="loginModel">The LoginModel object containing email and password.</param>
        /// <returns>The IActionResult representing the redirection to the AdminDashboard view.</returns>
        public async Task<IActionResult> LoginAuth([Bind("Email, Password")] LoginModel loginModel)
        {
            return RedirectToAction("AdminDashboard", "Admin", loginModel);
        }

        /// <summary>
        /// Provides temporary login using email.
        /// </summary>
        /// <param name="Email">The email address used for login.</param>
        /// <returns>The IActionResult representing the redirection to the AdminDashboard view.</returns>
        [HttpPost]
        public IActionResult Login(string Email)
        {
            var data = _service.GetByEmail(Email);
            return RedirectToAction("AdminDashboard", "Admin", data);
        }

        /// <summary>
        /// Renders the ForgotPassword view.
        /// </summary>
        /// <returns>The IActionResult representing the view.</returns>
        public IActionResult ForgotPassword()
        {
            return View();
        }

        /// <summary>
        /// Renders the ResetPassword view.
        /// </summary>
        /// <returns>The IActionResult representing the view.</returns>
        public IActionResult ResetPassword()
        {
            return View();
        }

    }
}
