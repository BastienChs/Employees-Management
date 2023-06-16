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
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmpRepository _empRepository;
        public EmployeeService(IEmpRepository empRepository)
        {
            _empRepository = empRepository;
        }

        public async Task<List<Employee>> GetEmployeesAsync()
        {
            return await _empRepository.GetEmployeesAsync();
        }

        public async Task<Employee> GetEmployeeAsync(int id)
        {
            return await _empRepository.GetEmployeeAsync(id);
        }

        public async Task<List<Employee>> GetEmployeesByManagerIdAsync(int managerId)
        {
            return await _empRepository.GetEmployeesByManagerIdAsync(managerId);
        }

        public async Task<Employee> AddEmployeeAsync(Employee employee)
        {
            return await _empRepository.AddEmployeeAsync(employee);
        }

        public async Task<Employee> UpdateEmployeeAsync(Employee employee)
        {
            return await _empRepository.UpdateEmployeeAsync(employee);
        }

        public async Task<Employee> DeleteEmployeeAsync(int id)
        {
            return await _empRepository.DeleteEmployeeAsync(id);
        }
    }
}
