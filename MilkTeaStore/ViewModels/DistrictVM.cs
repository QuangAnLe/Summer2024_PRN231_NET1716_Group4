using MilkTeaBusinessObject.BusinessObject;

namespace MilkTeaStore.ViewModels
{
    public class DistrictVM
    {
        public int DistrictID { get; set; }
        public string DistrictName { get; set; }
        public string WardName { get; set; }
        public List<User> Users { get; set; }
    }
}
