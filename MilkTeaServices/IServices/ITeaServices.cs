using MilkTeaBusinessObject.BusinessObject;

namespace MilkTeaServices.IServices
{
    public interface ITeaServices
    {
        List<Tea> GetAllTea();
        void AddNewTea(Tea tea);
        Tea GetTeaByID(int id);
        void UpdateTea(Tea tea);
        bool ChangeStatusTea(Tea tea);
    }
}
