using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MilkTeaStore.DTO.Update
{ 
    public class DistrictUpdateDTO
    {
        public int DistrictID { get; set; }
        public string DistrictName { get; set; }
        public string WardName { get; set; }
    }
}
