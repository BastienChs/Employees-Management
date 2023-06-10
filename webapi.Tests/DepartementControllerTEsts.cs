using Application.Employees.Queries;
using Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using webapi.Controllers;
using webapi.Tests.Mock;

namespace webapi.Tests
{
    internal class DepartementControllerTests
    {
        [Test]
        public void Get_Should_Return_A_List_Of_Departments()
        {
            //Arrange
            //We want to mock a list of department
            List<Department> mockedDepartments = DepartmentMocks.GetDepartments();
            var mockMediator = new Mock<IMediator>();
            mockMediator.Setup(m => m.Send(It.IsAny<GetAllDepartments>(), default(CancellationToken)))
                .ReturnsAsync(mockedDepartments);
            var controller = new DepartmentController(mockMediator.Object);

            //Act
            var result = controller.Get();

            //Assert
            Assert.That(result, Is.InstanceOf<Task<IActionResult>>());
            var okResult = result.Result as OkObjectResult;
            Assert.That(okResult, Is.Not.Null);
            Assert.That(okResult.Value, Is.InstanceOf<List<Department>>());
            var departments = okResult.Value as List<Department>;
            Assert.That(departments.Count, Is.EqualTo(mockedDepartments.Count));
            for (int i = 0; i < mockedDepartments.Count; i++)
            {
                Assert.That(CustomMockExtensions.Equals(departments[i], mockedDepartments[i]));
            }
        }

        [Test]
        public void Get_Should_Return_Empty_List_When_No_Departments()
        {
            //Arrange
            List<Department> mockedDepartments = new List<Department>();
            var mockMediator = new Mock<IMediator>();
            mockMediator.Setup(m => m.Send(It.IsAny<GetAllDepartments>(), default(CancellationToken)))
                .ReturnsAsync(mockedDepartments);
            var controller = new DepartmentController(mockMediator.Object);

            //Act
            var result = controller.Get();

            //Assert
            Assert.That(result, Is.InstanceOf<Task<IActionResult>>());
            var okResult = result.Result as OkObjectResult;
            Assert.That(okResult, Is.Not.Null);
            Assert.That(okResult.Value, Is.InstanceOf<List<Department>>());
            var departments = okResult.Value as List<Department>;
            Assert.IsEmpty(departments);
        }

        [Test]
        public void Get_Should_Return_An_Internal_Error_When_Exception_Raised()
        {
            //Arrange
            Exception exception = new Exception("An error has been raised.");
            var mockMediator = new Mock<IMediator>();
            mockMediator.Setup(m => m.Send(It.IsAny<GetAllDepartments>(), default(CancellationToken)))
                .Throws(exception);
            var controller = new DepartmentController(mockMediator.Object);
            
            //Act
            var result = controller.Get();

            //Assert
            Assert.That(result, Is.InstanceOf<Task<IActionResult>>());
            var objectResult = result.Result as ObjectResult;
            Assert.That(objectResult, Is.Not.Null);
            Assert.That(objectResult.StatusCode, Is.EqualTo(500));
            Assert.That(objectResult.Value, Is.EqualTo(exception.Message));
        }
    }
}
