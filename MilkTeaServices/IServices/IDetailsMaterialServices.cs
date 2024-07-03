using MilkTeaBusinessObject.BusinessObject;

namespace MilkTeaServices.IServices
{
    public interface IDetailsMaterialServices
    {
        List<DetailsMaterial> GetAllDetailsMaterial();
        void AddNewDetailsMaterial(DetailsMaterial detail);
        DetailsMaterial GetDetailsMaterialByID(int id);
        void UpdateDetailsMaterial(DetailsMaterial detail);
        void DeleteDetailsMaterialById(int id);
    }
}
