using Domain.Models;
using Domain.Repositories;
using Domain.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services.Implementations
{
    public class DepartmentService : IDepartmentService
    {
        private readonly IDeptRepository _deptRepository;
        public DepartmentService(IDeptRepository deptRepository)
        {
            _deptRepository = deptRepository;
        }

        public Task<List<Department>> GetDepartmentsAsync()
        {
            return _deptRepository.GetDepartmentsAsync();
        }
    }
}
