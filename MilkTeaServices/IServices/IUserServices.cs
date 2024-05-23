using MilkTeaBusinessObject.BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MilkTeaServices.IServices
{
    public interface IUserServices
    {
        User CheckLogin(string email, string password);
        List<User> GetAllUser();
        void AddNewUser(User user);
        User GetUserByID(int id);
        void UpdateUser(User user);
        bool ChangeStatusUser(User user);
    }
}
