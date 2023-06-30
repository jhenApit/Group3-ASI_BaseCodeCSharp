using Microsoft.AspNetCore.Mvc;

namespace Basecode.WebApp.Controllers
{
    public class HrEmployee : Controller
    {
        public IActionResult CreateHrAccount()
        {
            return View();
        }
    }
}
