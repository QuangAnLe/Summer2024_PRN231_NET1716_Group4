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
    public class UserRepo : IUserRepo
    {
        UserDAO dao = new UserDAO();

        public void AddNewUser(User user) => dao.AddNewUser(user);

        public bool ChangeStatusUser(User user) => dao.ChangeStatusUser(user);

        public List<User> GetAllUser() => dao.GetAllUser();

        public User GetUserByID(int id) => dao.GetUserByID(id);

        public void UpdateUser(User user) => dao.UpdateUser(user);

    }
}
