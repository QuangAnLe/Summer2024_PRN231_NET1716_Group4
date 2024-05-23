using MilkTeaBusinessObject.BusinessObject;
using MilkTeaRepository.IRepo;
using MilkTeaServices.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MilkTeaServices.Services
{
    public class UserServices : IUserServices
    {
        private readonly IUserRepo _userRepo;
        public UserServices(IUserRepo userRepo)
        {
            _userRepo = userRepo;
        }

        public void AddNewUser(User user) => _userRepo.AddNewUser(user);

        public bool ChangeStatusUser(User user) => _userRepo.ChangeStatusUser(user);

        public User CheckLogin(string email, string password) => _userRepo.CheckLogin(email, password);

        public List<User> GetAllUser() => _userRepo.GetAllUser();

        public User GetUserByID(int id) => _userRepo.GetUserByID(id);

        public void UpdateUser(User user) => _userRepo.UpdateUser(user);

    }
}
