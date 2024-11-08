﻿using EvrenCo.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvrenCo.Core.DTOs
{
    public class UserDto:BaseDto
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int DepartmentId { get; set; }
        public int GroupId { get; set; }
       
        public DepartmentDto? Department { get; set; }
        public GroupDto? Group { get; set; }
    }
}
