using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Basecode.Data.Interfaces;
using Basecode.Data.Models;

namespace Basecode.Data.Repositories
{
    public class InterviewsRepository : BaseRepository, IInterviewsRepository
    {
        private readonly BasecodeContext _context;
        public InterviewsRepository(IUnitOfWork unitOfWork, BasecodeContext context) : base(unitOfWork)
        {
            _context = context;
        }

        public IQueryable<Interviews> RetrieveAll()
        {
            return this.GetDbSet<Interviews>();
        }

        public void Add(Interviews Interviews)
        {
            _context.Interviews.Add(Interviews);
            _context.SaveChanges();
        }

        public Interviews? GetById(int id)
        {
            return _context.Interviews.Find(id);
        }

        public void Update(Interviews Interviews)
        {
            var existingInterviews = _context.Interviews.Find(Interviews.Id);
            if (existingInterviews != null)
            {
                // Update the properties of the existing entity
                existingInterviews.ApplicantId = Interviews.ApplicantId;
                existingInterviews.InterviewerId = Interviews.InterviewerId;
                existingInterviews.InterviewType = Interviews.InterviewType;
                existingInterviews.Results = Interviews.Results;
                existingInterviews.InterviewDate = Interviews.InterviewDate;
                existingInterviews.TimeStart = Interviews.TimeStart;
                existingInterviews.TimeEnd = Interviews.TimeEnd;

                // Save the changes
                _context.SaveChanges();
            }

        }
        public void Delete(int id)
        {
            var data = _context.Interviews.Find(id);
            if (data != null)
            {
                _context.Interviews.Remove(data);
                _context.SaveChanges();
            }
        }

        public Interviews? GetByApplicantId(int applicantId)
        {
            return _context.Interviews.FirstOrDefault(e => e.ApplicantId == applicantId);
        }

    }

}
