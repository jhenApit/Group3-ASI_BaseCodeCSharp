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
        public async Task<List<CurrentHires>> RetrieveAllAsync()
        {
            var currentHires = await _repository.RetrieveAllAsync();
            return currentHires.ToList();
        }

        public async Task AddAsync(CurrentHiresCreationDto CurrentHires)
        {
            var CurrentHiresModel = _mapper.Map<CurrentHires>(CurrentHires);
            await _repository.AddAsync(CurrentHiresModel);
        }

        public async Task<CurrentHires?> GetByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task UpdateAsync(CurrentHiresUpdationDto CurrentHires)
        {
            var CurrentHiresModel = _mapper.Map<CurrentHires>(CurrentHires);
            await _repository.UpdateAsync(CurrentHiresModel);
        }

        public async Task DeleteAsync(int id)
        {
            await _repository.DeleteAsync(id);
        }

        public async Task<CurrentHires?> GetByPositionIdAsync(int positionId)
        {
            return await _repository.GetByPositionIdAsync(positionId);
        }
        public async Task<CurrentHires?> GetByHireStatusAsync(string status)
        {
            return await _repository.GetByHireStatusAsync(status);
        }

    }
}
