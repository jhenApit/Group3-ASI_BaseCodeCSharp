using Microsoft.AspNetCore.Mvc;

namespace Basecode.WebApp.Controllers
{
    public class ApplicantController : Controller
    {
        public IActionResult TrackApplication()
        {
            return View();
        }

        public IActionResult ContactUs()
        {
            return View();
        }
    }
}
