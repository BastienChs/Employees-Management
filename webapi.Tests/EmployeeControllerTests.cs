using Application.Employees.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using webapi.Controllers;
using webapi.Models;
using webapi.Tests.Mock;

namespace webapi.Tests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        //We want to test all methods contained in our controllers
        //extensions will be tested through the controller tests

        [Test]
        public void Get_Should_Return_A_List_Of_Employees()
        {
            //Arrange

            //We want to mock a list of employee
            var mockMediator = new Mock<IMediator>();
            mockMediator.Setup(m => m.Send(It.IsAny<GetAllEmployees>(), default(CancellationToken)))
                .ReturnsAsync(Mocking.GetEmployees());

            //We want to mock the GetEmployeeById
            mockMediator.Setup(mockMediator => mockMediator.Send(It.IsAny<GetEmployeeById>(), default(CancellationToken)))
                .ReturnsAsync(Mocking.GetEmployees()[1]);

            var controller = new EmployeeController(mockMediator.Object);

            //Act
            var result = controller.Get();

            //Assert
            Assert.IsInstanceOf<Task<IActionResult>>(result);
            var returnedTask = result as Task<IActionResult>;
            Assert.IsInstanceOf<OkObjectResult>(returnedTask.Result);
            var returnedResult = returnedTask.Result as OkObjectResult;
            Assert.IsInstanceOf<List<EmployeeWithManager>>(returnedResult.Value);
            var employees = returnedResult.Value as List<EmployeeWithManager>;
        }

        [Test]
        public void Get_Should_Return_A_List_Of_Employees_With_A_Manager ()
        {
            //Arrange

            //We want to mock a list of employee
                var mockMediator = new Mock<IMediator>();
            mockMediator.Setup(m => m.Send(It.IsAny<GetAllEmployees>(), default(CancellationToken)))
                .ReturnsAsync(Mocking.GetEmployees());

            //We want to mock the GetEmployeeById
            mockMediator.Setup(mockMediator => mockMediator.Send(It.IsAny<GetEmployeeById>(), default(CancellationToken)))
                .ReturnsAsync(Mocking.GetEmployees()[1]);

            var controller = new EmployeeController(mockMediator.Object);

            //Act
            var result = controller.Get();

            //Assert
            Assert.IsInstanceOf<Task<IActionResult>>(result);
            var returnedTask = result as Task<IActionResult>;
            Assert.IsInstanceOf<OkObjectResult>(returnedTask.Result);
            var returnedResult = returnedTask.Result as OkObjectResult;
            Assert.IsInstanceOf<List<EmployeeWithManager>>(returnedResult.Value);
            var employees = returnedResult.Value as List<EmployeeWithManager>;

            //We should retrieve mostly the same values as the mocked list, except for the new propert Manager
            Assert.AreEqual(employees[0].Id, Mocking.GetEmployees()[0].Id);
            Assert.AreEqual(employees[0].Name, Mocking.GetEmployees()[0].Name);
            Assert.AreEqual(employees[0].JobTitle, Mocking.GetEmployees()[0].JobTitle);
            Assert.AreEqual(employees[0].ManagerId, Mocking.GetEmployees()[0].ManagerId);
            Assert.AreEqual(employees[0].HireDate, Mocking.GetEmployees()[0].HireDate);
            Assert.AreEqual(employees[0].Salary, Mocking.GetEmployees()[0].Salary);
            Assert.AreEqual(employees[0].Commission, Mocking.GetEmployees()[0].Commission);
            Assert.AreEqual(employees[0].DepartmentId, Mocking.GetEmployees()[0].DepartmentId);

            //We should have a manager which is the second employee of the mocked list
            Assert.NotNull(employees[0].Manager);
            Assert.AreEqual(true, CustomMockExtensions.Equals(employees[0].Manager, Mocking.GetEmployees()[1]));
        }

        [Test]
        public void Get_Should_Return_A_List_Of_Employees_Without_A_Manager()
        {
            //Arrange

            //We want to mock a list of employee
            var mockMediator = new Mock<IMediator>();
            mockMediator.Setup(m => m.Send(It.IsAny<GetAllEmployees>(), default(CancellationToken)))
                .ReturnsAsync(Mocking.GetEmployees());

            //We want to mock the GetEmployeeById
            mockMediator.Setup(mockMediator => mockMediator.Send(It.IsAny<GetEmployeeById>(), default(CancellationToken)))
                .ReturnsAsync(Mocking.GetEmployees()[1]);

            var controller = new EmployeeController(mockMediator.Object);

            //Act
            var result = controller.Get();

            //Assert
            Assert.IsInstanceOf<Task<IActionResult>>(result);
            var returnedTask = result as Task<IActionResult>;
            Assert.IsInstanceOf<OkObjectResult>(returnedTask.Result);
            var returnedResult = returnedTask.Result as OkObjectResult;
            Assert.IsInstanceOf<List<EmployeeWithManager>>(returnedResult.Value);
            var employees = returnedResult.Value as List<EmployeeWithManager>;

            //We should retrieve mostly the same values as the mocked list, except for the new propert Manager
            Assert.AreEqual(employees[1].Id, Mocking.GetEmployees()[1].Id);
            Assert.AreEqual(employees[1].Name, Mocking.GetEmployees()[1].Name);
            Assert.AreEqual(employees[1].JobTitle, Mocking.GetEmployees()[1].JobTitle);
            Assert.AreEqual(employees[1].ManagerId, null);
            Assert.AreEqual(employees[1].HireDate, Mocking.GetEmployees()[1].HireDate);
            Assert.AreEqual(employees[1].Salary, Mocking.GetEmployees()[1].Salary);
            Assert.AreEqual(employees[1].Commission, Mocking.GetEmployees()[1].Commission);
            Assert.AreEqual(employees[1].DepartmentId, Mocking.GetEmployees()[1].DepartmentId);

            //We should have a manager which is the second employee of the mocked list
            Assert.IsNull(employees[1].Manager);
        }

        [Test]
        //We have to test the case when an exception is raised
        public void Get_Should_Return_An_Internal_Error_When_Exception_Raised()
        {
            var mockMediator = new Mock<IMediator>();
            mockMediator.Setup(m => m.Send(It.IsAny<GetAllEmployees>(), default(CancellationToken)))
                .Throws(new Exception("An exception has been raised"));

            var controller = new EmployeeController(mockMediator.Object);

            //Act
            var result = controller.Get();

            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<Task<IActionResult>>(result);
            var returnedTask = result as Task<IActionResult>;
            Assert.IsInstanceOf<ObjectResult>(returnedTask.Result);
            var returnedResult = returnedTask.Result as ObjectResult;
            Assert.AreEqual(500, returnedResult.StatusCode);
        }
    }
}