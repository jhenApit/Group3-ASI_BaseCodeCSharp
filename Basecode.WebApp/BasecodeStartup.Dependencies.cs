using Basecode.WebApp.Authentication;
using Basecode.Data;
using Basecode.Data.Interfaces;
using Basecode.Data.Repositories;
using Basecode.Services.Interfaces;
using Basecode.Services.Services;
using Basecode.Services.Utils;
using Basecode.Data.Models;

namespace Basecode.WebApp
{
    public partial class BasecodeStartup
    {
        private void ConfigureDependencies(IServiceCollection services)
        {            
            // Common
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<ClaimsProvider, ClaimsProvider>();

            // Services 
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IHrEmployeeService, HrEmployeeService>();
            services.AddScoped<IJobPostingsService, JobPostingsService>();
            services.AddScoped<IApplicantService, ApplicantService>();
            services.AddScoped<IAdminService, AdminService>();
            services.AddTransient<IEmailService, EmailService>();
            services.AddScoped<IAddressService, AddressService>();
            services.AddScoped<ICharacterReferencesService, CharacterReferencesService>();
            services.AddScoped<EmailService>();
            services.AddScoped<EmailSender>();
            services.AddScoped<HrEmployee>();
            services.AddScoped<Applicant>();
            services.AddScoped<Interviewers>();
            services.AddScoped<CharacterReferences>();

            // Repositories
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IHrEmployeeRepository, HrEmployeeRepository>();
            services.AddScoped<IJobPostingsRepository, JobPostingsRepository>();
            services.AddScoped<IApplicantRepository, ApplicantRepository>();
            services.AddScoped<IAdminRepository, AdminRepository>();

        }
    }
}