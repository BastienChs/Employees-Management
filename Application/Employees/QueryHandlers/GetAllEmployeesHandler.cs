using Application.Employees.Queries;
using Domain.Models;
using Domain.Repositories;
using Domain.Services.Implementations;
using Domain.Services.Interfaces;
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
        private readonly IEmployeeService _employeeService;
        public GetAllEmployeesHandler(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        public Task<List<Employee>> Handle(GetAllEmployees request, CancellationToken cancellationToken)
        {
            return _employeeService.GetEmployeesAsync();
        }
    }
}
