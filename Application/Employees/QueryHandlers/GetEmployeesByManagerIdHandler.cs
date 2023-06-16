using Application.Employees.Queries;
using Domain.Models;
using Domain.Repositories;
using Domain.Services.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Employees.QueryHandlers
{
    public class GetEmployeesByManagerIdHandler : IRequestHandler<GetEmployeesByManagerId, List<Employee>>
    {
        private readonly IEmployeeService _employeeRepository;
        public GetEmployeesByManagerIdHandler(IEmployeeService employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public Task<List<Employee>> Handle(GetEmployeesByManagerId request, CancellationToken cancellationToken)
        {
            return _employeeRepository.GetEmployeesByManagerIdAsync(request.Id);
        }
    }
}
