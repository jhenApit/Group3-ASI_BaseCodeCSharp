using Basecode.Data.Dtos;
using Basecode.Data.Dtos.HrEmployee;
using Basecode.Data.Models;
using Basecode.Services.Interfaces;
using Basecode.Services.Services;
using Basecode.Services.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NLog;
using static Basecode.Data.Constants;


namespace Basecode.WebApp.Controllers
{
    [Authorize(Roles = "admin")]
    public class AdminController : Controller
    {
        private readonly IHrEmployeeService _service;
        private readonly IAdminService _adminService;
        private readonly IErrorHandling _errorHandling;
        private readonly ISendEmailService _sendEmailService;
        private static readonly Logger _logger = LogManager.GetCurrentClassLogger();
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AdminController(IHrEmployeeService service, IAdminService adminService, IErrorHandling errorHandling, ISendEmailService sendEmailService, UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _service = service;
            _adminService = adminService;
            _errorHandling = errorHandling;
            _sendEmailService = sendEmailService;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        /// <summary>
        /// Retrieves all HR employees and displays the HR list.
        /// </summary>
        /// <returns>The HR list view with all HR employee data</returns>
        public async Task<IActionResult> HrList()
        {
            var data = _service.RetrieveAll();
            var user = await _userManager.GetUserAsync(User);
            if (user != null)
            {
                data = data.Where(item => item.UserId != user.Id).ToList();
            }
            return View(data);
        }

        /// <summary>
        /// Creates an account for the HR employee.
        /// </summary>
        /// <param name="hrEmployee">Details of the HR employee to be created</param>
        /// <returns>Redirects to the HrList page, displaying the updated list of accounts, including the newly created account</returns>
        /*public async Task<IActionResult> CreateHrAccount(HREmployeeCreationDto hrEmployee)
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

                await _sendEmailService.SendHrDetailsEmail(hrEmployee);

                return RedirectToAction("HrList");
            }
            ModelState.Clear();
            return View(hrEmployee);
        }*/

        /// <summary>
        /// Displays the account selected for editing.
        /// </summary>
        /// <param name="id">The ID of the account selected</param>
        /// <returns>View of the page with the details of the account</returns>
        public async Task<IActionResult> EditHrAccountView(int id)
        {
            // Retrieve the HR employee from the database using the ID
            var hrEmployee = _service.GetById(id);
            var hrRole = await _userManager.GetRolesAsync(hrEmployee.User);
            var loggedUser = await _userManager.GetUserAsync(User);
            var role = hrRole.FirstOrDefault();
            if (hrEmployee != null)
            {
                // Access user attributes
                string userName = hrEmployee.User.UserName!;
                // Other user attributes you may want to access
                // Create an instance of HREmployeeUpdationDto and populate it with data
                var hrEmployeeDto = new HREmployeeUpdationDto
                {
                    Name = hrEmployee.Name!,
                    Email = hrEmployee.Email!,
                    Password = hrEmployee.Password!,
                    UserName = userName!,
                    UserId = hrEmployee.User.Id,
                    ModifiedBy = loggedUser!.UserName,
                    Id = hrEmployee.Id
                };
                if (role == "admin")
                {
                    hrEmployeeDto.IsAdmin = true;
                }
                else
                {
                    hrEmployeeDto.IsAdmin = false;
                }
                // Pass the HREmployeeUpdationDto as the model to the view
                return View(hrEmployeeDto);
            }
            return View(hrEmployee);

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
        public async Task<IActionResult> EditHrAccount(HREmployeeUpdationDto hrEmployee)
        {
            hrEmployee.Name = hrEmployee.FirstName + ' ' + hrEmployee.MiddleName + ' ' + hrEmployee.LastName;
            var data = _service.EditHrAccount(hrEmployee);
            if (!data.Result)
            {
                _logger.Error(_errorHandling.SetLog(data));
                ViewBag.ErrorMessage = data.Message;
                return View("EditHrAccountView", hrEmployee);
            }
            else if (ModelState.IsValid)
            {
                //get hremployee data
                var hr = _service.GetById(hrEmployee.Id);
                //update username
                await _userManager.SetUserNameAsync(hr.User, hrEmployee.UserName);
                await _userManager.GenerateChangeEmailTokenAsync(hr.User, hrEmployee.Email);
                await _userManager.ChangePasswordAsync(hr.User, hr.Password!, hrEmployee.Password);

                if (hrEmployee.IsAdmin)
                {
                    await _userManager.AddToRoleAsync(hr.User, "admin");
                    await _userManager.RemoveFromRoleAsync(hr.User, "hr");
                }
                else
                {
                    await _userManager.AddToRoleAsync(hr.User, "hr");
                    await _userManager.RemoveFromRoleAsync(hr.User, "admin");
                }
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
        public async Task<IActionResult> DeleteHrAccount(int id)
        {
            var hr = _service.GetById(id);
            await _userManager.DeleteAsync(hr.User);
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
        /*[HttpPost]
        public IActionResult Add(HREmployeeCreationDto hrEmployee)
        {
            _service.Add(hrEmployee);
            _sendEmailService.SendHrDetailsEmail(hrEmployee);
            return RedirectToAction("HrList");
        }*/
    }
}

