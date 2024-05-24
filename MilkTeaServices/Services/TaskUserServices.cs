using MilkTeaBusinessObject.BusinessObject;
using MilkTeaRepository.IRepo;
using MilkTeaServices.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

        public void AddNewTaskUser(TaskUser taskUser) => _taskUserRepo.AddNewTaskUser(taskUser);

        public bool ChangeStatusTaskUser(TaskUser taskUser) => _taskUserRepo.ChangeStatusTaskUser(taskUser);

        public User CheckLogin(string email, string password) => _taskUserRepo.CheckLogin(email, password);

        public List<TaskUser> GetAllTaskUser() => _taskUserRepo.GetAllTaskUser();

        public TaskUser GetTaskUserByID(int id) => _taskUserRepo.GetTaskUserByID(id);

        public void UpdateTaskUser(TaskUser taskUser) => _taskUserRepo.UpdateTaskUser(taskUser);

    }
}