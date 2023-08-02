using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Basecode.Data.Interfaces;
using Basecode.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Basecode.Data.Repositories
{
    public class InterviewsRepository : BaseRepository, IInterviewsRepository
    {
        private readonly BasecodeContext _context;
        public InterviewsRepository(IUnitOfWork unitOfWork, BasecodeContext context) : base(unitOfWork)
        {
            _context = context;
        }

        public async Task<IQueryable<Interviews>> RetrieveAllAsync()
        {
            return await Task.FromResult(this.GetDbSet<Interviews>().Include(e => e.Interviewer).Include(e => e.Applicant));
        }

        public async Task AddAsync(Interviews Interviews)
        {
            await _context.Interviews.AddAsync(Interviews);
            await _context.SaveChangesAsync();
        }

        public async Task<Interviews?> GetByIdAsync(int id)
        {
            return await _context.Interviews.FindAsync(id);
        }

        public async Task UpdateAsync(Interviews Interviews)
        {
            var existingInterviews = await _context.Interviews.FindAsync(Interviews.Id);
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
                await _context.SaveChangesAsync();
            }

        }
        public async Task DeleteAsync(int id)
        {
            var data = await _context.Interviews.FindAsync(id);
            if (data != null)
            {
                _context.Interviews.Remove(data);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<Interviews?> GetByApplicantIdAsync(int applicantId)
        {
            return await _context.Interviews.FirstOrDefaultAsync(e => e.ApplicantId == applicantId);
        }

        public IEnumerable<Interviews> GetInterviewsByInterviewerAndDate(int interviewerId, DateTime interviewDate)
        {
            return _context.Interviews
            .Where(i => i.InterviewerId == interviewerId && i.InterviewDate == interviewDate)
            .ToList();
        }

        public IEnumerable<Interviews> GetInterviewsByInterviewer(int interviewerId)
        {
            return _context.Interviews
                .Where(i => i.InterviewerId == interviewerId)
                .ToList();
        }

        public IEnumerable<Interviews> GetInterviewsByApplicant(int applicantId)
        {
            return _context.Interviews
                .Where(i => i.ApplicantId == applicantId)
                .ToList();
        }


    }

}
