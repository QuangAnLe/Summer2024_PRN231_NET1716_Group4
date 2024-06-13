using MilkTeaBusinessObject.BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
