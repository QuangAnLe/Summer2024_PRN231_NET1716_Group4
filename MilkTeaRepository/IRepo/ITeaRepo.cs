using MilkTeaBusinessObject.BusinessObject;

namespace MilkTeaRepository.IRepo
{
    public interface ITeaRepo
    {
        List<Tea> GetAllTea();
        void AddNewTea(Tea tea);
        Tea GetTeaByID(int id);
        void UpdateTea(Tea tea);
        bool ChangeStatusTea(Tea tea);
    }
}
