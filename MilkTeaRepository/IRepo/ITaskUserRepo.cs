using MilkTeaBusinessObject.BusinessObject;
using System.Collections.Generic;

namespace MilkTeaRepository.IRepo
{
    public interface ITaskUserRepo
    {
        List<TaskUser> getList();
        TaskUser get(int id);
        void delete(int id);
        void update(TaskUser taskUser);
        void add(TaskUser taskUser);
        void UpdateTaskStatus(int taskId, bool status);
        TaskUser GetByOrderID(int orderId);
    }
}
