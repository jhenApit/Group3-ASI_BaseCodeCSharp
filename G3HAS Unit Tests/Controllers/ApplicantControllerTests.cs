using Basecode.Data.Models;
using Basecode.Services.Interfaces;
using Basecode.WebApp.Controllers;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace G3HAS_Unit_Tests.Controllers
{
    public class ApplicantControllerTests
    {
        private readonly Mock<IApplicantService> _serviceMock;
        private readonly ApplicantController _controller;

        public ApplicantControllerTests()
        {
            _serviceMock = new Mock<IApplicantService>();
            _controller = new ApplicantController(_serviceMock.Object);
        }

        [Fact]
        public void TrackStatus_ReturnsViewWithApplicantData()
        {
            // Arrange
            int applicantId = 123;
            Applicant expectedData = new Applicant { /* initialize expected applicant data */ };
            _serviceMock.Setup(x => x.GetById(applicantId)).Returns(expectedData);

            // Act
            IActionResult result = _controller.TrackStatus(applicantId);

            // Assert
            ViewResult viewResult = Assert.IsType<ViewResult>(result);
            Assert.Equal(expectedData, viewResult.Model);
        }

        [Fact]
        public void ContactUs_ReturnsView()
        {
            // Act
            IActionResult result = _controller.ContactUs();

            // Assert
            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void TrackApplication_ReturnsView()
        {
            // Act
            IActionResult result = _controller.TrackApplication();

            // Assert
            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void ApplicationForm_ReturnsView()
        {
            // Act
            IActionResult result = _controller.ApplicationForm();

            // Assert
            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void TermsAndConditions_ReturnsView()
        {
            // Act
            IActionResult result = _controller.TermsAndConditions();

            // Assert
            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void JobDescription_ReturnsView()
        {
            // Act
            IActionResult result = _controller.JobDescription();

            // Assert
            Assert.IsType<ViewResult>(result);
        }
    }
}
