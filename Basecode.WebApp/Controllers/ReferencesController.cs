using Basecode.Data.Dtos;
using Microsoft.AspNetCore.Mvc;
using Basecode.Services.Interfaces;
using NLog;

namespace Basecode.WebApp.Controllers
{
    public class ReferencesController : Controller
    {
        public readonly IReferenceFormsService _service;
        public readonly ICharacterReferencesService _characterReferencesService;
        private static readonly Logger _logger = LogManager.GetCurrentClassLogger();

        public ReferencesController(IReferenceFormsService service, ICharacterReferencesService characterReferencesService)
        {
            _service = service;
            _characterReferencesService = characterReferencesService;
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
            try
            {
                TempData["Answer1"] = referenceFormsCreationDto.Answer1;
                TempData["Answer2"] = referenceFormsCreationDto.Answer2;
                TempData["Answer3"] = referenceFormsCreationDto.Answer3;
                TempData["Answer4"] = referenceFormsCreationDto.Answer4;
                TempData["Answer5"] = referenceFormsCreationDto.Answer5;
                return RedirectToAction("Page2");
            }
            catch (Exception ex) 
            {
                _logger.Error("Error Occured: Failed to retrive data");
                return BadRequest(ex.Message);
            }
        }

        public IActionResult Page2()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Submit(ReferenceFormsCreationDto referenceFormsCreationDto)
        {
            try
            {
                var id = 1; //temporary
                var existingRef = await _characterReferencesService.GetByIdAsync(id);
                referenceFormsCreationDto.CharacterReferenceId = id; //temporary

                await _service.AddAsync(referenceFormsCreationDto);
                TempData.Clear();
                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {
                _logger.Error("Error Occured: Failed to retrive data");
                return BadRequest(ex.Message);
            }
        }
    }
}