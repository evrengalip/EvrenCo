﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvrenCo.Core.DTOs
{
    public class GroupInRoleDto:BaseDto
    {
        public int GroupId { get; set; }
        public int RoleId { get; set; }
    }
}
