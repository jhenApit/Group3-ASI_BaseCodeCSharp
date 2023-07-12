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
    public class ApplicationTrackerService : ErrorHandling, IApplicationTrackerService
    {
        private readonly IApplicationTrackerRepository _repository;
        private readonly IMapper _mapper;
        private readonly LogContent _logContent = new();

        public ApplicationTrackerService(IApplicationTrackerRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public List<ApplicationTracker> RetrieveAll()
        {
            return _repository.RetrieveAll().ToList();
        }

        public void Add(ApplicationTrackerCreationDto applicationTrackerDto)
        {
            var applicationTrackerModel = _mapper.Map<ApplicationTracker>(applicationTrackerDto);
            applicationTrackerModel.TrackerId = GenerateRandomTrackerId();
            _repository.Add(applicationTrackerModel);
        }

        private static string? GenerateRandomTrackerId()
        {
            const string allowedChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var random = new Random();
            var result = new string(Enumerable.Repeat(allowedChars, 8)
                .Select(s => s[random.Next(s.Length)]).ToArray());
            return result;
        }

        public ApplicationTracker? GetByTrackerId(string trackerId)
        {
            return _repository.GetByTrackerId(trackerId);
        }

        public ApplicationTracker? GetByStatus(string status)
        {
            return _repository.GetByStatus(status);
        }

    }
}
