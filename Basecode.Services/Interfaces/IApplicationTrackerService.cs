using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Basecode.Data.Dtos;
using Basecode.Data.Models;

namespace Basecode.Services.Interfaces
{
    public interface IApplicationTrackerService
    {
        List<ApplicationTracker> RetrieveAll();
        void Add(ApplicationTrackerCreationDto ApplicationTracker);
        ApplicationTracker GetByTrackerId(int id);
    }
}
