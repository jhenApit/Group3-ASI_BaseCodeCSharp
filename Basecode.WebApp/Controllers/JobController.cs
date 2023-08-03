using Basecode.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NLog;

namespace Basecode.WebApp.Controllers
{
    public class JobController : Controller
    {
        private readonly IJobPostingsService _service;
        private static readonly Logger _logger = LogManager.GetCurrentClassLogger();

        public JobController(IJobPostingsService service)
        {
            _service = service;
        }

        /// <summary>
        /// Retrieves all jobs from the service and returns a view with the job data.
        /// </summary>
        /// <returns>An <see cref="IActionResult"/> representing the result of the action.</returns>
        public async Task<IActionResult> FindJobs()
        {
            try
            {
                var data = await _service.RetrieveAllAsync();
                return View(data);
            }
            catch (Exception e)
            {
                _logger.Error("Error Occurred: Failed to retrieve jobs");
                return BadRequest(e.Message);
            }

        }

        /// <summary>
        /// Retrieves a specific job by its ID from the service and returns a view with the job data.
        /// </summary>
        /// <param name="id">The ID of the job to retrieve.</param>
        /// <returns>An <see cref="IActionResult"/> representing the result of the action.</returns>
        public async Task<IActionResult> JobDescription(int id)
        {
            try
            {
                var data = await _service.GetByIdAsync(id);
                return View(data);
            }
            catch (Exception e)
            {
                _logger.Error("Error Occurred: Failed to retrieve job description");
                return BadRequest(e.Message);
            }
        }
    }
}
