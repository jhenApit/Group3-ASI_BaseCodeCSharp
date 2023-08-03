using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Basecode.Data.Dtos;
using Basecode.Data.Models;
using Basecode.Data.Interfaces;
using Basecode.Services.Interfaces;
using Basecode.Services.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Routing;
using Moq;

namespace G3HAS_Unit_Tests.Services
{
	public class UserServiceTests
	{
		private UserService _userService;
		private Mock<IUserRepository> _mockUserRepository;
		private Mock<IMapper> _mockMapper;

		public UserServiceTests()
		{
			_mockUserRepository = new Mock<IUserRepository>();
			_userService = new UserService(_mockUserRepository.Object);
			_mockMapper = new Mock<IMapper>();
		}

		[Fact]
		public void FindByUsername_ValidUsername_ReturnsUser()
		{
			// Arrange
			string username = "testUser";
			var expectedUser = new User { Username = username };
			_mockUserRepository.Setup(repo => repo.FindByUsername(username)).Returns(expectedUser);

			// Act
			var user = _userService.FindByUsername(username);

			// Assert
			Assert.NotNull(user);
			Assert.Equal(expectedUser.Username, user.Username);
		}

		[Fact]
		public void FindByUsername_NonExistentUsername_ReturnsNull()
		{
			// Arrange
			string nonExistentUsername = "nonExistentUser";
			_mockUserRepository.Setup(repo => repo.FindByUsername(nonExistentUsername)).Returns((User)null);

			// Act
			var user = _userService.FindByUsername(nonExistentUsername);

			// Assert
			Assert.Null(user);
		}

		[Fact]
		public void FindById_ValidId_ReturnsUser()
		{
			// Arrange
			string userId = "12345";
			var expectedUser = new User { Id = userId };
			_mockUserRepository.Setup(repo => repo.FindById(userId)).Returns(expectedUser);

			// Act
			var user = _userService.FindById(userId);

			// Assert
			Assert.NotNull(user);
			Assert.Equal(expectedUser.Id, user.Id);
		}

		[Fact]
		public void FindById_NonExistentId_ReturnsNull()
		{
			// Arrange
			string nonExistentId = "98765";
			_mockUserRepository.Setup(repo => repo.FindById(nonExistentId)).Returns((User)null);

			// Act
			var user = _userService.FindById(nonExistentId);

			// Assert
			Assert.Null(user);
		}

		[Fact]
		public void FindUser_ValidUserName_ReturnsIdentityUser()
		{
			// Arrange
			string username = "testUser";
			var expectedIdentityUser = new IdentityUser { UserName = username };
			_mockUserRepository.Setup(repo => repo.FindUser(username)).Returns(expectedIdentityUser);

			// Act
			var identityUser = _userService.FindUser(username);

			// Assert
			Assert.NotNull(identityUser);
			Assert.Equal(expectedIdentityUser.UserName, identityUser.UserName);
		}

		[Fact]
		public void FindUser_NonExistentUserName_ReturnsNull()
		{
			// Arrange
			string nonExistentUsername = "nonExistentUser";
			_mockUserRepository.Setup(repo => repo.FindUser(nonExistentUsername)).Returns((IdentityUser)null);

			// Act
			var identityUser = _userService.FindUser(nonExistentUsername);

			// Assert
			Assert.Null(identityUser);
		}

		[Fact]
		public void FindAll_ValidUsers_ReturnsAllUsers()
		{
			// Arrange
			var expectedUsers = new List<User>
			{
				new User { Id = "1", Username = "user1" },
				new User { Id = "2", Username = "user2" }
			};
			_mockUserRepository.Setup(repo => repo.FindAll()).Returns(expectedUsers);

			// Act
			var users = _userService.FindAll();

			// Assert
			Assert.NotNull(users);
			Assert.Equal(expectedUsers.Count, users.Count());
		}

		[Fact]
		public void Create_ValidUser_ReturnsTrue()
		{
			// Arrange
			var user = new User { Username = "testUser" };
			_mockUserRepository.Setup(repo => repo.Create(user)).Returns(true);

			// Act
			var result = _userService.Create(user);

			// Assert
			Assert.True(result);
		}

		[Fact]
		public void Create_InvalidUser_ReturnsFalse()
		{
			// Arrange
			var user = new User { Username = "testUser" };
			_mockUserRepository.Setup(repo => repo.Create(user)).Returns(false);

			// Act
			var result = _userService.Create(user);

			// Assert
			Assert.False(result);
		}

		[Fact]
		public void Update_ValidUser_ReturnsTrue()
		{
			// Arrange
			var user = new User { Id = "12345", Username = "testUser" };
			_mockUserRepository.Setup(repo => repo.Update(user)).Returns(true);

			// Act
			var result = _userService.Update(user);

			// Assert
			Assert.True(result);
		}

		[Fact]
		public void Update_InvalidUser_ReturnsFalse()
		{
			// Arrange
			var user = new User { Id = "12345", Username = "testUser" };
			_mockUserRepository.Setup(repo => repo.Update(user)).Returns(false);

			// Act
			var result = _userService.Update(user);

			// Assert
			Assert.False(result);
		}

		[Fact]
		public void Delete_ValidUser_DeletesSuccessfully()
		{
			// Arrange
			var user = new User { Id = "12345", Username = "testUser" };
			_mockUserRepository.Setup(repo => repo.Delete(user));

			// Act
			_userService.Delete(user);

			// Assert - Verify that the Delete method of the repository was called with the correct user.
			_mockUserRepository.Verify(repo => repo.Delete(user), Times.Once);
		}

		[Fact]
		public async Task FindUserAsync_ValidData_ReturnsIdentityUser()
		{
			// Arrange
			string username = "testUser";
			string password = "testPassword";
			var expectedIdentityUser = new IdentityUser { UserName = username };
			_mockUserRepository.Setup(repo => repo.FindUserAsync(username, password)).ReturnsAsync(expectedIdentityUser);

			// Act
			var identityUser = await _userService.FindUserAsync(username, password);

			// Assert
			Assert.NotNull(identityUser);
			Assert.Equal(expectedIdentityUser.UserName, identityUser.UserName);
		}

		[Fact]
		public async Task RegisterUser_ValidData_ReturnsIdentityResultSuccess()
		{
			// Arrange
			string username = "testUser";
			string password = "testPassword";
			string firstName = "John";
			string lastName = "Doe";
			string email = "john.doe@example.com";
			string role = "Admin";

			var expectedIdentityResult = IdentityResult.Success;
			_mockUserRepository.Setup(repo => repo.RegisterUser(username, password, firstName, lastName, email, role)).ReturnsAsync(expectedIdentityResult);

			// Act
			var identityResult = await _userService.RegisterUser(username, password, firstName, lastName, email, role);

			// Assert
			Assert.Equal(IdentityResult.Success, identityResult);
		}

		[Fact]
		public async Task CreateRole_ValidRole_ReturnsIdentityResultSuccess()
		{
			// Arrange
			string roleName = "Administrator";
			var expectedIdentityResult = IdentityResult.Success;
			_mockUserRepository.Setup(repo => repo.CreateRole(roleName)).ReturnsAsync(expectedIdentityResult);

			// Act
			var identityResult = await _userService.CreateRole(roleName);

			// Assert
			Assert.Equal(IdentityResult.Success, identityResult);
		}

		[Fact]
		public async Task FindUser_ValidCredentials_ReturnsIdentityUser()
		{
			// Arrange
			string username = "testUser";
			string password = "testPassword";
			var expectedIdentityUser = new IdentityUser { UserName = username };

			// Setup mock repository behavior
			_mockUserRepository.Setup(repo => repo.FindUser(username, password)).ReturnsAsync(expectedIdentityUser);

			// Act
			var identityUser = await _userService.FindUser(username, password);

			// Assert
			Assert.NotNull(identityUser);
			Assert.Equal(expectedIdentityUser.UserName, identityUser.UserName);
		}

		[Fact]
		public async Task FindUser_InvalidCredentials_ReturnsNull()
		{
			// Arrange
			string username = "testUser";
			string password = "testPassword";
			_mockUserRepository.Setup(repo => repo.FindUser(username, password)).ReturnsAsync((IdentityUser)null);

			// Act
			var identityUser = await _userService.FindUser(username, password);

			// Assert
			Assert.Null(identityUser);
		}
	}
}