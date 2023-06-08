using Domain.Models;
using webapi.Models;

namespace webapi.Tests.Mock
{
    public class Mocking
    {
        public static List<Employee> GetEmployees()
        {
            return new List<Employee>() { 
                new Employee() 
                { 
                    Id = 1,
                    Name = "Doe", 
                    ManagerId = 7850, 
                    JobTitle = "MANAGER", 
                    Salary = 3000, 
                    Commission = null, 
                    DepartmentId = 10, 
                    HireDate = DateTime.Parse("17-DEC-1980")
                },
                new Employee()
                {
                    Id = 7850,
                    Name = "KING",
                    ManagerId = null,
                    JobTitle = "CEO",
                    Salary = 5000,
                    Commission = 150,
                    DepartmentId = 10,
                    HireDate = DateTime.Parse("17-DEC-1975")
                }
            };
        }
        
        public static NewEmployee GetNewEmployee()
        {
            return new NewEmployee()
            {
                Name = "Doe",
                ManagerId = 7850,
                JobTitle = "MANAGER",
                Salary = 3000,
                Commission = null,
                DepartmentId = 10,
                HireDate = DateTime.Parse("17-DEC-1980")
            };
        }
    }
}
