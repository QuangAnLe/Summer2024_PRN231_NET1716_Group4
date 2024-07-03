using MilkTeaBusinessObject.BusinessObject;
using MilkTeaDAO.DAOs;
using MilkTeaRepository.IRepo;

namespace MilkTeaRepository.Repo
{
    public class RoleRepo : IRoleRepo
    {
        RoleDAO dao = new RoleDAO();

        public List<Role> GetAllRole() => dao.GetAllRole();

    }
}
