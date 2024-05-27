using System.ComponentModel.DataAnnotations;

namespace MilkTeaStore.DTO.Update
{
    public class TaskUserUpdateDTO
    {
        public int TaskId { get; set; }
        [Required(ErrorMessage = "WorkName is required.")]
        public string WorkName { get; set; }
        [Required(ErrorMessage = "WorkDescription is required.")]
        public string WorkDescription { get; set; }
        [Required(ErrorMessage = "Status is required.")]
        public bool Status { get; set; }
        [Required(ErrorMessage = "User is required.")]
        public int UserID { get; set; }
        [Required(ErrorMessage = "Order is required.")]
        public int OrderID { get; set; }
        [Required(ErrorMessage = "ShipAddress is required.")]
        public string ShipAddress { get; set; }
        [Required(ErrorMessage = "FullName is required.")]
        public string FullName { get; set; }
        [Required(ErrorMessage = "PhoneNumber is required.")]
        [RegularExpression(@"^(?=.*[0-9])[-0-9]{10,15}$", ErrorMessage = "Invalid phone number.")]
        public string Phone { get; set; }
    }
}
