using MilkTeaBusinessObject.BusinessObject;
using MilkTeaDAO.DAOs;
using MilkTeaRepository.IRepo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MilkTeaRepository.Repo
{
    public class TakUserRepo : ITaskUserRepo

    {
        TaskUserDAO dao = new TaskUserDAO();

        public void AddNewTaskUser(TaskUser taskUser) => dao.AddNewTaskUser(taskUser);

        public bool ChangeStatusTaskUser(TaskUser taskUser) => dao.ChangeStatusTaskUser(taskUser);

        public User CheckLogin(string email, string password) => dao.CheckLogin(email, password);

        public List<TaskUser> GetAllTaskUsers() => dao.GetAllTaskUser();

        public TaskUser GetTaskUserByID(int id) => dao.GetTaskUserByID(id);

        public void UpdateUser(TaskUser taskUser) => dao.UpdateTaskUser(taskUser);

    }
}
