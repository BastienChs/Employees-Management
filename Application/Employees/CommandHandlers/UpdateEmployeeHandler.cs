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
    public class UpdateEmployeeHandler : IRequestHandler<UpdateEmployee, Employee>
    {
        private readonly IEmpRepository _employeeRepository;
        public UpdateEmployeeHandler(IEmpRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public Task<Employee> Handle(UpdateEmployee request, CancellationToken cancellationToken)
        {
            return _employeeRepository.UpdateEmployeeAsync(request.employee);
        }
    }
}
