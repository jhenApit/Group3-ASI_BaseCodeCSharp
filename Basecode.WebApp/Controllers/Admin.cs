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

        public IActionResult HrList()
        {
            var data = _service.RetrieveAll();
            return View(data);
        }
        public IActionResult CreateHrAccount()
        {
            return View();
        }

        public IActionResult EditHrAccount(int id)
        {
            var data = _service.GetById(id);
            return View(data);
        }

        public IActionResult Update(HrEmployee hrEmployee)
        {
            _service.Update(hrEmployee);
            return RedirectToAction("HrList");
        }

        [HttpPost]
        public IActionResult Add(HrEmployee hrEmployee)
        {
            _service.Add(hrEmployee);
            return RedirectToAction("HrList");
        }

    }
}
