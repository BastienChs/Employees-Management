using Domain.Models;

namespace webapi.Models
{
    public class EmployeeWithManager : Employee
    {
        public Employee? Manager { get; set; }
    }
}
