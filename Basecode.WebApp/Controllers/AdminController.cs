using Basecode.Data.Dtos;
using Basecode.Data.Models;
using Basecode.Services.Interfaces;
using Basecode.Services.Utils;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NLog;


namespace Basecode.WebApp.Controllers
{
    public class AdminController : Controller
    {
        private readonly IHrEmployeeService _service;
        private readonly IAdminService _admin_service;
        private readonly IErrorHandling _errorHandling;
        private static readonly Logger _logger = LogManager.GetCurrentClassLogger();
        public AdminController(IHrEmployeeService service, IErrorHandling errorHandling, IAdminService admin_service)
        {
            _service = service;
            _errorHandling = errorHandling;
            _admin_service = admin_service;
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
        /// <returns>Redirect to the HrList page, displaying the updated list of accounts, including the newly created account.</returns>
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
        /// Shows the account selected for editing
        /// </summary>
        /// <param name="id">the id of the account selected</param>
        /// <returns>view of the page with the details of the account</returns>
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
        /// checks for server side error
        /// updates account
        /// </summary>
        /// <param name="hrEmployee">the object passed to be updated</param>
        /// <returns>
        /// if there are errors page will return to edithraccountview
        /// if no errors page will redirect to hrlist
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

        public IActionResult CreateRole()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateRole(CreateRoleDto createRoleDto)
        {
            if (ModelState.IsValid)
            {

                IdentityResult result = await _admin_service.CreateRole(createRoleDto.RoleName);

                if (result.Succeeded)
                {
                    return RedirectToAction("AdminDashboard", "Admin");
                }
            }

            return View();
        }
    }
}
