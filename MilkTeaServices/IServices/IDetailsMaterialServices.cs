using MilkTeaBusinessObject.BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MilkTeaServices.IServices
{
    public interface IDetailsMaterialServices
    {
        List<DetailsMaterial> GetAllDetailsMaterial();
        void AddNewDetailsMaterial(DetailsMaterial detail);
        DetailsMaterial GetDetailsMaterialByID(int id);
        void UpdateDetailsMaterial(DetailsMaterial detail);
        void DeleteDetailsMaterialById(int id);
    }
}
