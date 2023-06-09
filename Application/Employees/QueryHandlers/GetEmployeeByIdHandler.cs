﻿using Application.Employees.Queries;
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
    public class GetEmployeeByIdHandler : IRequestHandler<GetEmployeeById, Employee>
    {
        private readonly IEmployeeService _employeeRepository;
        public GetEmployeeByIdHandler(IEmployeeService employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public Task<Employee> Handle(GetEmployeeById request, CancellationToken cancellationToken)
        {
            return _employeeRepository.GetEmployeeAsync(request.Id);
        }
    }
}
