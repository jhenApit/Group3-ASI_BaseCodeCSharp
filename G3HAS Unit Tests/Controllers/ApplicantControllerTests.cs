using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Basecode.Data.Models;
using Basecode.Services.Interfaces;
using Basecode.WebApp.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace G3HAS_Unit_Tests.Controllers
{
	public class ApplicantControllerTests
	{
		private readonly Mock<IApplicantService> _applicantServiceMock;
		private readonly Mock<ICurrentHiresService> _currentHiresServiceMock;
		private readonly Mock<IJobPostingsService> _jobPostingsServiceMock;
		private readonly Mock<ISendEmailService> _sendEmailServiceMock;
		private readonly Mock<IMapper> _mapperMock;
		private readonly Mock<IInterviewsService> _interviewsServiceMock;
		private readonly Mock<ICharacterReferencesService> _characterServiceMock;

		private readonly ApplicantController _controller;

		public ApplicantControllerTests()
		{
			_characterServiceMock = new Mock<ICharacterReferencesService>();
			_applicantServiceMock = new Mock<IApplicantService>();
			_currentHiresServiceMock = new Mock<ICurrentHiresService>();
			_jobPostingsServiceMock = new Mock<IJobPostingsService>();
			_sendEmailServiceMock = new Mock<ISendEmailService>();
			_mapperMock = new Mock<IMapper>();
			_interviewsServiceMock = new Mock<IInterviewsService>();

			_controller = new ApplicantController(
				Mock.Of<IErrorHandling>(), // Mock other services as needed
				_currentHiresServiceMock.Object,
				_jobPostingsServiceMock.Object,
				_applicantServiceMock.Object,
				Mock.Of<IAddressService>(),
				Mock.Of<ICharacterReferencesService>(),
				_sendEmailServiceMock.Object,
				_mapperMock.Object,
				_interviewsServiceMock.Object);
		}

		[Fact]
		public async Task ApplicationStatus_ShouldReturnViewWithApplicantData()
		{
			// Arrange
			var applicantId = "123";
			var applicantData = new Applicants { Id = 1, ApplicantId = applicantId };

			_applicantServiceMock.Setup(service => service.GetByApplicantIdAsync(applicantId))
				.ReturnsAsync(applicantData);

			// Act
			var result = await _controller.ApplicationStatus(applicantId);

			// Assert
			var viewResult = Assert.IsType<ViewResult>(result);
			Assert.Equal(applicantData, viewResult.Model);
		}

		[Fact]
		public void TrackApplication_ShouldReturnView_WhenFromIsApplication()
		{
			// Arrange
			var from = "application";

			// Act
			var result = _controller.TrackApplication(from);

			// Assert
			var viewResult = Assert.IsType<ViewResult>(result);
			Assert.True((bool)viewResult.ViewData["IsFromApplication"]);
		}

		[Fact]
		public void TrackApplication_ShouldReturnView_WhenFromIsNotApplication()
		{
			// Arrange
			var from = "other"; // Replace with a value that is not "application"

			// Act
			var result = _controller.TrackApplication(from);

			// Assert
			var viewResult = Assert.IsType<ViewResult>(result);
			Assert.False((bool)viewResult.ViewData["IsFromApplication"]);
		}

		[Fact]
		public void TrackApplication_ShouldReturnBadRequest_WhenExceptionOccurs()
		{
			// Arrange
			var from = "application";
			_controller.ControllerContext = new ControllerContext();
			_controller.ControllerContext.HttpContext = new DefaultHttpContext();
			_controller.ControllerContext.HttpContext.Request.Headers["X-Requested-With"] = "XMLHttpRequest";

			_applicantServiceMock.Setup(service => service.GetByApplicantIdAsync(It.IsAny<string>()))
				.Throws(new Exception("Simulated error"));

			// Act
			var result = _controller.TrackApplication(from);

			// Assert
			var viewResult = Assert.IsType<ViewResult>(result);
			Assert.Null(viewResult.ViewName); // ViewName is null for the default view
			Assert.True(viewResult.ViewData.ContainsKey("ErrorMessage"));
			Assert.Equal("An error occured when trying to access track application", viewResult.ViewData["ErrorMessage"]);
		}
	}
}
