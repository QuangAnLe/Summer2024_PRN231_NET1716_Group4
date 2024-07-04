﻿using MilkTeaBusinessObject.BusinessObject;

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
