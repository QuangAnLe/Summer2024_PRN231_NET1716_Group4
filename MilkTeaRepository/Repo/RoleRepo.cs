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
    public class RoleRepo : IRoleRepo
    {
        RoleDAO dao = new RoleDAO();

        public List<Role> GetAllRole() => dao.GetAllRole();

    }
}
