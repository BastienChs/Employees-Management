using Application.Employees.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using webapi.Extensions;
using webapi.Models;

namespace webapi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeeController : ControllerBase
    {
        private readonly IMediator _mediator;
        public EmployeeController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Get()
        {
            List<EmployeeWithManager> employeesWithManagers = new List<EmployeeWithManager>();
            //We want to retrieve all employees and the manager of each employee
            var employees = await _mediator.Send(new GetAllEmployees());
            foreach(var employee in employees)
            {
                EmployeeWithManager employeeWithManager = new EmployeeWithManager();
                employeeWithManager = EmployeeExtensions.ToEmployeeWithManager(employee);

                if (employeeWithManager.ManagerId.HasValue)
                {
                    var manager = await _mediator.Send(new GetEmployeeById() { Id = employeeWithManager.ManagerId.Value });
                    employeeWithManager.Manager = manager;
                }
                employeesWithManagers.Add(employeeWithManager);
            }

            return Ok(employeesWithManagers);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var command = new GetEmployeeById();
            command.Id = id;
            var result = await _mediator.Send(command);
            return Ok(result);
        }
        
    }
}
