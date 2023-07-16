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

        /// <summary>
        /// Retrieves all application trackers.
        /// </summary>
        /// <returns>A list of ApplicationTracker objects.</returns>
        public List<ApplicationTracker> RetrieveAll()
        {
            return _repository.RetrieveAll().ToList();
        }

        /// <summary>
        /// Adds a new application tracker.
        /// </summary>
        /// <param name="applicationTrackerDto">The ApplicationTrackerCreationDto object containing the data for the new tracker.</param>
        public void Add(ApplicationTrackerCreationDto applicationTrackerDto)
        {
            var applicationTrackerModel = _mapper.Map<ApplicationTracker>(applicationTrackerDto);
            applicationTrackerModel.TrackerId = GenerateRandomTrackerId();
            _repository.Add(applicationTrackerModel);
        }

        /// <summary>
        /// Retrieves an application tracker by its tracker ID.
        /// </summary>
        /// <param name="trackerId">The tracker ID of the application tracker to retrieve.</param>
        /// <returns>An ApplicationTracker object if found, or null if not found.</returns>
        public ApplicationTracker? GetByTrackerId(string trackerId)
        {
            return _repository.GetByTrackerId(trackerId);
        }

        /// <summary>
        /// Retrieves an application tracker by its status.
        /// </summary>
        /// <param name="status">The status of the application tracker to retrieve.</param>
        /// <returns>An ApplicationTracker object if found, or null if not found.</returns>
        public ApplicationTracker? GetByStatus(string status)
        {
            return _repository.GetByStatus(status);
        }

        #region Helper Funtion
        /// <summary>
        /// Generates a random tracker ID.
        /// </summary>
        /// <returns>A string representing the randomly generated tracker ID.</returns>
        private static string? GenerateRandomTrackerId()
        {
            const string allowedChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var random = new Random();
            var result = new string(Enumerable.Repeat(allowedChars, 8)
                .Select(s => s[random.Next(s.Length)]).ToArray());
            return result;
        }
        #endregion
    }
}
