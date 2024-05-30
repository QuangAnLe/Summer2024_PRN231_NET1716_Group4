using MilkTeaBusinessObject.BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
