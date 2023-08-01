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
        private static readonly Logger _logger = LogManager.GetCurrentClassLogger();
        private readonly IEmailService _emailService;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        public AdminController(
            IHrEmployeeService service, 
            IErrorHandling errorHandling, 
            IAdminService adminService, 
            IEmailService emailService, 
            UserManager<IdentityUser> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            _service = service;
            _errorHandling = errorHandling;
            _adminService = adminService;
            _emailService = emailService;
            _userManager = userManager;
            _roleManager = roleManager;
        }


        /// <summary>
        /// Retrieves all HR employees and displays the HR list.
        /// </summary>
        /// <returns>The HR list view with all HR employee data</returns>
        public async Task<IActionResult> HrList()
        {
            var data = await _service.RetrieveAllAsync();
            var user = await _userManager.GetUserAsync(User);
            if (user != null)
            {
                data = data.Where(item => item.UserId != user.Id).ToList();
            }
            return View(data);
        }

        /// <summary>
        /// Displays the account selected for editing.
        /// </summary>
        /// <param name="id">The ID of the account selected</param>
        /// <returns>View of the page with the details of the account</returns>
        public async Task<IActionResult> EditHrAccountView(int id)
        {
            // Retrieve the HR employee from the database using the ID
            var hrEmployee = await _service.GetByIdAsync(id);
            var hrRole = await _userManager.GetRolesAsync(hrEmployee.User);
            var loggedUser = await _userManager.GetUserAsync(User);
            var role = hrRole.FirstOrDefault();
            if (hrEmployee != null)
            {
                // Access user attributes
                string userName = hrEmployee.User.UserName;
                // Other user attributes you may want to access
                // Create an instance of HREmployeeUpdationDto and populate it with data
                var hrEmployeeDto = new HREmployeeUpdationDto
                {
                    Name = hrEmployee.Name,
                    Email = hrEmployee.Email,
                    Password = hrEmployee.Password,
                    UserName = userName,
                    UserId = hrEmployee.User.Id,
                    ModifiedBy = loggedUser.UserName,
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
            /*var data = _service.EditHrAccount(hrEmployee);
            if (!data.Result)
            {
                _logger.Error(_errorHandling.SetLog(data));
                ViewBag.ErrorMessage = data.Message;
                return View("EditHrAccountView", hrEmployee);
            }
            else*/ if (ModelState.IsValid)
            {
                //get hremployee data
                var hr = await _service.GetByIdAsync(hrEmployee.Id);
                //get aspnetuser data
                var user = await _userManager.GetUserAsync(User);
                //update username
                await _userManager.SetUserNameAsync(hr.User, hrEmployee.UserName);
                await _userManager.GenerateChangeEmailTokenAsync(hr.User, hrEmployee.Email);
                if(hrEmployee.Password != null) 
                {
                    await _userManager.ChangePasswordAsync(hr.User, hr.Password, hrEmployee.Password);
                }
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
                await _service.UpdateAsync(hrEmployee);
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
            var hr = await _service.GetByIdAsync(id);
            await _userManager.DeleteAsync(hr.User);
            return RedirectToAction("HrList");
        }

        /// <summary>
        /// Updates the HR employee's account.
        /// </summary>
        /// <param name="hrEmployee">The updated HR employee details</param>
        /// <returns>Redirects to the HrList page</returns>
        public async Task<IActionResult> Update(HREmployeeUpdationDto hrEmployee)
        {
            await _service.UpdateAsync(hrEmployee);
            return RedirectToAction("HrList");
        }
    }
}

