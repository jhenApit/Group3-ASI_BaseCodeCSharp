using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace Basecode.WebApp.Controllers
{
    [Authorize(AuthenticationSchemes = "Bearer", Roles ="hr,admin")]
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