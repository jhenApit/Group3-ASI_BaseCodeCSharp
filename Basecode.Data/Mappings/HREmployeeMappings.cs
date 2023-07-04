using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Basecode.Data.Dtos;
using Basecode.Data.Models;

namespace Basecode.Data.Mappings
{
    public class HREmployeeMappings : Profile
    {
        public HREmployeeMappings()
        {
            CreateMap<HREmployeeUpdationDto, HrEmployee>();
        }
    }
}
