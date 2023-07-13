using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Basecode.Data.Models;

namespace Basecode.Data.Interfaces
{
    public interface ICurrentHiresRepository
    {
        IQueryable<CurrentHires> RetrieveAll();
        CurrentHires? GetByPositionId(int positionId);
        void Add(CurrentHires currentHires);
        CurrentHires? GetById(int id);
        void Update(CurrentHires currentHires);
        void Delete(int id);
        CurrentHires? GetByHireStatus(string status);
    }
}
