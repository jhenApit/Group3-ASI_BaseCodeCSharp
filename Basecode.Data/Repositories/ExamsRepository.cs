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

        public async Task<IQueryable<Exams>> RetrieveAllAsync()
        {
            return await Task.FromResult(this.GetDbSet<Exams>());
        }

        public async Task AddAsync(Exams Exams)
        {
            await _context.Exams.AddAsync(Exams);
            await _context.SaveChangesAsync();
        }

        public async Task<Exams?> GetByIdAsync(int id)
        {
            return await _context.Exams.FindAsync(id);
        }

        public async Task UpdateAsync(Exams Exams)
        {
            var existingExams = await _context.Exams.FindAsync(Exams.Id);
            if (existingExams != null)
            {
                // Update the properties of the existing entity
                existingExams.ApplicantId = Exams.ApplicantId;
                existingExams.ProctorId = Exams.ProctorId;
                existingExams.ExamType = Exams.ExamType;
                existingExams.Results = Exams.Results;
                existingExams.ExamDate = Exams.ExamDate;

                // Save the changes
                await _context.SaveChangesAsync();
            }

        }
        public async Task DeleteAsync(int id)
        {
            var data = await _context.Exams.FindAsync(id);
            if (data != null)
            {
                _context.Exams.Remove(data);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<Exams?> GetByApplicantIdAsync(int applicantId)
        {
            return await _context.Exams.FirstOrDefaultAsync(e => e.ApplicantId == applicantId);
        }

    }
}
