using Basecode.Data.Dtos;
using Microsoft.AspNetCore.Mvc;
using Basecode.Services.Interfaces;
using NLog;
using Basecode.Data.Models;
using AutoMapper;
using Humanizer;
using Microsoft.CodeAnalysis;
using NLog.Layouts;

namespace Basecode.WebApp.Controllers
{
    public class ReferencesController : Controller
    {
        public readonly IReferenceFormsService _service;
        public readonly ICharacterReferencesService _characterReferencesService;
        private readonly ISendEmailService _sendEmailService;
        private static readonly Logger _logger = LogManager.GetCurrentClassLogger();
        private readonly IMapper _mapper;

        public ReferencesController(IReferenceFormsService service, 
            ICharacterReferencesService characterReferencesService,
            ISendEmailService sendEmailService, IMapper mapper)
        {
            _service = service;
            _characterReferencesService = characterReferencesService;
            _sendEmailService = sendEmailService;
            _mapper = mapper;
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


                var characterReference = _mapper.Map<CharacterReferences>(referenceFormsCreationDto);

                // Sends an email to express gratitude for providing a character reference to support an applicant's job application.
                await _sendEmailService.SendReferenceGratitudeEmail(characterReference);
                
                //Sends an email notification to the HR team about the completion of a reference form for applicant evaluation.
                await _sendEmailService.SendHrAnsweredFormNotificationEmail(characterReference.Applicant);

                //Sends an email notification to the HR department for character reference approval of an applicant.
                await _sendEmailService.SendHrReferenceApprovalEmail(characterReference.Applicant);
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