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

        public IActionResult FindJobs()
        {
            var data = _service.RetrieveAll();
            return View(data);
        }
    }
}
