using Application.Employees.Commands;
using Application.Employees.Queries;
using Domain.Models;
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
            Assert.That(result, Is.InstanceOf<Task<IActionResult>>());
            var returnedTask = result as Task<IActionResult>;
            Assert.That(returnedTask.Result, Is.InstanceOf<OkObjectResult>());
            var returnedResult = returnedTask.Result as OkObjectResult;
            Assert.That(returnedResult.Value, Is.InstanceOf<List<EmployeeWithManager>>());
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
            Assert.That(result, Is.InstanceOf<Task<IActionResult>>());
            var returnedTask = result as Task<IActionResult>;
            Assert.That(returnedTask.Result, Is.InstanceOf<OkObjectResult>());
            var returnedResult = returnedTask.Result as OkObjectResult;
            Assert.That(returnedResult.Value, Is.InstanceOf<List<EmployeeWithManager>>());
            var employees = returnedResult.Value as List<EmployeeWithManager>;
            Assert.Multiple(() =>
            {

                //We should retrieve mostly the same values as the mocked list, except for the new propert Manager
                Assert.That(Mocking.GetEmployees()[0].Id, Is.EqualTo(employees[0].Id));
                Assert.That(Mocking.GetEmployees()[0].Name, Is.EqualTo(employees[0].Name));
                Assert.That(Mocking.GetEmployees()[0].JobTitle, Is.EqualTo(employees[0].JobTitle));
                Assert.That(Mocking.GetEmployees()[0].ManagerId, Is.EqualTo(employees[0].ManagerId));
                Assert.That(Mocking.GetEmployees()[0].HireDate, Is.EqualTo(employees[0].HireDate));
                Assert.That(Mocking.GetEmployees()[0].Salary, Is.EqualTo(employees[0].Salary));
                Assert.That(Mocking.GetEmployees()[0].Commission, Is.EqualTo(employees[0].Commission));
                Assert.That(Mocking.GetEmployees()[0].DepartmentId, Is.EqualTo(employees[0].DepartmentId));
            });

            //We should have a manager which is the second employee of the mocked list
            Assert.That(employees[0].Manager, Is.Not.Null);
            Assert.That(CustomMockExtensions.Equals(employees[0].Manager, Mocking.GetEmployees()[1]), Is.EqualTo(true));
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
            Assert.That(result, Is.InstanceOf<Task<IActionResult>>());
            var returnedTask = result as Task<IActionResult>;
            Assert.That(returnedTask.Result, Is.InstanceOf<OkObjectResult>());
            var returnedResult = returnedTask.Result as OkObjectResult;
            Assert.That(returnedResult.Value, Is.InstanceOf<List<EmployeeWithManager>>());
            var employees = returnedResult.Value as List<EmployeeWithManager>;
            Assert.Multiple(() =>
            {

                //We should retrieve mostly the same values as the mocked list, except for the new propert Manager
                Assert.That(Mocking.GetEmployees()[1].Id, Is.EqualTo(employees[1].Id));
                Assert.That(Mocking.GetEmployees()[1].Name, Is.EqualTo(employees[1].Name));
                Assert.That(Mocking.GetEmployees()[1].JobTitle, Is.EqualTo(employees[1].JobTitle));
                Assert.That(employees[1].ManagerId, Is.Null);
                Assert.That(Mocking.GetEmployees()[1].HireDate, Is.EqualTo(employees[1].HireDate));
                Assert.That(Mocking.GetEmployees()[1].Salary, Is.EqualTo(employees[1].Salary));
                Assert.That(Mocking.GetEmployees()[1].Commission, Is.EqualTo(employees[1].Commission));
                Assert.That(Mocking.GetEmployees()[1].DepartmentId, Is.EqualTo(employees[1].DepartmentId));
                //We should have a manager which is the second employee of the mocked list
                Assert.That(employees[1].Manager, Is.Null);
            });
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
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.InstanceOf<Task<IActionResult>>());
            var returnedTask = result as Task<IActionResult>;
            Assert.That(returnedTask.Result, Is.InstanceOf<ObjectResult>());
            var returnedResult = returnedTask.Result as ObjectResult;
            Assert.That(returnedResult.StatusCode, Is.EqualTo(500));
        }

        //TODO: Test cases for Get(int id)
        // 1. It returns a 200 OK with the employee
        // 2. It returns a 404 Not Found when the employee is not found
        // 3. It returns a 500 Internal Error when an exception is raised

        [Test]
        public void GetById_Should_Return_An_Employee_When_Id_Exist()
        {
            //Arrange
            var mockMediator = new Mock<IMediator>();
            mockMediator.Setup(m => m.Send(It.IsAny<GetEmployeeById>(), default(CancellationToken)))
                .ReturnsAsync(Mocking.GetEmployees()[0]);
            var controller = new EmployeeController(mockMediator.Object);
            
            //Act
            var result = controller.Get(1);

            //Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.InstanceOf<Task<IActionResult>>());
            var returnedTask = result as Task<IActionResult>;
            Assert.That(returnedTask.Result, Is.InstanceOf<OkObjectResult>());
            var returnedResult = returnedTask.Result as OkObjectResult;
            Assert.That(returnedResult.Value, Is.InstanceOf<Employee>());
            var employee = returnedResult.Value as Employee;
            Assert.Multiple(() =>
            {
                Assert.That(employee, Is.Not.Null);
                Assert.That(Mocking.GetEmployees()[0].Id, Is.EqualTo(employee.Id));
                Assert.That(Mocking.GetEmployees()[0].Name, Is.EqualTo(employee.Name));
                Assert.That(Mocking.GetEmployees()[0].JobTitle, Is.EqualTo(employee.JobTitle));
                Assert.That(Mocking.GetEmployees()[0].ManagerId, Is.EqualTo(employee.ManagerId));
                Assert.That(Mocking.GetEmployees()[0].HireDate, Is.EqualTo(employee.HireDate));
                Assert.That(Mocking.GetEmployees()[0].Salary, Is.EqualTo(employee.Salary));
                Assert.That(Mocking.GetEmployees()[0].Commission, Is.EqualTo(employee.Commission));
                Assert.That(Mocking.GetEmployees()[0].DepartmentId, Is.EqualTo(employee.DepartmentId));
            });
        }

        [Test]
        public void GetById_Should_Return_Not_Found_Error_When_Id_Not_Exist()
        {
            //Arrange
            var mockMediator = new Mock<IMediator>();
            mockMediator.Setup(m => m.Send(It.IsAny<GetEmployeeById>(), default(CancellationToken)))
                .ReturnsAsync((Employee)null);
            var controller = new EmployeeController(mockMediator.Object);

            //Act
            var result = controller.Get(1);

            //Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.InstanceOf<Task<IActionResult>>());
            var returnedTask = result as Task<IActionResult>;
            Assert.That(returnedTask.Result, Is.TypeOf<NotFoundObjectResult>());
        }


        [Test]
        public void GetById_Should_Return_Internal_Error_When_Exception_Raised()
        {
            //Arrange
            var mockMediator = new Mock<IMediator>();
            mockMediator.Setup(m => m.Send(It.IsAny<GetEmployeeById>(), default(CancellationToken)))
                .Throws(new Exception("An exception has been raised"));
            var controller = new EmployeeController(mockMediator.Object);

            //Act
            var result = controller.Get(1);

            //Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.InstanceOf<Task<IActionResult>>());
            var returnedTask = result as Task<IActionResult>;
            Assert.That(returnedTask.Result, Is.InstanceOf<ObjectResult>());
            var returnedResult = returnedTask.Result as ObjectResult;
            Assert.That(returnedResult.StatusCode, Is.EqualTo(500));
        }

        //TODO: Test cases for Post(Employee employee)
        // 1. It returns a 201 Created with the employee
        // 2. It returns a 400 Bad Request when the employee is null
        // 3. It returns a 500 Internal Error when an exception is raised
        [Test]
        public void Post_Should_Return_A_Created_Employee_When_Employee_Is_Created()
        {
            //Arrange
            var mockMediator = new Mock<IMediator>();
            mockMediator.Setup(m => m.Send(It.IsAny<AddEmployee>(), default(CancellationToken)))
                .ReturnsAsync(Mocking.GetEmployees()[0]);
            var controller = new EmployeeController(mockMediator.Object);

            //Act
            var result = controller.Post(Mocking.GetNewEmployee());

            //Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.InstanceOf<Task<IActionResult>>());
            var returnedTask = result as Task<IActionResult>;
            Assert.That(returnedTask.Result, Is.InstanceOf<CreatedAtActionResult>());
            var returnedResult = returnedTask.Result as CreatedAtActionResult;
            Assert.That(returnedResult.Value, Is.InstanceOf<Employee>());
            var employee = returnedResult.Value as Employee;
            Assert.Multiple(() =>
            {
                Assert.That(employee, Is.Not.Null);
                //We should not check the Id because it is generated by the database
                Assert.That(Mocking.GetNewEmployee().Name, Is.EqualTo(employee.Name));
                Assert.That(Mocking.GetNewEmployee().JobTitle, Is.EqualTo(employee.JobTitle));
                Assert.That(Mocking.GetNewEmployee().ManagerId, Is.EqualTo(employee.ManagerId));
                Assert.That(Mocking.GetNewEmployee().HireDate, Is.EqualTo(employee.HireDate));
                Assert.That(Mocking.GetNewEmployee().Salary, Is.EqualTo(employee.Salary));
                Assert.That(Mocking.GetNewEmployee().Commission, Is.EqualTo(employee.Commission));
                Assert.That(Mocking.GetNewEmployee().DepartmentId, Is.EqualTo(employee.DepartmentId));
            });
        }

        [Test]
        public void Post_Should_Return_Bad_Request_When_Employee_Is_Null()
        {
            //Arrange
            var mockMediator = new Mock<IMediator>();
            mockMediator.Setup(m => m.Send(It.IsAny<AddEmployee>(), default(CancellationToken)))
                .ReturnsAsync((Employee)null);
            var controller = new EmployeeController(mockMediator.Object);

            //Act
            var result = controller.Post(null);

            //Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.InstanceOf<Task<IActionResult>>());
            var returnedTask = result as Task<IActionResult>;
            Assert.That(returnedTask.Result, Is.TypeOf<BadRequestObjectResult>());
        }

        [Test]
        public void Post_Should_Return_Internal_Error_When_Exception_Raised()
        {
              //Arrange
            var mockMediator = new Mock<IMediator>();
            mockMediator.Setup(m => m.Send(It.IsAny<AddEmployee>(), default(CancellationToken)))
                .Throws(new Exception("An exception has been raised"));
            var controller = new EmployeeController(mockMediator.Object);

            //Act
            var result = controller.Post(Mocking.GetNewEmployee());

            //Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.InstanceOf<Task<IActionResult>>());
            var returnedTask = result as Task<IActionResult>;
            Assert.That(returnedTask.Result, Is.InstanceOf<ObjectResult>());
            var returnedResult = returnedTask.Result as ObjectResult;
            Assert.That(returnedResult.StatusCode, Is.EqualTo(500));
        }

        //TODO: Test cases for Patch(int id, Employee employee)
        // 1. It returns a 200 OK with the employee
        // 2. It returns a 400 Bad Request when the employee is null
        // 3. It returns a 404 Not Found when the employee is not found
        // 4. It returns a 500 Internal Error when an exception is raised
        [Test]
        public void Patch_Should_Return_An_Updated_Employee_When_Employee_Is_Updated()
        {
            //Arrange
            var mockMediator = new Mock<IMediator>();
            mockMediator.Setup(m => m.Send(It.IsAny<UpdateEmployee>(), default(CancellationToken)))
                .ReturnsAsync(Mocking.GetEmployees()[0]);
            var controller = new EmployeeController(mockMediator.Object);

            //Act
            var result = controller.Patch(Mocking.GetEmployees()[0]);

            //Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.InstanceOf<Task<IActionResult>>());
            var returnedTask = result as Task<IActionResult>;
            Assert.That(returnedTask.Result, Is.InstanceOf<CreatedAtActionResult>());
            var returnedResult = returnedTask.Result as CreatedAtActionResult;
            Assert.That(returnedResult.Value, Is.InstanceOf<Employee>());
            var employee = returnedResult.Value as Employee;
            Assert.Multiple(() =>
            {
                Assert.That(employee, Is.Not.Null);
                Assert.That(employee.Id, Is.EqualTo(1));
                Assert.That(Mocking.GetEmployees()[0].Name, Is.EqualTo(employee.Name));
                Assert.That(Mocking.GetEmployees()[0].JobTitle, Is.EqualTo(employee.JobTitle));
                Assert.That(Mocking.GetEmployees()[0].ManagerId, Is.EqualTo(employee.ManagerId));
                Assert.That(Mocking.GetEmployees()[0].HireDate, Is.EqualTo(employee.HireDate));
                Assert.That(Mocking.GetEmployees()[0].Salary, Is.EqualTo(employee.Salary));
                Assert.That(Mocking.GetEmployees()[0].Commission, Is.EqualTo(employee.Commission));
                Assert.That(Mocking.GetEmployees()[0].DepartmentId, Is.EqualTo(employee.DepartmentId));
            });
        }

        [Test]
        public void Patch_Should_Return_Bad_Request_When_Employee_Is_Null()
        {
            //Arrange
            var mockMediator = new Mock<IMediator>();
            mockMediator.Setup(m => m.Send(It.IsAny<UpdateEmployee>(), default(CancellationToken)))
                .ReturnsAsync((Employee)null);
            var controller = new EmployeeController(mockMediator.Object);

            //Act
            var result = controller.Patch(null);

            //Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.InstanceOf<Task<IActionResult>>());
            var returnedTask = result as Task<IActionResult>;
            Assert.That(returnedTask.Result, Is.TypeOf<BadRequestObjectResult>());
        }

        [Test]
        public void Patch_Should_Return_Not_Found_When_Employee_Is_Not_Found()
        {
            //Arrange
            var mockMediator = new Mock<IMediator>();
            mockMediator.Setup(m => m.Send(It.IsAny<UpdateEmployee>(), default(CancellationToken)))
                .ReturnsAsync((Employee)null);
            var controller = new EmployeeController(mockMediator.Object);

            //Act
            var result = controller.Patch(Mocking.GetEmployees()[0]);

            //Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.InstanceOf<Task<IActionResult>>());
            var returnedTask = result as Task<IActionResult>;
            Assert.That(returnedTask.Result, Is.TypeOf<NotFoundObjectResult>());
        }

        [Test]
        public void Patch_Should_Return_Internal_Error_When_Exception_Raised()
        {
            //Arrange
            var mockMediator = new Mock<IMediator>();
            mockMediator.Setup(m => m.Send(It.IsAny<UpdateEmployee>(), default(CancellationToken)))
                .Throws(new Exception("An exception has been raised"));
            var controller = new EmployeeController(mockMediator.Object);

            //Act
            var result = controller.Patch(Mocking.GetEmployees()[0]);

            //Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.InstanceOf<Task<IActionResult>>());
            var returnedTask = result as Task<IActionResult>;
            Assert.That(returnedTask.Result, Is.InstanceOf<ObjectResult>());
            var returnedResult = returnedTask.Result as ObjectResult;
            Assert.That(returnedResult.StatusCode, Is.EqualTo(500));
        }
    }
}