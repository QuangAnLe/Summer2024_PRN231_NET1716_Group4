using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MilkTeaBusinessObject.BusinessObject
{
    public class TaskUser
    {
        [Key]
        public int TaskId { get; set; }
        public string WorkName { get; set; }
        public string WorkDescription { get; set; }
        public bool Status { get; set; }
        public int UserID { get; set; }
        public int OrderID { get; set; }
        public Order Order { get; set; }
        public User User { get; set; }
    }
}
