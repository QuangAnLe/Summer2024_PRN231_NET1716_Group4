using MilkTeaBusinessObject.BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MilkTeaRepository.IRepo
{
    public interface IDetailsMaterialRepo
    {
        List<DetailsMaterial> GetAllDetailsMaterial();
        void AddNewDetailsMaterial(DetailsMaterial detail);
        DetailsMaterial GetDetailsMaterialByID(int id);
        void UpdateDetailsMaterial(DetailsMaterial detail);
        void DeleteDetailsMaterialById(int id);

    }
}
