using MilkTeaBusinessObject.BusinessObject;

namespace MilkTeaRepository.IRepo
{
    public interface IDetailsMaterialRepo
    {
        List<DetailsMaterial> GetAllDetailsMaterial();
        void AddNewDetailsMaterial(DetailsMaterial detail);
        DetailsMaterial GetDetailsMaterialByID(int id);
        void UpdateDetailsMaterial(DetailsMaterial detail);
        void DeleteDetailsMaterialById(int id);

    }
}
