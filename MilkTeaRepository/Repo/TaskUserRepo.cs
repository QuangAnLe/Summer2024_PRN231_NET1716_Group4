using MilkTeaBusinessObject.BusinessObject;
using MilkTeaDAO.DAOs;
using MilkTeaRepository.IRepo;

namespace MilkTeaRepository.Repo
{
    public class TaskUserRepo : ITaskUserRepo
    {
        TaskUserDAO dao = new TaskUserDAO();
        public void add(TaskUser taskUser)
        {
            dao.Add(taskUser);
        }

        public void delete(int id)
        {
            dao.Delete(id);
        }

        public TaskUser get(int id)
        {
            return dao.Get(id);
        }

        public List<TaskUser> getList()
        {
            return dao.GetList();
        }

        public void update(TaskUser taskUser)
        {
            dao.Update(taskUser);
        }
    }
}

