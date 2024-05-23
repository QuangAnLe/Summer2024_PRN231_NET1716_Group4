using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MilkTeaBusinessObject.BusinessObject
{
    public class User
    {
        public int UserID { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string FullName { get; set; }
        public string Phone { get; set; }
        public string UserAddress { get; set; }
        public string Email { get; set; }
        public bool Status { get; set; }

        public string RoleID { get; set; }
        public Role Role { get; set; }
        public string DistrictID { get; set; }
        public District District { get; set; }
        public List<Comment> Comments { get; set; }
        public List<Order> Orders { get; set; }

        public List<TaskUser> TaskUsers { get; set; }
    }
}
