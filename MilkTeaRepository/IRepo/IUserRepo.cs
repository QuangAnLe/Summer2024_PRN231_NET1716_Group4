using MilkTeaBusinessObject.BusinessObject;

namespace MilkTeaRepository.IRepo
{
    public interface IUserRepo
    {
        User CheckLogin(string email, string password);
        User GetUserByEmail(string email);
        List<User> GetAllUser();
        void AddNewUser(User user);
        User GetUserByID(int id);
        void UpdateUser(User user);
        bool ChangeStatusUser(User user);

    }
}
