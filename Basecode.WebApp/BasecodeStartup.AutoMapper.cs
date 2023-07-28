using AutoMapper;
using Basecode.Data.Dtos;
using Basecode.Data.Dtos.HrEmployee;
using Basecode.Data.Dtos.JobPostings;
using Basecode.Data.Models;
using Basecode.Data.ViewModels;
using Microsoft.Extensions.DependencyInjection;

namespace Basecode.WebApp
{
    public partial class BasecodeStartup
    {
        private void ConfigureMapper(IServiceCollection services)
        {
            var Config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<HREmployeeCreationDto, HrEmployee>();
                cfg.CreateMap<HREmployeeUpdationDto, HrEmployee>();
                cfg.CreateMap<JobPostingsCreationDto, JobPostings>();
                cfg.CreateMap<JobPostingsUpdationDto, JobPostings>();
                cfg.CreateMap<ApplicantCreationDto, Applicants>();
				cfg.CreateMap<AddressCreationDto, Address>();
                cfg.CreateMap<CharacterReferencesCreationDto, CharacterReferences>();
                cfg.CreateMap<ReferenceFormsCreationDto, ReferenceForms>();
            });

            services.AddSingleton(Config.CreateMapper());
        }
    }
}