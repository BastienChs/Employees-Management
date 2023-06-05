using Application.Abstractions;
using Application.Employees.Queries;
using Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Employees.QueryHandlers
{
    public class GetEmployeeByIdHandler : IRequestHandler<GetEmployeeById, Employee>
    {
        private readonly IEmployeeRepository _employeeRepository;
        public GetEmployeeByIdHandler(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public Task<Employee> Handle(GetEmployeeById request, CancellationToken cancellationToken)
        {
            return _employeeRepository.GetEmployeeAsync(request.Id);
        }
    }
}
