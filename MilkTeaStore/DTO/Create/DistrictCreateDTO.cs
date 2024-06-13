using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MilkTeaStore.DTO.Create
{
    public class DistrictCreateDTO
    {
        public string DistrictName { get; set; }
        public string WardName { get; set; }
    }
}
