using Basecode.Data.Dtos;
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
        public IActionResult CreateHrAccount()
        {
            return View();
        }

        /// <summary>
        /// Creates account for the HR.
        /// </summary>
        /// <param name="hrEmployee">Details of the HR employee</param>
        /// <returns>Newly created HR account</returns>
        [HttpPost]
        public IActionResult CreateHrAccount(HREmployeeCreationDto hrEmployee)
        {
            var data = _service.CreateHrAccount(hrEmployee);
            if(!data.Result) 
            {
                _logger.Error(ErrorHandling.SetLog(data));
                return View();
            }
            _service.Add(hrEmployee);
            return RedirectToAction("HrList");
        }

        /// <summary>
        /// Edit Hr Account view
        /// </summary>
        /// <param name="id">id of the selected account</param>
        /// <returns>edithraccount view</returns>
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

        /// <summary>
        /// Updates account in the DB
        /// </summary>
        /// <param name="hrEmployee">Details of the HR employee account</param>
        /// <returns>redirects to hrlist if successful/returns validations for errors</returns>
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
    }
}
