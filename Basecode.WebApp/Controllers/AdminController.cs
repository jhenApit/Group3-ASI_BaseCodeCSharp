using Basecode.Data.Dtos;
using Basecode.Data.Models;
using Basecode.Services.Interfaces;
using Basecode.Services.Utils;
using Microsoft.AspNetCore.Mvc;
using NLog;


namespace Basecode.WebApp.Controllers
{
    public class AdminController : Controller
    {
        private readonly IHrEmployeeService _service;
        private readonly IErrorHandling _errorHandling;
        private static readonly Logger _logger = LogManager.GetCurrentClassLogger();
        public AdminController(IHrEmployeeService service, IErrorHandling errorHandling)
        {
            _service = service;
            _errorHandling = errorHandling;
        }
        /// <summary>
        /// Retrieves the HR employee with the specified email and displays the admin dashboard.
        /// </summary>
        /// <param name="Email">Email of the HR employee</param>
        /// <returns>The admin dashboard view with the HR employee's details</returns>
        public IActionResult AdminDashboard(string Email)
        {
            var hrEmployee = _service.GetByEmail(Email);
            return View(hrEmployee);
        }

        /// <summary>
        /// Retrieves all HR employees and displays the HR list.
        /// </summary>
        /// <returns>The HR list view with all HR employee data</returns>
        public IActionResult HrList()
        {
            var data = _service.RetrieveAll();
            return View(data);
        }

        /// <summary>
        /// Creates an account for the HR employee.
        /// </summary>
        /// <param name="hrEmployee">Details of the HR employee to be created</param>
        /// <returns>Redirects to the HrList page, displaying the updated list of accounts, including the newly created account</returns>
        public IActionResult CreateHrAccount(HREmployeeCreationDto hrEmployee)
        {
            if (ModelState.IsValid)
            {
                var data = _service.CreateHrAccount(hrEmployee);
                if (!data.Result)
                {
                    _logger.Error(_errorHandling.SetLog(data));
                    ViewBag.ErrorMessage = data.Message;
                    return View(hrEmployee);
                }
                _service.Add(hrEmployee);
                return RedirectToAction("HrList");
            }
            ModelState.Clear();
            return View(hrEmployee);
        }

        /// <summary>
        /// Displays the account selected for editing.
        /// </summary>
        /// <param name="id">The ID of the account selected</param>
        /// <returns>View of the page with the details of the account</returns>
        public IActionResult EditHrAccountView(int id)
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
        /// Checks for server-side errors and updates the HR account.
        /// </summary>
        /// <param name="hrEmployee">The HR employee object to be updated</param>
        /// <returns>
        /// If there are errors, returns to the EditHrAccountView with error message
        /// If no errors, redirects to the HrList page
        /// </returns>
        [HttpPost]
        public IActionResult EditHrAccount(HREmployeeUpdationDto hrEmployee)
        {
            var data = _service.EditHrAccount(hrEmployee);
            if (!data.Result)
            {
                _logger.Error(_errorHandling.SetLog(data));
                ViewBag.ErrorMessage = data.Message;
                return View("EditHrAccountView", hrEmployee);
            }
            else if (ModelState.IsValid)
            {
                // Perform account update logic
                _service.Update(hrEmployee);
                return RedirectToAction("HrList");
            }
            return RedirectToAction("HrList");

        }

        /// <summary>
        /// Deletes the HR account with the specified ID.
        /// </summary>
        /// <param name="id">The ID of the HR account to be deleted</param>
        /// <returns>Redirects to the HrList page</returns>
        public IActionResult DeleteHrAccount(int id)
        {
            _service.SemiDelete(id);
            return RedirectToAction("HrList");
        }

        /// <summary>
        /// Updates the HR employee's account.
        /// </summary>
        /// <param name="hrEmployee">The updated HR employee details</param>
        /// <returns>Redirects to the HrList page</returns>
        public IActionResult Update(HREmployeeUpdationDto hrEmployee)
        {
            _service.Update(hrEmployee);
            return RedirectToAction("HrList");
        }

        /// <summary>
        /// Adds a new HR employee account.
        /// </summary>
        /// <param name="hrEmployee">The new HR employee details</param>
        /// <returns>Redirects to the HrList page</returns>
        [HttpPost]
        public IActionResult Add(HREmployeeCreationDto hrEmployee)
        {
            _service.Add(hrEmployee);
            return RedirectToAction("HrList");
        }
    }
}
