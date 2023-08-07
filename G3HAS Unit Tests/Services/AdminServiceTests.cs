using System.Threading.Tasks;
using Basecode.Data.Interfaces;
using Basecode.Services.Interfaces;
using Basecode.Services.Services;
using Microsoft.AspNetCore.Identity;
using Moq;
using Xunit;

namespace G3HAS_Unit_Tests.Services
{
	public class AdminServiceTests
	{
		private AdminService _adminService;
		private Mock<IAdminRepository> _mockRepository;

		public AdminServiceTests()
		{
			_mockRepository = new Mock<IAdminRepository>();
			_adminService = new AdminService(_mockRepository.Object);
		}

		[Fact]
		public async Task CreateRole_ValidRoleName_ReturnsIdentityResultSuccess()
		{
			// Arrange
			string roleName = "TestRole";
			var expectedIdentityResult = IdentityResult.Success;

			_mockRepository.Setup(repo => repo.CreateRole(roleName)).ReturnsAsync(expectedIdentityResult);

			// Act
			var result = await _adminService.CreateRole(roleName);

			// Assert
			Assert.Equal(expectedIdentityResult, result);
		}
	}
}
