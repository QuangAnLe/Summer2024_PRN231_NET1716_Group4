namespace ClientMilkTeamPage.DTO.UserDTO
{
    public class UserCreateDTO
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string FullName { get; set; }
        public string Phone { get; set; }
        public string UserAddress { get; set; }
        public string Email { get; set; }
        public bool Status { get; set; }
        public string RoleID { get; set; }
        public string DistrictID { get; set; }
    }
}
