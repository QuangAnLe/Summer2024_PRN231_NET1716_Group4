using MilkTeaBusinessObject.BusinessObject;
using MilkTeaDAO.DAOs;
using MilkTeaRepository.IRepo;

namespace MilkTeaRepository.Repo
{
    public class TeaRepo : ITeaRepo
    {
        TeaDAO dao = new TeaDAO();

        public void AddNewTea(Tea tea) => dao.AddNewTea(tea);

        public bool ChangeStatusTea(Tea tea) => dao.ChangeStatusTea(tea);

        public List<Tea> GetAllTea() => dao.GetAllTea();

        public Tea GetTeaByID(int id) => dao.GetTeaByID(id);

        public void UpdateTea(Tea tea) => dao.UpdateTea(tea);

    }
}
