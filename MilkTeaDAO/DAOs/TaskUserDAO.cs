using Microsoft.EntityFrameworkCore;
using MilkTeaBusinessObject.BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MilkTeaDAO.DAOs
{
    public class TaskUserDAO
    {
        private readonly MilkTeaDeliveryDBContext _context;
        public TaskUserDAO()
        {
            _context = new MilkTeaDeliveryDBContext();
        }
        public User CheckLogin(string email, string password)
        {
            return _context.Users.Where(u => u.Email!.Equals(email) && u.Password!.Equals(password)).FirstOrDefault();
        }

        public List<TaskUser> GetAllTaskUser()
        {
            try
            {
                return _context.TaskUsers.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void AddNewTaskUser(TaskUser taskUser)
        {
            try
            {
                _context.Add(taskUser);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        public TaskUser GetTaskUserByID(int id)
        {
            try
            {
                var taskUser = _context.TaskUsers!.SingleOrDefault(c => c.TaskId == id);
                return taskUser;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void UpdateTaskUser(TaskUser taskUser)
        {
            try
            {
                var a = _context.TaskUsers!.SingleOrDefault(c => c.TaskId == taskUser.TaskId);

                _context.Entry(a).CurrentValues.SetValues(taskUser);
                _context.SaveChanges();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool ChangeStatusTaskUser(TaskUser taskUser)
        {
            var _taskUser = _context.TaskUsers!.FirstOrDefault(c => c.TaskId.Equals(taskUser.TaskId));


            if (_taskUser == null)
            {
                return false;
            }
            else
            {
                _taskUser.Status = false;
                _context.Entry(_taskUser).State = EntityState.Modified;
                _context.SaveChanges();
                return true;
            }
        }
    }
}
    

