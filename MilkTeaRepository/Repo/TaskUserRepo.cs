using MilkTeaBusinessObject.BusinessObject;
using MilkTeaDAO.DAOs;
using MilkTeaRepository.IRepo;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MilkTeaRepository.Repo
{
    public class TaskUserRepo : ITaskUserRepo
    {
        TaskUserDAO _dao = new TaskUserDAO();

        public Task<List<TaskUser>> GetList() => _dao.GetListAsync();

        public Task<TaskUser> Get(int id) => _dao.GetAsync(id);

        public Task Add(TaskUser taskUser) => _dao.AddAsync(taskUser);

        public Task Update(TaskUser taskUser) => _dao.UpdateAsync(taskUser);

        public Task Delete(int id) => _dao.DeleteAsync(id);

        public Task UpdateTaskStatus(int taskId, bool status, string failureReason) =>
            _dao.UpdateTaskStatusAsync(taskId, status, failureReason);

        public TaskUser GetByOrderID(int orderId)
        {
            return _dao.GetByOrderID(orderId);
        }
        public Task UpdateStatusOfTask(int taskId, bool status) =>
            _dao.UpdateStatusOfTaskAsync(taskId, status);
    }
}
