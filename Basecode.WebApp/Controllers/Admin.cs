using Basecode.Data.Models;
using Basecode.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Basecode.WebApp.Controllers
{
    public class Admin : Controller
    {
        private readonly IHrEmployeeService _service;

        public Admin(IHrEmployeeService service)
        {
            _service = service;
        }
        public IActionResult AdminDashboard()
        {
            return View();
        }
        public IActionResult CreateHrAccount()
        {
            return View();
        }

        public IActionResult EditHrAccount()
        {
            return View();
        }

        public IActionResult HrList()
        {
            var data = _service.RetrieveAll();
            return View(data);
        }

        [HttpPost]
        public IActionResult Add(HrEmployee hrEmployee)
        {
            _service.Add(hrEmployee);
            return RedirectToAction("HrList");
        }
    }
}
