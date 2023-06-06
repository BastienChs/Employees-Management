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
    public class GetAllDepartmentsHandler : IRequestHandler<GetAllDepartments, List<Department>>
    {
        private readonly IDeptRepository _deptRepository;
        public GetAllDepartmentsHandler(IDeptRepository deptRepository)
        {
            _deptRepository = deptRepository;
        }

        public Task<List<Department>> Handle(GetAllDepartments request, CancellationToken cancellationToken)
        {
            return _deptRepository.GetDepartmentsAsync();
        }
    }
}
