using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Basecode.Data.Interfaces;
using Basecode.Data.Models;

namespace Basecode.Data.Repositories
{
    public class HrEmployeeRepository : BaseRepository , IHrEmployeeRepository
    { 
        private readonly BasecodeContext _context;
        public HrEmployeeRepository(IUnitOfWork unitOfWork, BasecodeContext context) : base (unitOfWork) 
        {
            _context = context;
        }

        public IQueryable<HrEmployee> RetrieveAll() 
        { 
            return this.GetDbSet<HrEmployee>().Where(e => !e.IsDeleted);
        }

        public void Add(HrEmployee hrEmployee)
        {
            _context.HrEmployees.Add(hrEmployee);
            _context.SaveChanges();
        }
    }
}
