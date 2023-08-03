using Basecode.Data.Interfaces;
using Basecode.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Basecode.Data.Repositories
{
    /// <summary>
    /// Represents a repository for managing interviews data.
    /// </summary>
    public class InterviewsRepository : BaseRepository, IInterviewsRepository
    {
        private readonly BasecodeContext _context;

        /// <summary>
        /// Initializes a new instance of the InterviewsRepository class.
        /// </summary>
        /// <param name="unitOfWork">The unit of work to be used for database operations.</param>
        /// <param name="context">The database context.</param>
        public InterviewsRepository(IUnitOfWork unitOfWork, BasecodeContext context) : base(unitOfWork)
        {
            _context = context;
        }

        /// <summary>
        /// Retrieves all interviews asynchronously.
        /// </summary>
        /// <returns>An asynchronous task that represents the operation, containing the collection of interviews.</returns>
        public async Task<IQueryable<Interviews>> RetrieveAllAsync()
        {
            return await Task.FromResult(this.GetDbSet<Interviews>().Include(e => e.Interviewer).Include(e => e.Applicant));
        }

        /// <summary>
        /// Adds a new interview asynchronously.
        /// </summary>
        /// <param name="interview">The interview to be added.</param>
        public async Task AddAsync(Interviews interview)
        {
            await _context.Interviews.AddAsync(interview);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Retrieves an interview by its ID asynchronously.
        /// </summary>
        /// <param name="id">The ID of the interview to retrieve.</param>
        /// <returns>An asynchronous task that represents the operation, containing the interview if found, or null if not found.</returns>
        public async Task<Interviews?> GetByIdAsync(int id)
        {
            return await _context.Interviews.FindAsync(id);
        }

        /// <summary>
        /// Updates an existing interview asynchronously.
        /// </summary>
        /// <param name="interview">The updated interview data.</param>
        public async Task UpdateAsync(Interviews interview)
        {
            var existingInterview = await _context.Interviews.FindAsync(interview.Id);
            if (existingInterview != null)
            {
                // Update the properties of the existing entity
                existingInterview.ApplicantId = interview.ApplicantId;
                existingInterview.InterviewerId = interview.InterviewerId;
                existingInterview.InterviewType = interview.InterviewType;
                existingInterview.Results = interview.Results;
                existingInterview.InterviewDate = interview.InterviewDate;
                existingInterview.TimeStart = interview.TimeStart;
                existingInterview.TimeEnd = interview.TimeEnd;

                // Save the changes
                await _context.SaveChangesAsync();
            }
        }

        /// <summary>
        /// Deletes an interview by its ID asynchronously.
        /// </summary>
        /// <param name="id">The ID of the interview to delete.</param>
        public async Task DeleteAsync(int id)
        {
            var data = await _context.Interviews.FindAsync(id);
            if (data != null)
            {
                _context.Interviews.Remove(data);
                await _context.SaveChangesAsync();
            }
        }

        /// <summary>
        /// this gets the list of jobs the applicant applied for
        /// </summary>
        /// <param name="applicantId">the applicant id set to jobs</param>
        /// <returns>returns the jobs of an applicant</returns>
        public async Task<IQueryable<Interviews>> GetByApplicantIdAsync(int applicantId)
        {
            return await Task.FromResult(this.GetDbSet<Interviews>().Where(e => e.ApplicantId == applicantId).Include(e => e.Interviewer).Include(e => e.Applicant));
        }

        /// <summary>
        /// Retrieves a collection of interviews for a specific interviewer and date asynchronously.
        /// </summary>
        /// <param name="interviewerId">The ID of the interviewer.</param>
        /// <param name="interviewDate">The date of the interviews to retrieve.</param>
        /// <returns>An asynchronous task that represents the operation, containing the collection of interviews.</returns>
        public async Task<IEnumerable<Interviews>> GetInterviewsByInterviewerAndDateAsync(int interviewerId, DateTime interviewDate)
        {
            return await _context.Interviews
                .Where(i => i.InterviewerId == interviewerId && i.InterviewDate == interviewDate)
                .ToListAsync();
        }

        /// <summary>
        /// Retrieves a collection of interviews for a specific interviewer asynchronously.
        /// </summary>
        /// <param name="interviewerId">The ID of the interviewer.</param>
        /// <returns>An asynchronous task that represents the operation, containing the collection of interviews.</returns>
        public async Task<IEnumerable<Interviews>> GetInterviewsByInterviewerAsync(int interviewerId)
        {
            return await _context.Interviews
                .Where(i => i.InterviewerId == interviewerId)
                .ToListAsync();
        }

        /// <summary>
        /// Retrieves a collection of interviews for a specific applicant asynchronously.
        /// </summary>
        /// <param name="applicantId">The ID of the applicant.</param>
        /// <returns>An asynchronous task that represents the operation, containing the collection of interviews.</returns>
        public async Task<IEnumerable<Interviews>> GetInterviewsByApplicantAsync(int applicantId)
        {
            return await _context.Interviews
                .Where(i => i.ApplicantId == applicantId)
                .ToListAsync();
        }
    }
}