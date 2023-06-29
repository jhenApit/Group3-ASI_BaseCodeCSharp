using Microsoft.AspNetCore.Mvc;

namespace Basecode.WebApp.Controllers
{
    public class AdminDashboardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
