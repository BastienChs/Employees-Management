using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace webapi.Tests.Mock
{
    internal class CustomMockExtensions
    {
        public static bool Equals(Employee actual, Employee expected)
        {
            return actual.Id == expected.Id &&
              actual.Name == expected.Name &&
              actual.JobTitle == expected.JobTitle &&
              actual.ManagerId == expected.ManagerId &&
              actual.HireDate == expected.HireDate &&
              actual.Salary == expected.Salary &&
              actual.Commission == expected.Commission &&
              actual.DepartmentId == expected.DepartmentId;
        }
    }
}
