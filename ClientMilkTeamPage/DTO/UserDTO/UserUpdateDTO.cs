using System.ComponentModel.DataAnnotations;

namespace ClientMilkTeamPage.DTO.UserDTO
{
    public class UserUpdateDTO
    {
        public int UserID { get; set; }
        [Required(ErrorMessage = "UserName is required.")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Password is required.")]
        public string Password { get; set; }

        [Required(ErrorMessage = "FullName is required.")]
        public string FullName { get; set; }
        [Required(ErrorMessage = "PhoneNumber is required.")]
        [RegularExpression(@"^(?=.*[0-9])[-0-9]{10,15}$", ErrorMessage = "Invalid phone number.")]
        public string Phone { get; set; }
        public string UserAddress { get; set; }
        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email address.")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Status is required.")]
        public bool Status { get; set; }
        [Required(ErrorMessage = "Role is required.")]
        public string RoleID { get; set; }
        [Required(ErrorMessage = "District is required.")]
        public string DistrictID { get; set; }
    }
}
