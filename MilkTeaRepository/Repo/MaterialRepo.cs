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
    public class MaterialRepo : IMaterialRepo
    {
        MaterialDAO dao = new MaterialDAO();

        public void AddNewMaterial(Material material) => dao.AddNewMaterial(material);

        public bool ChangeStatusMaterial(Material material) => dao.ChangeStatusMaterial(material);

        public List<Material> GetAllMaterial() => dao.GetAllMaterial();

        public Material GetMaterialByID(int id) => dao.GetMaterialByID(id);

        public void UpdateMaterial(Material material) => dao.UpdateMaterial(material);

    }
}
