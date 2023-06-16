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
    public class DeleteEmployeeHandler : IRequestHandler<DeleteEmployee, Employee>
    {
        private readonly IEmployeeService _employeeRepository;
        public DeleteEmployeeHandler(IEmployeeService employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public Task<Employee> Handle(DeleteEmployee request, CancellationToken cancellationToken)
        {
            return _employeeRepository.DeleteEmployeeAsync(request.Id);
        }
    }
}
