using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Basecode.Data.Interfaces;
using Basecode.Data.Models;
using static Basecode.Data.Enums.Enums;
using static Basecode.Data.Models.CurrentHires;

namespace Basecode.Data.Repositories
{
    public class CurrentHiresRepository : BaseRepository, ICurrentHiresRepository
    {
        private readonly BasecodeContext _context;
        public CurrentHiresRepository(IUnitOfWork unitOfWork, BasecodeContext context) : base(unitOfWork)
        {
            _context = context;
        }

        public IQueryable<CurrentHires> RetrieveAll()
        {
            return this.GetDbSet<CurrentHires>();
        }

        public void Add(CurrentHires CurrentHires)
        {
            _context.CurrentHires.Add(CurrentHires);
            _context.SaveChanges();
        }

        public CurrentHires? GetById(int id)
        {
            return _context.CurrentHires.Find(id);
        }

        public void Update(CurrentHires CurrentHires)
        {
            var existingCurrentHires = _context.CurrentHires.Find(CurrentHires.Id);
            if (existingCurrentHires != null)
            {
                // Update the properties of the existing entity
                existingCurrentHires.HireStatus = CurrentHires.HireStatus;
                existingCurrentHires.HireDate = CurrentHires.HireDate;

                // Save the changes
                _context.SaveChanges();
            }

        }
        public void Delete(int id)
        {
            var data = _context.CurrentHires.Find(id);
            if (data != null)
            {
                _context.CurrentHires.Remove(data);
                _context.SaveChanges();
            }
        }

        public CurrentHires? GetByPositionId(int positionId)
        {
            return _context.CurrentHires.FirstOrDefault(e => e.PositionId == positionId);
        }

        public CurrentHires? GetByHireStatus(string status)
        {
            if (Enum.TryParse(status, out HireStatus hireStatus))
            {
                return _context.CurrentHires.FirstOrDefault(e => e.HireStatus == hireStatus);
            }
            return null;
        }
    }
}
