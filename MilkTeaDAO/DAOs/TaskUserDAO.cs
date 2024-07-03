using Microsoft.EntityFrameworkCore;
using MilkTeaBusinessObject.BusinessObject;

namespace MilkTeaDAO.DAOs
{
    public class TaskUserDAO
    {

        private readonly MilkTeaDeliveryDBContext _context;

        public TaskUserDAO()
        {
            _context = new MilkTeaDeliveryDBContext();
        }

        public List<TaskUser> GetList()
        {
            try
            {
                return _context.TaskUsers.Include(t => t.Order).Include(t => t.User)
                        .ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public TaskUser Get(int id)
        {
            try
            {
                var taskUser = _context.TaskUsers.Include(t => t.Order).Include(t => t.User)
                                           .SingleOrDefault(t => t.TaskId == id);
                return taskUser;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void Add(TaskUser taskUser)
        {
            try
            {
                taskUser.TaskId = 0;
                _context.TaskUsers.Add(taskUser);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void Update(TaskUser taskUser)
        {
            try
            {
                var existingTask = _context.TaskUsers.SingleOrDefault(t => t.TaskId == taskUser.TaskId);
                if (existingTask != null)
                {
                    _context.Entry(existingTask).CurrentValues.SetValues(taskUser);
                    _context.SaveChanges();
                }
                else
                {
                    throw new Exception("Task not found");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void Delete(int id)
        {
            try
            {
                var taskUser = _context.TaskUsers.SingleOrDefault(t => t.TaskId == id);
                if (taskUser != null)
                {
                    _context.TaskUsers.Remove(taskUser);
                    _context.SaveChanges();
                }
                else
                {
                    throw new Exception("Task not found");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}

