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
    public class UpdateEmployeeHandler : IRequestHandler<UpdateEmployee, Employee>
    {
        private readonly IEmployeeService _employeeRepository;
        public UpdateEmployeeHandler(IEmployeeService employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public Task<Employee> Handle(UpdateEmployee request, CancellationToken cancellationToken)
        {
            return _employeeRepository.UpdateEmployeeAsync(request.employee);
        }
    }
}
