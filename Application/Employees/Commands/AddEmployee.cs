﻿using Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Employees.Commands
{
    public class AddEmployee : IRequest<Employee>
    {
        public Employee Employee { get; set; } = new Employee();

    }
}
