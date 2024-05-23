using Microsoft.EntityFrameworkCore;
using MilkTeaBusinessObject.BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MilkTeaDAO.DAOs
{
    public class UserDAO
    {
        private readonly MilkTeaDeliveryDBContext _context;
        public UserDAO()
        {
            _context = new MilkTeaDeliveryDBContext();
        }
        public User CheckLogin(string email, string password)
        {
            return _context.Users.Where(u => u.Email!.Equals(email) && u.Password!.Equals(password)).FirstOrDefault();
        }

        public List<User> GetAllUser()
        {
            try
            {
                return _context.Users.Include(c => c.Role)
                                    .Include(a => a.District)
                                    .Include(b => b.Comments)
                                    .Include(b => b.Orders)
                                    .ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void AddNewUser(User user)
        {
            try
            {
                _context.Add(user);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        public User GetUserByID(int id)
        {
            try
            {
                var user = _context.Users!.Include(c => c.Role)
                                         .Include(a => a.District)
                                         .Include(b => b.Comments)
                                         .Include(b => b.Orders)
                                           .SingleOrDefault(c => c.UserID == id);
                return user;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void UpdateUser(User user)
        {
            try
            {
                var a = _context.Users!.SingleOrDefault(c => c.UserID == user.UserID);

                _context.Entry(a).CurrentValues.SetValues(user);
                _context.SaveChanges();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool ChangeStatusUser(User user)
        {
            var _user = _context.Users!.FirstOrDefault(c => c.UserID.Equals(user.UserID));


            if (_user == null)
            {
                return false;
            }
            else
            {
                _user.Status = false;
                _context.Entry(_user).State = EntityState.Modified;
                _context.SaveChanges();
                return true;
            }
        }
    }
}
