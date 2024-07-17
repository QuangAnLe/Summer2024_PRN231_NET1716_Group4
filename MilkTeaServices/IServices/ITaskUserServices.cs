using MilkTeaBusinessObject.BusinessObject;
using System.Collections.Generic;

namespace MilkTeaServices.IServices
{
    public interface ITaskUserServices
    {
        List<TaskUser> getList();
        TaskUser get(int id);
        TaskUser GetByOrderID(int orderId);
        void delete(int id);
        void update(TaskUser taskUser);
        void add(TaskUser taskUser);
        void UpdateTaskStatus(int taskId, bool status);
    }
}
