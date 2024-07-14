using System.ComponentModel.DataAnnotations;

namespace ClientMilkTeamPage.DTO.MaterialDTO
{
    public class MaterialCreateDTO
    {
        public string MaterialName { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "Quantity phải lớn hơn 0.")]
        public int Quantity { get; set; }
        [Range(0.01, double.MaxValue, ErrorMessage = "Price phải lớn hơn 0.")]
        public decimal Price { get; set; }
        public bool Status { get; set; }
    }
}
