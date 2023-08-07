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
    public class ExamsRepository : BaseRepository, IExamsRepository
    {
        private readonly BasecodeContext _context;

        public ExamsRepository(IUnitOfWork unitOfWork, BasecodeContext context) : base(unitOfWork)
        {
            _context = context;
        }

        /// <summary>
        /// Retrieves all exams from the database.
        /// </summary>
        /// <returns>An IQueryable containing all exams.</returns>
        public async Task<IQueryable<Exams>> RetrieveAllAsync()
        {
            return await Task.FromResult(this.GetDbSet<Exams>());
        }

        /// <summary>
        /// Adds an exam to the database asynchronously.
        /// </summary>
        /// <param name="exams">The exam to add.</param>
        /// <returns>Task representing the asynchronous operation.</returns>
        public async Task AddAsync(Exams exams)
        {
            await _context.Exams.AddAsync(exams);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Retrieves an exam from the database by its ID.
        /// </summary>
        /// <param name="id">The ID of the exam to retrieve.</param>
        /// <returns>The exam with the specified ID, or null if not found.</returns>
        public async Task<Exams?> GetByIdAsync(int id)
        {
            return await _context.Exams.FindAsync(id);
        }

        /// <summary>
        /// Updates an existing exam in the database asynchronously.
        /// </summary>
        /// <param name="exams">The updated exam object.</param>
        /// <returns>Task representing the asynchronous operation.</returns>
        public async Task UpdateAsync(Exams exams)
        {
            var existingExams = await _context.Exams.FindAsync(exams.Id);
            if (existingExams != null)
            {
                // Update the properties of the existing entity
                existingExams.ApplicantId = exams.ApplicantId;
                existingExams.ProctorId = exams.ProctorId;
                existingExams.ExamType = exams.ExamType;
                existingExams.Results = exams.Results;
                existingExams.ExamDate = exams.ExamDate;

                // Save the changes
                await _context.SaveChangesAsync();
            }
        }

        /// <summary>
        /// Deletes an exam from the database asynchronously.
        /// </summary>
        /// <param name="id">The ID of the exam to delete.</param>
        /// <returns>Task representing the asynchronous operation.</returns>
        public async Task DeleteAsync(int id)
        {
            var data = await _context.Exams.FindAsync(id);
            if (data != null)
            {
                _context.Exams.Remove(data);
                await _context.SaveChangesAsync();
            }
        }

        /// <summary>
        /// Retrieves an exam from the database by the applicant's ID.
        /// </summary>
        /// <param name="applicantId">The ID of the applicant.</param>
        /// <returns>The exam associated with the specified applicant ID, or null if not found.</returns>
        public async Task<Exams?> GetByApplicantIdAsync(int applicantId)
        {
            return await _context.Exams.FirstOrDefaultAsync(e => e.ApplicantId == applicantId);
        }
    }
}
