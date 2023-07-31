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
        /// <summary>
        /// this gets the list of jobs the applicant applied for
        /// </summary>
        /// <param name="applicantId">the applicant id set to jobs</param>
        /// <returns>returns the jobs of an applicant</returns>
        public IQueryable<Interviews> GetByApplicantId(int applicantId)
        {
            return this.GetDbSet<Interviews>().Where(e => e.ApplicantId == applicantId);
        }

    }

}
