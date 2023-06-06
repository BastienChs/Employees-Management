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
    public class GetAllEmployeesHandler : IRequestHandler<GetAllEmployees, List<Employee>>
    {
        private readonly IEmpRepository _employeeRepository;
        public GetAllEmployeesHandler(IEmpRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public Task<List<Employee>> Handle(GetAllEmployees request, CancellationToken cancellationToken)
        {
            return _employeeRepository.GetEmployeesAsync();
        }
    }
}
