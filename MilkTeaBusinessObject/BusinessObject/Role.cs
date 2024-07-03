namespace MilkTeaBusinessObject.BusinessObject
{
    public class Role
    {
        public int RoleID { get; set; }
        public string RoleName { get; set; }
        public List<User> Users { get; set; }

    }
}
