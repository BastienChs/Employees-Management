using Application.Employees.Commands;
using Domain.Models;
using Domain.Repositories;
using Domain.Services.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Employees.CommandHandlers
{
    public class AddEmployeeHandler : IRequestHandler<AddEmployee, Employee>
    {
        private readonly IEmployeeService _employeeService;
        public AddEmployeeHandler(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        public Task<Employee> Handle(AddEmployee request, CancellationToken cancellationToken)
        {
            return _employeeService.AddEmployeeAsync(request.Employee);
        }
    }
}
