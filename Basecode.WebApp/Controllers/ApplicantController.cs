using Basecode.Data.Models;
using Basecode.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Basecode.WebApp.Controllers
{
    public class ApplicantController : Controller
    {
        private readonly IApplicantService _service;

        public ApplicantController(IApplicantService service)
        {
            _service = service;
        }
        public IActionResult TrackStatus(int id)
        {
            Applicant data = _service.GetById(id);
            return View(data);
        }

        public IActionResult ContactUs()
        {
            return View();
        }

        public IActionResult TrackApplication()
        {
            return View();
        }


		public IActionResult ApplicationForm()
		{
			return View();
		}
	}
}
