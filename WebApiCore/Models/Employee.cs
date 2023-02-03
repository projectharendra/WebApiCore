﻿using System;
using System.Collections.Generic;

namespace WebApiCore.Models
{
    public partial class Employee
    {
        public int EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public string Department { get; set; }
        public DateTime? DateOfJoining { get; set; }
        public string PhotoFileName { get; set; }
    }
}
