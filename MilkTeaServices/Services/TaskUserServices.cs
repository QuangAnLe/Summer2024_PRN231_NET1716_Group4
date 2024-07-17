using MilkTeaBusinessObject.BusinessObject;
using MilkTeaRepository.IRepo;
using MilkTeaServices.IServices;
using System.Collections.Generic;

namespace MilkTeaServices.Services
{
    public class TaskUserServices : ITaskUserServices
    {
        private readonly ITaskUserRepo _taskUserRepo;

        public TaskUserServices(ITaskUserRepo taskUserRepo)
        {
            _taskUserRepo = taskUserRepo;
        }

        public List<TaskUser> getList() => _taskUserRepo.getList();

        public TaskUser get(int id) => _taskUserRepo.get(id);

        public void delete(int id) => _taskUserRepo.delete(id);

        public TaskUser GetByOrderID(int orderId)
        {
            return _taskUserRepo.GetByOrderID(orderId);
        }

        public void update(TaskUser taskUser) => _taskUserRepo.update(taskUser);

        public void add(TaskUser taskUser) => _taskUserRepo.add(taskUser);

        public void UpdateTaskStatus(int taskId, bool status) => _taskUserRepo.UpdateTaskStatus(taskId, status);
    }
}
