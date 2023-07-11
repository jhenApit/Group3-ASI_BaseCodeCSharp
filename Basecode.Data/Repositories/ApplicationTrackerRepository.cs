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
        public void Add(ApplicationTracker applicationTracker)
        {
            _context.ApplicationTrackers.Add(applicationTracker);
            _context.SaveChanges();
        }

        public ApplicationTracker? GetByStatus(string status)
        {
            if (!Enum.TryParse<ApplicationStatus>(status, out var applicationStatus))
            {
                throw new ArgumentException("Invalid status value.");
            }
            return _context.ApplicationTrackers.FirstOrDefault(e => e.Status == applicationStatus);
        }

        public ApplicationTracker? GetByTrackerId(string trackerId)
        {
            return _context.ApplicationTrackers.FirstOrDefault(e => e.TrackerId == trackerId);
        }

        public IQueryable<ApplicationTracker> RetrieveAll()
        {
            return this.GetDbSet<ApplicationTracker>();
        }
    }
}
