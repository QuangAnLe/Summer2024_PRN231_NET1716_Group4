using MilkTeaBusinessObject.BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MilkTeaRepository.IRepo
{
    public interface IMaterialRepo
    {
        List<Material> GetAllMaterial();
        void AddNewMaterial(Material material);
        Material GetMaterialByID(int id);
        void UpdateMaterial(Material material);
        bool ChangeStatusMaterial(Material material);

    }
}
