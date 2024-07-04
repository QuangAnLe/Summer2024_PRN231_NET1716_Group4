using MilkTeaBusinessObject.BusinessObject;

namespace MilkTeaServices.IServices
{
    public interface IMaterialServices
    {
        List<Material> GetAllMaterial();
        void AddNewMaterial(Material material);
        Material GetMaterialByID(int id);
        void UpdateMaterial(Material material);
        bool ChangeStatusMaterial(Material material);
    }
}
