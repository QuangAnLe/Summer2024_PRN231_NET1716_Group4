using System.ComponentModel.DataAnnotations;

namespace ClientMilkTeamPage.DTO.DetailMaterialDTO
{
    public class DetailMaterialCreateDTO
    {
        [Range(1, int.MaxValue, ErrorMessage = "Quantity phải lớn hơn 0.")]
        public int Quanity { get; set; }
        public string DetailMaterialName { get; set; }
        public int TeaID { get; set; }
        public int MaterialID { get; set; }
    }
}
