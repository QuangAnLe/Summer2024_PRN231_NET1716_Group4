using Microsoft.EntityFrameworkCore;
using MilkTeaBusinessObject.BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public async Task<List<TaskUser>> GetListAsync()
        {
            try
            {
                return await _context.TaskUsers.Include(t => t.Order).Include(t => t.User).ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Error retrieving TaskUser list.", ex);
            }
        }

        public async Task<TaskUser> GetAsync(int id)
        {
            try
            {
                return await _context.TaskUsers.Include(t => t.Order).Include(t => t.User)
                                              .SingleOrDefaultAsync(t => t.TaskId == id);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error retrieving TaskUser with ID {id}.", ex);
            }
        }

        public async Task AddAsync(TaskUser taskUser)
        {
            try
            {
                await _context.TaskUsers.AddAsync(taskUser);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Error adding TaskUser.", ex);
            }
        }

        public async Task UpdateAsync(TaskUser taskUser)
        {
            try
            {
                var existingTask = await _context.TaskUsers.FindAsync(taskUser.TaskId);
                if (existingTask != null)
                {
                    _context.Entry(existingTask).CurrentValues.SetValues(taskUser);
                    await _context.SaveChangesAsync();
                }
                else
                {
                    throw new Exception("Task not found");
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error updating TaskUser.", ex);
            }
        }

        public async Task DeleteAsync(int id)
        {
            try
            {
                var taskUser = await _context.TaskUsers.FindAsync(id);
                if (taskUser != null)
                {
                    _context.TaskUsers.Remove(taskUser);
                    await _context.SaveChangesAsync();
                }
                else
                {
                    throw new Exception("Task not found");
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error deleting TaskUser.", ex);
            }
        }

        public async Task UpdateTaskStatusAsync(int taskId, bool status, string failureReason)
        {
            try
            {
                var existingTask = await _context.TaskUsers.Include(t => t.Order).SingleOrDefaultAsync(t => t.TaskId == taskId);
                if (existingTask != null)
                {
                    existingTask.Status = status;

                    if (!status)
                    {
                        existingTask.WorkDescription = failureReason; // Update work description with failure reason
                    }
                    else
                    {
                        existingTask.WorkDescription = null; // Clear work description if status is true
                    }

                    // Update the associated order's status
                    if (existingTask.Order != null)
                    {
                        existingTask.Order.Status = status; // Update Order status based on TaskUser status
                    }

                    await _context.SaveChangesAsync();
                }
                else
                {
                    throw new Exception("Task not found");
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error updating TaskUser status.", ex);
            }
        }

        public async Task UpdateStatusOfTaskAsync(int taskId, bool status)
        {
            try
            {
                var existingTask = await _context.TaskUsers.FindAsync(taskId);
                if (existingTask != null)
                {
                    existingTask.Status = status;
                    await _context.SaveChangesAsync();
                }
                else
                {
                    throw new Exception("Task not found");
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error updating TaskUser status.", ex);
            }
        }

		public TaskUser GetByOrderID(int orderId)
		{
			return _context.TaskUsers
				.Include(tu => tu.Order)
				.Include(tu => tu.User)
				.FirstOrDefault(tu => tu.OrderID == orderId);
		}
	}
}
