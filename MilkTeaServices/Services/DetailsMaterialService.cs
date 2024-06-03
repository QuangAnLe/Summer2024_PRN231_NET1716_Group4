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
    public class DetailsMaterialService : IDetailsMaterialService
    {
        private readonly IDetailsMaterialRepo _detailsMaterialRepo;
        public DetailsMaterialService(IDetailsMaterialRepo detailsMaterialRepo)
        {
            _detailsMaterialRepo = detailsMaterialRepo;
        }

        public void AddNewDetailDetailsMaterial(DetailsMaterial detailsMaterial) => _detailsMaterialRepo.AddNewDetailDetailsMaterial(detailsMaterial);

        public void DeleteDetailsMaterial(int id) => _detailsMaterialRepo.DeleteDetailsMaterial(id);

        public List<DetailsMaterial> GetAllDetailsMaterial() => _detailsMaterialRepo.GetAllDetailsMaterial();

        public List<DetailsMaterial> GetDetailsMaterialByTeaID(int id) => _detailsMaterialRepo.GetDetailsMaterialByTeaID(id);

        public DetailsMaterial GetDetailsMaterialByID(int id) => _detailsMaterialRepo.GetDetailsMaterialByID(id);

        public void UpdateDetailsMaterial(DetailsMaterial detailsMaterial) => _detailsMaterialRepo.UpdateDetailsMaterial(detailsMaterial);
       
    }
}
