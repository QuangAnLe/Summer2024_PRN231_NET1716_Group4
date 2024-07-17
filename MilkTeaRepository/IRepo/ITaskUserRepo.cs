using MilkTeaBusinessObject.BusinessObject;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MilkTeaRepository.IRepo
{
    public interface ITaskUserRepo
    {
        Task<List<TaskUser>> GetList();
        Task<TaskUser> Get(int id);
        Task Add(TaskUser taskUser);
        Task Update(TaskUser taskUser);
        Task Delete(int id);
        Task UpdateTaskStatus(int taskId, bool status, string failureReason);
        Task UpdateStatusOfTask(int taskId, bool status);
    }
}
