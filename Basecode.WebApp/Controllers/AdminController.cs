using Basecode.Data.Dtos;
using Basecode.Data.Models;
using Basecode.Services.Interfaces;
using Basecode.Services.Services;
using Microsoft.AspNetCore.Mvc;
using NLog;


namespace Basecode.WebApp.Controllers
{
    public class AdminController : Controller
    {
        private readonly IHrEmployeeService _service;
        private static readonly Logger _logger = LogManager.GetCurrentClassLogger();
        public AdminController(IHrEmployeeService service)
        {
            _service = service;
        }
        public IActionResult AdminDashboard(string Email)
        {
            var hrEmployee = _service.GetByEmail(Email);
            return View(hrEmployee);
        }

        public IActionResult HrList()
        {
            var data = _service.RetrieveAll();
            return View(data);
        }

        /// <summary>
        /// Creates account for the HR.
        /// </summary>
        /// <param name="hrEmployee">Details of the HR employee</param>
        /// <returns>Newly created HR account</returns>
        public IActionResult CreateHrAccount(HREmployeeCreationDto hrEmployee)
        {
            if (ModelState.IsValid)
            {
                var data = _service.CreateHrAccount(hrEmployee);
                if (!data.Result)
                {
                    _logger.Error(ErrorHandling.SetLog(data));
                    return View(hrEmployee);
                }
                _service.Add(hrEmployee);
                return RedirectToAction("HrList");
            }

            return View();
        }

        public IActionResult EditHrAccount(int id)
        {
            // Retrieve the HR employee from the database using the ID
            var hrEmployee = _service.GetById(id);

            // Create an instance of HREmployeeUpdationDto and populate it with data
            var hrEmployeeDto = new HREmployeeUpdationDto
            {
                Name = hrEmployee.Name,
                Email = hrEmployee.Email,
                Password = hrEmployee.Password,
                Id = hrEmployee.Id
            };

            // Pass the HREmployeeUpdationDto as the model to the view
            return View(hrEmployeeDto);
        }

        [HttpPost]
        public IActionResult EditHrAccount(HREmployeeUpdationDto hrEmployee)
        {
            if (ModelState.IsValid)
            {
                // Perform account update logic
                _service.Update(hrEmployee);

                return RedirectToAction("HrList");
            }

            return View(hrEmployee);
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
