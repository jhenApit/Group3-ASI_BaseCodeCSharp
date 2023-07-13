using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Basecode.Data.Interfaces;
using Basecode.Data.Models;

namespace Basecode.Data.Repositories
{
    public class ExamsRepository : BaseRepository, IExamsRepository
    {
        private readonly BasecodeContext _context;
        public ExamsRepository(IUnitOfWork unitOfWork, BasecodeContext context) : base(unitOfWork)
        {
            _context = context;
        }

        public IQueryable<Exams> RetrieveAll()
        {
            return this.GetDbSet<Exams>();
        }

        public void Add(Exams Exams)
        {
            _context.Exams.Add(Exams);
            _context.SaveChanges();
        }

        public Exams? GetById(int id)
        {
            return _context.Exams.Find(id);
        }

        public void Update(Exams Exams)
        {
            var existingExams = _context.Exams.Find(Exams.Id);
            if (existingExams != null)
            {
                // Update the properties of the existing entity
                existingExams.ApplicantId = Exams.ApplicantId;
                existingExams.ProctorId = Exams.ProctorId;
                existingExams.ExamType = Exams.ExamType;
                existingExams.Results = Exams.Results;
                existingExams.ExamDate = Exams.ExamDate;

                // Save the changes
                _context.SaveChanges();
            }

        }
        public void Delete(int id)
        {
            var data = _context.Exams.Find(id);
            if (data != null)
            {
                _context.Exams.Remove(data);
                _context.SaveChanges();
            }
        }

        public Exams? GetByApplicantId(int applicantId)
        {
            return _context.Exams.FirstOrDefault(e => e.ApplicantId == applicantId);
        }

    }
}
