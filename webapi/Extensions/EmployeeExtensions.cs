using Domain.Models;
using webapi.Models;

namespace webapi.Extensions
{
    public static class EmployeeExtensions
    {
        public static EmployeeWithManager ToEmployeeWithManager(Employee emp)
        {
            return new EmployeeWithManager
            {
                Id = emp.Id,
                Name = emp.Name,
                JobTitle = emp.JobTitle,
                ManagerId = emp.ManagerId,
                HireDate = emp.HireDate,
                Salary = emp.Salary,
                Commission = emp.Commission,
                DepartmentId = emp.DepartmentId
            };
        }
    }
}
