using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace webapi.Tests.Mock
{
    public class DepartmentMocks
    {
        public static List<Department> GetDepartments()
        {
            return new List<Department>()
            {
                new Department()
                {
                    Id = 1,
                    Name = "IT",
                    LocationCity = "London",
                },
                new Department()
                {
                    Id = 2,
                    Name = "HR",
                    LocationCity = "London",
                },
                new Department()
                {
                    Id = 3,
                    Name = "Sales",
                    LocationCity = "London",
                },
            };
        }
    }
}
