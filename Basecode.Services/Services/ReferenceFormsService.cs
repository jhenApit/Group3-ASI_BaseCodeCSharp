using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Basecode.Data.Dtos;
using Basecode.Data.Interfaces;
using Basecode.Data.Models;
using Basecode.Services.Interfaces;
using Basecode.Services.Utils;

namespace Basecode.Services.Services
{
    public class ReferenceFormsService : IReferenceFormsService
    {
        private readonly IReferenceFormsRepository _repository;
        private readonly IMapper _mapper;
        private readonly LogContent _logContent = new();

        public ReferenceFormsService(IReferenceFormsRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        /// <summary>
        /// Retrieves all reference forms.
        /// </summary>
        /// <returns>A list of ReferenceForms.</returns>
        public List<ReferenceForms> RetrieveAll()
        {
            return _repository.RetrieveAll().ToList();
        }

        /// <summary>
        /// Adds a new reference form.
        /// </summary>
        /// <param name="referenceFormsDto">The ReferenceFormsCreationDto object containing the data for the new reference form.</param>
        public void Add(ReferenceFormsCreationDto referenceFormsDto)
        {
            var referenceFormsModel = _mapper.Map<ReferenceForms>(referenceFormsDto);
            referenceFormsModel.AnsweredDate = DateTime.Now.Date;
            _repository.Add(referenceFormsModel);
        }

        /// <summary>
        /// Retrieves a reference form by its ID.
        /// </summary>
        /// <param name="id">The ID of the reference form to retrieve.</param>
        /// <returns>The ReferenceForms object with the specified ID, or null if not found.</returns>
        public ReferenceForms? GetById(int id)
        {
            return _repository.GetById(id);
        }

        /// <summary>
        /// Retrieves a reference form by a character reference ID.
        /// </summary>
        /// <param name="characterReferenceId">The ID of the character reference associated with the reference form.</param>
        /// <returns>The ReferenceForms object with the specified character reference ID, or null if not found.</returns>
        public ReferenceForms? GetByCharacterReferenceId(int characterReferenceId)
        {
            return _repository.GetByCharacterReferenceId(characterReferenceId);
        }

    }

}
