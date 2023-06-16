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
    public class GetAllDepartmentsHandler : IRequestHandler<GetAllDepartments, List<Department>>
    {
        private readonly IDepartmentService _deptRepository;
        public GetAllDepartmentsHandler(IDepartmentService deptRepository)
        {
            _deptRepository = deptRepository;
        }

        public Task<List<Department>> Handle(GetAllDepartments request, CancellationToken cancellationToken)
        {
            return _deptRepository.GetDepartmentsAsync();
        }
    }
}
