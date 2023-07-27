using Basecode.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Basecode.WebApp.Controllers
{
    public class JobController : Controller
    {
        private readonly IJobPostingsService _service;

        public JobController(IJobPostingsService service)
        {
            _service = service;
        }

        /// <summary>
        /// Retrieves all jobs from the service and returns a view with the job data.
        /// </summary>
        /// <returns>An <see cref="IActionResult"/> representing the result of the action.</returns>
        public IActionResult FindJobs()
        {
            var data = _service.RetrieveAll();
            return View(data);
        }

        /// <summary>
        /// Retrieves a specific job by its ID from the service and returns a view with the job data.
        /// </summary>
        /// <param name="id">The ID of the job to retrieve.</param>
        /// <returns>An <see cref="IActionResult"/> representing the result of the action.</returns>
        public IActionResult JobDescription(int id)
        {
            var data = _service.GetById(id);
            return View(data);
        }
    }
}
