using Microsoft.AspNetCore.Mvc;

namespace Basecode.WebApp.Controllers
{
    public class HRController : Controller
    {
        /// <summary>
        /// Displays the list of job posts.
        /// </summary>
        /// <returns>The view containing the job post list.</returns>
        public IActionResult JobPostList()
        {
            return View();
        }

        /// <summary>
        /// Displays the form to create a new job post.
        /// </summary>
        /// <returns>The view containing the job post creation form.</returns>
        public IActionResult CreateJobPost()
        {
            return View();
        }

        /// <summary>
        /// Displays the form to edit an existing job post.
        /// </summary>
        /// <returns>The view containing the job post edit form.</returns>
        public IActionResult EditJobPost()
        {
            return View();
        }

        /// <summary>
        /// Displays the details of a specific job post.
        /// </summary>
        /// <returns>The view containing the job post details.</returns>
        public IActionResult ViewJobPost()
        {
            return View();
        }

        /// <summary>
        /// Displays the interview page
        /// </summary>
        /// <returns>The view containing the list of interviews</returns>
        public IActionResult Interview()
        {
            return View();
        }
    }
}