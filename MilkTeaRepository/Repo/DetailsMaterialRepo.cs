using MilkTeaBusinessObject.BusinessObject;
using MilkTeaDAO.DAOs;
using MilkTeaRepository.IRepo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MilkTeaRepository.Repo
{
    public class DetailsMaterialRepo : IDetailsMaterialRepo
    {
        DetailsMeterialDAO dao = new DetailsMeterialDAO();
        public void AddNewDetailDetailsMaterial(DetailsMaterial detailsMaterial) => dao.AddNewDetailDetailsMaterial(detailsMaterial);

        public void DeleteDetailsMaterial(int id) => dao.DeleteDetailsMaterial(id);

        public List<DetailsMaterial> GetAllDetailsMaterial() => dao.GetAllDetailsMaterial();

        public List<DetailsMaterial> GetDetailsMaterialByTeaID(int id) => dao.GetDetailsMaterialByTeaID(id);

        public DetailsMaterial GetDetailsMaterialByID(int id) => dao.GetDetailsMaterialByID(id);

        public void UpdateDetailsMaterial(DetailsMaterial detailsMaterial) => dao.UpdateDetailsMaterial(detailsMaterial);
    }
}
