using Basecode.WebApp.Controllers;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace G3HAS_Unit_Tests.Controllers
{
    public class HRControllerTests
    {
        [Fact]
        public void JobPostList_ReturnsViewResult()
        {
            // Arrange
            var controller = new HRController();

            // Act
            var result = controller.JobPostList();

            // Assert
            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void CreateJobPost_ReturnsViewResult()
        {
            // Arrange
            var controller = new HRController();

            // Act
            var result = controller.CreateJobPost();

            // Assert
            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void EditJobPost_ReturnsViewResult()
        {
            // Arrange
            var controller = new HRController();

            // Act
            var result = controller.EditJobPost();

            // Assert
            Assert.IsType<ViewResult>(result);
        }
    }
}
