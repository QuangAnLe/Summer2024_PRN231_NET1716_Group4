using MilkTeaBusinessObject.BusinessObject;
using MilkTeaRepository.IRepo;
using MilkTeaServices.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MilkTeaServices.Services
{
    public class TeaServices : ITeaServices
    {
        private readonly ITeaRepo _teaRepo;
        public TeaServices(ITeaRepo teaRepo)
        {
            _teaRepo = teaRepo;
        }

        public void AddNewTea(Tea tea) => _teaRepo.AddNewTea(tea);

        public bool ChangeStatusTea(Tea tea) => _teaRepo.ChangeStatusTea(tea);

        public List<Tea> GetAllTea() => _teaRepo.GetAllTea();

        public Tea GetTeaByID(int id) => _teaRepo.GetTeaByID(id);

        public void UpdateTea(Tea tea) => _teaRepo.UpdateTea(tea);

    }
}
