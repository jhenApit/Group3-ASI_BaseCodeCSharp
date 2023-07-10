using Basecode.Services.Interfaces;
using Basecode.WebApp.Controllers;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace G3HAS_Unit_Tests.Controllers
{
    public class LoginControllerTests
    {
        private readonly Mock<IHrEmployeeService> _serviceMock;
        private readonly LoginController _controller;

        public LoginControllerTests()
        {
            _serviceMock = new Mock<IHrEmployeeService>();
            _controller = new LoginController(_serviceMock.Object);
        }


    }
}
