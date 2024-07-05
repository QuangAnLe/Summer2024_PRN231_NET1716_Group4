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
        DetailsMaterialDAO dao = new DetailsMaterialDAO();

        public void AddNewDetailsMaterial(DetailsMaterial detail) => dao.AddNewDetailsMaterial(detail);

        public void DeleteDetailsMaterialById(int id) => dao.DeleteDetailsMaterialById(id);

        public List<DetailsMaterial> GetAllDetailsMaterial() => dao.GetAllDetailsMaterial();

        public DetailsMaterial GetDetailsMaterialByID(int id) => dao.GetDetailsMaterialByID(id);

        public void UpdateDetailsMaterial(DetailsMaterial detail) => dao.UpdateDetailsMaterial(detail);

    }
}
