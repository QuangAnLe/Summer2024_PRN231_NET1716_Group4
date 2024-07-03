using MilkTeaBusinessObject.BusinessObject;

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
