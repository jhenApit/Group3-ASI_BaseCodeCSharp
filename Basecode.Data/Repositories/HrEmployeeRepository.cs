using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Basecode.Data.Dtos;
using Basecode.Data.Interfaces;
using Basecode.Data.Models;
using Microsoft.EntityFrameworkCore;

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

        public HrEmployee GetById(int id)
        {
            return _context.HrEmployees.Find(id);
        }

        public void Update(HrEmployee hrEmployee)
        {
            _context.Entry(hrEmployee).State = EntityState.Modified;
            _context.Entry(hrEmployee).Property(x => x.CreatedBy).IsModified = false;
            _context.Entry(hrEmployee).Property(x => x.CreatedDate).IsModified = false;
            _context.SaveChanges();

        }

        public void SemiDelete(HrEmployee hrEmployee)
        {
            _context.HrEmployees.Update(hrEmployee);
            _context.SaveChanges();
        }

        public void PermaDelete(int id)
        {
            var data = _context.HrEmployees.Find(id);
            if (data != null)
            {
                _context.HrEmployees.Remove(data);
                _context.SaveChanges();
            }
        }

        public HrEmployee GetByEmail(string email)
        {
            return _context.HrEmployees.FirstOrDefault(e => e.Email == email);
        }

    }
}
