using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Basecode.Data.Interfaces;
using Basecode.Data.Models;

namespace Basecode.Data.Repositories
{
    public class InterviewersRepository : BaseRepository, IInterviewersRepository
    {
        private readonly BasecodeContext _context;
        public InterviewersRepository(IUnitOfWork unitOfWork, BasecodeContext context) : base(unitOfWork)
        {
            _context = context;
        }

        public IQueryable<Interviewers> RetrieveAll()
        {
            return this.GetDbSet<Interviewers>();
        }

        public void Add(Interviewers Interviewers)
        {
            _context.Interviewers.Add(Interviewers);
            _context.SaveChanges();
        }

        public Interviewers? GetById(int id)
        {
            return _context.Interviewers.Find(id);
        }

        public void Update(Interviewers Interviewers)
        {
            var existingInterviewers = _context.Interviewers.Find(Interviewers.Id);
            if (existingInterviewers != null)
            {
                // Update the properties of the existing entity
                existingInterviewers.Name = Interviewers.Name;

                // Save the changes
                _context.SaveChanges();
            }

        }
        public void Delete(int id)
        {
            var data = _context.Interviewers.Find(id);
            if (data != null)
            {
                _context.Interviewers.Remove(data);
                _context.SaveChanges();
            }
        }

        public Interviewers? GetByName(string name)
        {
            return _context.Interviewers.FirstOrDefault(e => e.Name == name);
        }

    }
}
