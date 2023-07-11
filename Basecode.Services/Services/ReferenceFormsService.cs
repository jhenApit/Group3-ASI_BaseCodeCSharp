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

namespace Basecode.Services.Services
{
    public class ReferenceFormsService : ErrorHandling, IReferenceFormsService
    {
        private readonly IReferenceFormsRepository _repository;
        private readonly IMapper _mapper;
        private readonly LogContent _logContent = new();

        public ReferenceFormsService(IReferenceFormsRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public List<ReferenceForms> RetrieveAll() 
        {
            return _repository.RetrieveAll().ToList();
        }

        public void Add(ReferenceFormsCreationDto referenceFormsDto)
        {
            var referenceFormsModel = _mapper.Map<ReferenceForms>(referenceFormsDto);
            referenceFormsModel.AnsweredDate = DateTime.Now.Date;
            _repository.Add(referenceFormsModel);
        }

        public ReferenceForms? GetById(int id)
        {
            return _repository.GetById(id);
        }

        public ReferenceForms? GetByCharacterReferenceId(int characterReferenceId)
        {
            return _repository.GetByCharacterReferenceId(characterReferenceId);
        }
    }

}
