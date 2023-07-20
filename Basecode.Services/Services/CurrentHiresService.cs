using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Basecode.Data.Dtos;
using Basecode.Data.Dtos.CurrentHires;
using Basecode.Data.Interfaces;
using Basecode.Data.Models;
using Basecode.Services.Interfaces;
using Basecode.Services.Utils;
using static Basecode.Services.Utils.ErrorHandling;

namespace Basecode.Services.Services
{
    public class CurrentHiresService : ErrorHandling, ICurrentHiresService
    {
        private readonly ICurrentHiresRepository _repository;
        private readonly IMapper _mapper;
        private readonly LogContent _logContent = new();

        public CurrentHiresService(ICurrentHiresRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public List<CurrentHires> RetrieveAll()
        {
            return _repository.RetrieveAll().ToList();
        }

        public void Add(CurrentHiresCreationDto CurrentHires)
        {
            var CurrentHiresModel = _mapper.Map<CurrentHires>(CurrentHires);
            _repository.Add(CurrentHiresModel);
        }

        public CurrentHires? GetById(int id)
        {
            return _repository.GetById(id);
        }

        public void Update(CurrentHiresUpdationDto CurrentHires)
        {
            var CurrentHiresModel = _mapper.Map<CurrentHires>(CurrentHires);
            _repository.Update(CurrentHiresModel);
        }

        public void Delete(int id)
        {
            _repository.Delete(id);
        }

        public CurrentHires? GetByPositionId(int positionId)
        {
            return _repository.GetByPositionId(positionId);
        }
        public CurrentHires? GetByHireStatus(string status)
        {
            return _repository.GetByHireStatus(status);
        }

    }
}
