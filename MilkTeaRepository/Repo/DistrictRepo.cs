using MilkTeaBusinessObject.BusinessObject;
using MilkTeaDAO.DAOs;
using MilkTeaRepository.IRepo;

namespace MilkTeaRepository.Repo
{
    public class DistrictRepo : IDistrictRepo
    {
        DistrictDAO dao = new DistrictDAO();
        public void AddNewDistrict(District district)
        {
            dao.AddNewDistrict(district);
        }

        public bool ChangeStatusDistrict(District district) => dao.ChangeStatusDistrict(district);

        public List<District> GetAllDistrict() => dao.GetAllDistrict();

        public District GetDistrictByID(int id) => dao.GetDistrictByID(id);

        public void UpdateDistrict(District district)
        {
            dao.UpdateDistrict(district);
        }
    }
}
