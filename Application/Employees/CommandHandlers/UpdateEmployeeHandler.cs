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
        private readonly IEmployeeRepository _employeeRepository;
        public UpdateEmployeeHandler(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public Task<Employee> Handle(UpdateEmployee request, CancellationToken cancellationToken)
        {
            var updatedEmployee = new Employee
            {
                Name = request.Name,
                JobTitle = request.JobTitle,
                ManagerId = request.ManagerId,
                HireDate = request.HireDate,
                Salary = request.Salary,
                Commission = request.Commission,
                DepartmentId = request.DepartmentId
            };
            return _employeeRepository.UpdateEmployeeAsync(updatedEmployee);
        }
    }
}
