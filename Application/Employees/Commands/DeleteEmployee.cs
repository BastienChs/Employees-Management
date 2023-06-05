using Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Employees.Commands
{
    public class DeleteEmployee : IRequest<Employee>
    {
        public int Id { get; set; }
    }
}
