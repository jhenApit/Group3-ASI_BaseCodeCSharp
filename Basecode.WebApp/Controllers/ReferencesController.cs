using Basecode.Data.Dtos;
using Microsoft.AspNetCore.Mvc;
using Basecode.Services.Interfaces;

namespace Basecode.WebApp.Controllers
{
    public class ReferencesController : Controller
    {
        public readonly IReferenceFormsService _service;

        public ReferencesController(IReferenceFormsService service)
        {
            _service = service;
        }

        public readonly IReferenceFormsService _service;

        public ReferencesController(IReferenceFormsService service)
        {
            _service = service;
        }
        /// <summary>
        /// Shows the character reference questions in the first page.
        /// </summary>
        /// <returns>Redirect to page 1</returns>
        public IActionResult Page1()
        {
            return View();
        }

        [HttpPost]
        public IActionResult ToPage2(ReferenceFormsCreationDto referenceFormsCreationDto)
        {
            TempData["Answer1"] = referenceFormsCreationDto.Answer1;
            TempData["Answer2"] = referenceFormsCreationDto.Answer2;
            TempData["Answer3"] = referenceFormsCreationDto.Answer3;
            TempData["Answer4"] = referenceFormsCreationDto.Answer4;
            TempData["Answer5"] = referenceFormsCreationDto.Answer5;
            return RedirectToAction("Page2");
        }

        public IActionResult Page2()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Submit(ReferenceFormsCreationDto referenceFormsCreationDto)
        {
            referenceFormsCreationDto.CharacterReferenceId = 1;

            _service.Add(referenceFormsCreationDto);

            TempData.Clear();

            return RedirectToAction("Page1", referenceFormsCreationDto);
        }
    }
}