using Basecode.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Basecode.Data
{
    public class BasecodeContext : IdentityDbContext<IdentityUser>
    {
        public BasecodeContext(DbContextOptions<BasecodeContext> options)
            : base(options)
        { }

        public void InsertNew(RefreshToken token)
        {
            var tokenModel = RefreshToken.SingleOrDefault(i => i.Username == token.Username);
            if (tokenModel != null)
            {
                RefreshToken.Remove(tokenModel);
                SaveChanges();
            }
            RefreshToken.Add(token);
            SaveChanges();
        }

        public virtual DbSet<HrEmployee> HrEmployees { get; set; }
        public virtual DbSet<Applicants> Applicants { get; set; }
        public virtual DbSet<Addresses> Addresses { get; set; }
        public virtual DbSet<CharacterReferences> CharacterReferences { get; set; }
        public virtual DbSet<ReferenceForms> ReferenceForms { get; set; }
        public virtual DbSet<JobPostings> JobPostings { get; set; }
        public virtual DbSet<Interviewers> Interviewers { get; set; }
        public virtual DbSet<Interviews> Interviews { get; set; }
        public virtual DbSet<Exams> Exams { get; set; }
        public virtual DbSet<CurrentHires> CurrentHires { get; set; }
        public virtual DbSet<User> User { get; set; }

        public virtual DbSet<RefreshToken> RefreshToken { get; set; }
    }
}