using MilkTeaBusinessObject.BusinessObject;

namespace MilkTeaServices.IServices
{
    public interface ITaskUserServices
    {
        Task<List<TaskUser>> GetList();
        Task<TaskUser> Get(int id);
        Task Add(TaskUser taskUser);
        Task Update(TaskUser taskUser);
        Task Delete(int id);
        Task UpdateTaskStatus(int taskId, bool status);
            Task UpdateTaskFailureReason(int taskId, string failureReason);
        Task UpdateStatusOfTask(int taskId, bool status);
        TaskUser GetByOrderID(int orderId);
    }
}
