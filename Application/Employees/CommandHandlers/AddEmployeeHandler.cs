using Application.Abstractions;
using Application.Employees.Commands;
using Domain.Models;
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
        private readonly IEmpRepository _employeeRepository;
        public AddEmployeeHandler(IEmpRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public Task<Employee> Handle(AddEmployee request, CancellationToken cancellationToken)
        {
            return _employeeRepository.AddEmployeeAsync(request.employee);
        }
    }
}
