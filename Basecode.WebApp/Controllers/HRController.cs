using Microsoft.AspNetCore.Mvc;

namespace Basecode.WebApp.Controllers
{
    public class HRController : Controller
    {
        public IActionResult JobPostList()
        {
            return View();
        }

        public IActionResult CreateJobPost()
        {
            return View();
        }

        public IActionResult EditJobPost()
        {
            return View();
        }
    }
}