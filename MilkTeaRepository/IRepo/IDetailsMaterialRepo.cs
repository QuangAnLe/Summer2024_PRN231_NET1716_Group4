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
        public List<DetailsMaterial> GetAllDetailsMaterial();
        public List<DetailsMaterial> GetDetailsMaterialByTeaID(int id);
        public DetailsMaterial GetDetailsMaterialByID(int id);
        public void AddNewDetailDetailsMaterial(DetailsMaterial detailsMaterial);
        public void UpdateDetailsMaterial(DetailsMaterial detailsMaterial);
        public void DeleteDetailsMaterial(int id);
    }
}
