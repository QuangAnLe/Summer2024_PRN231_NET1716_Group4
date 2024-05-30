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
