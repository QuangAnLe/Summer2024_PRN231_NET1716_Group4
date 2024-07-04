using MilkTeaBusinessObject.BusinessObject;

namespace MilkTeaRepository.IRepo
{
    public interface IDistrictRepo
    {
        List<District> GetAllDistrict();
        void AddNewDistrict(District district);
        District GetDistrictByID(int id);
        void UpdateDistrict(District district);
        bool ChangeStatusDistrict(District district);
    }
}
