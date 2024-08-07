﻿using MilkTeaBusinessObject.BusinessObject;
using MilkTeaDAO.DAOs;
using MilkTeaRepository.IRepo;

namespace MilkTeaRepository.Repo
{
    public class UserRepo : IUserRepo
    {
        UserDAO dao = new UserDAO();

        public void AddNewUser(User user) => dao.AddNewUser(user);

        public bool ChangeStatusUser(User user) => dao.ChangeStatusUser(user);

        public User CheckLogin(string email, string password) => dao.CheckLogin(email, password);
        public User GetUserByEmail(string email) => dao.GetUserByEmail(email);

        public List<User> GetAllUser() => dao.GetAllUser();

        public User GetUserByID(int id) => dao.GetUserByID(id);

        public void UpdateUser(User user) => dao.UpdateUser(user);

    }
}
