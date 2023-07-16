using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Basecode.Data.Interfaces;
using Basecode.Data.Models;
using static Basecode.Data.Models.ApplicationTracker;

namespace Basecode.Data.Repositories
{
    public class ApplicationTrackerRepository : BaseRepository, IApplicationTrackerRepository
    {
        private readonly BasecodeContext _context;
        public ApplicationTrackerRepository(IUnitOfWork unitOfWork, BasecodeContext context) : base(unitOfWork)
        {
            _context = context;
        }
        /// <summary>
        /// Adds a new application tracker to the context and saves changes.
        /// </summary>
        /// <param name="applicationTracker">The application tracker to be added.</param>
        public void Add(ApplicationTracker applicationTracker)
        {
            _context.ApplicationTrackers.Add(applicationTracker);
            _context.SaveChanges();
        }

        /// <summary>
        /// Retrieves an application tracker by its status.
        /// </summary>
        /// <param name="status">The status of the application tracker.</param>
        /// <returns>The application tracker with the specified status, or null if not found.</returns>
        public ApplicationTracker? GetByStatus(string status)
        {
            if (!Enum.TryParse<ApplicationStatus>(status, out var applicationStatus))
            {
                throw new ArgumentException("Invalid status value.");
            }
            return _context.ApplicationTrackers.FirstOrDefault(e => e.Status == applicationStatus);
        }

        /// <summary>
        /// Retrieves an application tracker by its tracker ID.
        /// </summary>
        /// <param name="trackerId">The tracker ID of the application tracker.</param>
        /// <returns>The application tracker with the specified tracker ID, or null if not found.</returns>
        public ApplicationTracker? GetByTrackerId(string trackerId)
        {
            return _context.ApplicationTrackers.FirstOrDefault(e => e.TrackerId == trackerId);
        }

        /// <summary>
        /// Retrieves all application trackers.
        /// </summary>
        /// <returns>An IQueryable of application trackers.</returns>
        public IQueryable<ApplicationTracker> RetrieveAll()
        {
            return this.GetDbSet<ApplicationTracker>();
        }

    }
}
