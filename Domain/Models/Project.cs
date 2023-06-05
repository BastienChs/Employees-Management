﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class Project
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public DateTime Start { get; set;}
        public DateTime End { get; set;}
    }
}
