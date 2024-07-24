using MilkTeaBusinessObject.BusinessObject;
using MilkTeaRepository.IRepo;
using MilkTeaServices.IServices;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MilkTeaServices.Services
{
    public class TaskUserServices : ITaskUserServices
    {
        private readonly ITaskUserRepo _taskUserRepo;

        public TaskUserServices(ITaskUserRepo taskUserRepo)
        {
            _taskUserRepo = taskUserRepo;
        }

        public Task<List<TaskUser>> GetList() => _taskUserRepo.GetList();

        public Task<TaskUser> Get(int id) => _taskUserRepo.Get(id);

        public Task Add(TaskUser taskUser) => _taskUserRepo.Add(taskUser);

        public TaskUser GetByOrderID(int orderId)
        {
            return _taskUserRepo.GetByOrderID(orderId);
        }
        public Task Update(TaskUser taskUser) => _taskUserRepo.Update(taskUser);

        public Task Delete(int id) => _taskUserRepo.Delete(id);

        public Task UpdateTaskStatus(int taskId, bool status) => _taskUserRepo.UpdateTaskStatus(taskId, status);
        public Task UpdateTaskFailureReason(int taskId,  string failureReason) => _taskUserRepo.UpdateTaskFailureReason(taskId, failureReason);

        public Task UpdateStatusOfTask(int taskId, bool status) => _taskUserRepo.UpdateStatusOfTask(taskId, status);
    }
}
