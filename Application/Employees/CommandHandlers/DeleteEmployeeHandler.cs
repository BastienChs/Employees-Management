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
    public class DeleteEmployeeHandler : IRequestHandler<DeleteEmployee, Employee>
    {
        private readonly IEmpRepository _employeeRepository;
        public DeleteEmployeeHandler(IEmpRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public Task<Employee> Handle(DeleteEmployee request, CancellationToken cancellationToken)
        {
            return _employeeRepository.DeleteEmployeeAsync(request.Id);
        }
    }
}
