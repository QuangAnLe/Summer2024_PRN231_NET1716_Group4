using MilkTeaBusinessObject.BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MilkTeaServices.IServices
{
    public interface ITaskUserServices
    {
        User CheckLogin(string email, string password);
        List<TaskUser> GetAllTaskUser();
        void AddNewTaskUser(TaskUser taskUser);
        TaskUser GetTaskUserByID(int id);
        void UpdateTaskUser(TaskUser taskUser);
        bool ChangeStatusTaskUser(TaskUser taskUser);
    }
}
