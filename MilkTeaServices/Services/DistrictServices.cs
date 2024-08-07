﻿using MilkTeaBusinessObject.BusinessObject;
using MilkTeaRepository.IRepo;
using MilkTeaServices.IServices;

namespace MilkTeaServices.Services
{
    public class DistrictServices : IDistrictServices
    {
        private readonly IDistrictRepo _districtRepo;
        public DistrictServices(IDistrictRepo districtRepo)
        {
            _districtRepo = districtRepo;
        }

        public void AddNewDistrict(District district) => _districtRepo.AddNewDistrict(district);

        public bool ChangeStatusDistrict(District district) => _districtRepo.ChangeStatusDistrict(district);

        public List<District> GetAllDistrict() => _districtRepo.GetAllDistrict();

        public District GetDistrictByID(int id) => _districtRepo.GetDistrictByID(id);

        public void UpdateDistrict(District district) => _districtRepo.UpdateDistrict(district);
    }
}
