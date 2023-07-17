using Basecode.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Basecode.Data.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace Basecode.Services.Services
{
    public class AdminService : IAdminService
    {
        private readonly IAdminRepository _adminRepository;
        public AdminService(IAdminRepository adminRepository)
        {
            _adminRepository = adminRepository;
        }
        public async Task<IdentityResult> CreateRole(string roleName)
        {
            return await _adminRepository.CreateRole(roleName);
        }
    }
}
