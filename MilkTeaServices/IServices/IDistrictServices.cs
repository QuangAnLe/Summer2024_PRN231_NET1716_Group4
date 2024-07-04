using MilkTeaBusinessObject.BusinessObject;

namespace MilkTeaServices.IServices
{
    public interface IDistrictServices
    {
        List<District> GetAllDistrict();
        void AddNewDistrict(District district);
        District GetDistrictByID(int id);
        void UpdateDistrict(District district);
        bool ChangeStatusDistrict(District district);
    }
}
