using Microsoft.AspNetCore.Mvc;

namespace Basecode.WebApp.Controllers
{
    public class ReferencesController : Controller
    {
        /// <summary>
        /// Shows the character reference questions in the first page.
        /// </summary>
        /// <returns>Redirect to page 1</returns>
        public IActionResult Page1()
        {
            return View();
        }

        /// <summary>
        /// Shows the character reference questions in the second page.
        /// </summary>
        /// <returns>Redirect to page 2</returns>
        public IActionResult Page2()
        {
            return View();
        }
    }
}
