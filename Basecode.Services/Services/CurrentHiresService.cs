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
using NLog;
using static Basecode.Services.Utils.ErrorHandling;

namespace Basecode.Services.Services
{
    public class CurrentHiresService : ErrorHandling, ICurrentHiresService
    {
        private readonly ICurrentHiresRepository _repository;
        private readonly IMapper _mapper;
        private static readonly Logger _logger = LogManager.GetCurrentClassLogger();

        public CurrentHiresService(ICurrentHiresRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<List<CurrentHires>> RetrieveAllAsync()
        {
            try
            {
                var currentHires = await _repository.RetrieveAllAsync();
                return currentHires.ToList();
            }
            catch (Exception ex)
            {
                _logger.Error("CurrentHiresService > RetrieveAllAsync > Failed:" + ex.Message);
                throw;
            }
        }

        public async Task AddAsync(CurrentHiresCreationDto CurrentHires)
        {
            try
            {
                var CurrentHiresModel = _mapper.Map<CurrentHires>(CurrentHires);
                await _repository.AddAsync(CurrentHiresModel);
            }
            catch (Exception ex)
            {
                _logger.Error("CurrentHiresService > AddAsync > Failed:" + ex.Message);
                throw;
            }
        }

        public async Task<CurrentHires?> GetByIdAsync(int id)
        {
            try
            {
                return await _repository.GetByIdAsync(id);
            }
            catch (Exception ex)
            {
                _logger.Error("CurrentHiresService > GetByIdAsync > Failed:" + ex.Message);
                throw;
            }
        }

        public async Task UpdateAsync(CurrentHiresUpdationDto CurrentHires)
        {
            try
            {
                var CurrentHiresModel = _mapper.Map<CurrentHires>(CurrentHires);
                await _repository.UpdateAsync(CurrentHiresModel);
            }
            catch (Exception ex)
            {
                _logger.Error("CurrentHiresService > UpdateAsync > Failed:" + ex.Message);
                throw;
            }
        }

        public async Task DeleteAsync(int id)
        {
            try
            {
                await _repository.DeleteAsync(id);
            }
            catch (Exception ex)
            {
                _logger.Error("CurrentHiresService > DeleteAsync > Failed:" + ex.Message);
                throw;
            }
        }

        public async Task<CurrentHires?> GetByPositionIdAsync(int positionId)
        {
            try
            {
                return await _repository.GetByPositionIdAsync(positionId);
            }
            catch (Exception ex)
            {
                _logger.Error("CurrentHiresService > GetByPositionIdAsync > Failed:" + ex.Message);
                throw;
            }
        }
        public async Task<CurrentHires?> GetByHireStatusAsync(string status)
        {
            try
            {
                return await _repository.GetByHireStatusAsync(status);
            }
            catch (Exception ex)
            {
                _logger.Error("CurrentHiresService > GetByHireStatusAsync > Failed:" + ex.Message);
                throw;
            }
        }

    }
}
