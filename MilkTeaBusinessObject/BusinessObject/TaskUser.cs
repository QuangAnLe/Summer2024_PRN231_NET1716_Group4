using System.ComponentModel.DataAnnotations;

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
