using Microsoft.AspNetCore.Mvc;

namespace Basecode.WebApp.Controllers
{
    public class HrEmployeeController : Controller
    {
        public IActionResult HrList()
        {
            return View();
        }

		public IActionResult EditHrAccount()
		{
			return View();
		}
	}
}