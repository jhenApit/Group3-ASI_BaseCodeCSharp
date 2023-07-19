using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Basecode.Data.Dtos;
using Basecode.Data.Dtos.CurrentHires;
using Basecode.Data.Models;

namespace Basecode.Services.Interfaces
{
    public interface ICurrentHiresService
    {
        List<CurrentHires> RetrieveAll();
        CurrentHires? GetByPositionId(int positionId);
        CurrentHires? GetByHireStatus(string status);
        void Add(CurrentHiresCreationDto CurrentHires);
        CurrentHires? GetById(int id);
        void Update(CurrentHiresUpdationDto CurrentHires);
        void Delete(int id);
    }
}
