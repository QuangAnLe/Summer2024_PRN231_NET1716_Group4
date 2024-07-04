using MilkTeaBusinessObject.BusinessObject;
using MilkTeaRepository.IRepo;
using MilkTeaServices.IServices;

namespace MilkTeaServices.Services
{
    public class MaterialServices : IMaterialServices
    {
        private readonly IMaterialRepo _materialRepo;
        public MaterialServices(IMaterialRepo materialRepo)
        {
            _materialRepo = materialRepo;
        }

        public void AddNewMaterial(Material material) => _materialRepo.AddNewMaterial(material);

        public bool ChangeStatusMaterial(Material material) => _materialRepo.ChangeStatusMaterial(material);

        public List<Material> GetAllMaterial() => _materialRepo.GetAllMaterial();

        public Material GetMaterialByID(int id) => _materialRepo.GetMaterialByID(id);

        public void UpdateMaterial(Material material) => _materialRepo.UpdateMaterial(material);

    }
}
