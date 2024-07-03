using MilkTeaBusinessObject.BusinessObject;
using MilkTeaRepository.IRepo;
using MilkTeaServices.IServices;

namespace MilkTeaServices.Services
{
    public class RoleServices : IRoleServices
    {
        private readonly IRoleRepo _roleRepo;
        public RoleServices(IRoleRepo roleRepo)
        {
            _roleRepo = roleRepo;
        }

        public List<Role> GetAllRole() => _roleRepo.GetAllRole();

    }
}
