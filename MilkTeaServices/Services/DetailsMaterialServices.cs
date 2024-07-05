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
    public class DetailsMaterialServices : IDetailsMaterialServices
    {
        private readonly IDetailsMaterialRepo _detailRepo;
        public DetailsMaterialServices(IDetailsMaterialRepo detailRepo)
        {
            _detailRepo = detailRepo;
        }

        public void AddNewDetailsMaterial(DetailsMaterial detail) => _detailRepo.AddNewDetailsMaterial(detail);

        public void DeleteDetailsMaterialById(int id) => _detailRepo.DeleteDetailsMaterialById(id);

        public List<DetailsMaterial> GetAllDetailsMaterial() => _detailRepo.GetAllDetailsMaterial();

        public DetailsMaterial GetDetailsMaterialByID(int id) => _detailRepo.GetDetailsMaterialByID(id);

        public void UpdateDetailsMaterial(DetailsMaterial detail) => _detailRepo.UpdateDetailsMaterial(detail);

    }
}
