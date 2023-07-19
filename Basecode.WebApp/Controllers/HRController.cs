using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace Basecode.WebApp.Controllers
{
    [Authorize(Roles = "hr")]
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