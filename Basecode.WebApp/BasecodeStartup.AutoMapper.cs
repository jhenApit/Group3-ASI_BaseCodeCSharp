using AutoMapper;
using Basecode.Data.Dtos;
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
            });

            services.AddSingleton(Config.CreateMapper());
        }
    }
}