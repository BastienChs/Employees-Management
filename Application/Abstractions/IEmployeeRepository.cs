using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Abstractions
{
    public interface IEmployeeRepository
    {
        Task<List<Employee>> GetEmployeesAsync();
        Task<Employee> GetEmployeeAsync(int id);
        Task<Employee> AddEmployeeAsync(Employee employee);
        Task<Employee> UpdateEmployeeAsync(Employee employee);
        Task<Employee> DeleteEmployeeAsync(int id);
    }
}
