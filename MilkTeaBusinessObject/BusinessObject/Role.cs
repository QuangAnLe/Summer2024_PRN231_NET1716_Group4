﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MilkTeaBusinessObject.BusinessObject
{
    public class Role
    {
        public string RoleID { get; set; }
        public string RoleName { get; set; }
        public List<User> Users { get; set; }
    
    }
}
