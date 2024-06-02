using MilkTeaBusinessObject.BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MilkTeaServices.IServices
{
    public interface IDetailsMaterialService
    {
        List<DetailsMaterial> GetAllDetailsMaterial();
        List<DetailsMaterial> GetDetailsMaterialByTeaID(int id);
        DetailsMaterial GetDetailsMaterialByID(int id);
        void AddNewDetailDetailsMaterial(DetailsMaterial detailsMaterial);
        void UpdateDetailsMaterial(DetailsMaterial detailsMaterial);
        void DeleteDetailsMaterial(int id);
    }
}
