using MilkTeaBusinessObject.BusinessObject;

namespace MilkTeaServices.IServices
{
    public interface ITaskUserServices
    {
        public List<TaskUser> getList();
        public TaskUser get(int id);
        public void delete(int id);
        public void update(TaskUser taskUser);
        public void add(TaskUser taskUser);
    }
}
