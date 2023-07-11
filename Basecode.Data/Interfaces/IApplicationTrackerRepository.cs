using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Basecode.Data.Models;

namespace Basecode.Data.Interfaces
{
    public interface IApplicationTrackerRepository
    {
        IQueryable<ApplicationTracker> RetrieveAll();
        void Add(ApplicationTracker applicationTracker);
        ApplicationTracker? GetByTrackerId(string trackerId);
        ApplicationTracker? GetByStatus(string status);
    }
}
