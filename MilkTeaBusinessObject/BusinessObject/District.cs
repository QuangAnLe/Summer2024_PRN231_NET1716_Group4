namespace MilkTeaBusinessObject.BusinessObject
{
    public class District
    {
        public int DistrictID { get; set; }
        public string DistrictName { get; set; }
        public string WardName { get; set; }
        public List<User> Users { get; set; }

    }
}
