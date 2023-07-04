using Basecode.Data.Dtos;
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
            // Retrieve the HR employee from the database using the ID
            var hrEmployee = _service.GetById(id);

            // Create an instance of HREmployeeUpdationDto and populate it with data
            var hrEmployeeDto = new HREmployeeUpdationDto
            {
                Id = id,
                Name = hrEmployee.Name,
                Email = hrEmployee.Email,
                Password = hrEmployee.Password
            };

            // Pass the HREmployeeUpdationDto as the model to the view
            return View(hrEmployeeDto);
        }

        public IActionResult DeleteHrAccount(int id)
        {
            _service.SemiDelete(id);
            return RedirectToAction("HrList");
        }

        public IActionResult Update(HREmployeeUpdationDto hrEmployee)
        {
            _service.Update(hrEmployee);
            return RedirectToAction("HrList");
        }

        [HttpPost]
        public IActionResult Add(HREmployeeCreationDto hrEmployee)
        {
            _service.Add(hrEmployee);
            return RedirectToAction("HrList");
        }

    }
}
