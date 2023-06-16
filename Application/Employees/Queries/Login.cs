﻿using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Employees.Queries
{
    public class Login : IRequest<int>
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
