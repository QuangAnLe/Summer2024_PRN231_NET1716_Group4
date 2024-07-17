using System.ComponentModel.DataAnnotations;

namespace ClientMilkTeamPage.DTO.TeaDTO
{
    public class TeaUpdateDTO
    {
        public int TeaID { get; set; }
        public string TeaName { get; set; }
		[Range(0, double.MaxValue, ErrorMessage = "Estimation >= 0.")]
		public int Estimation { get; set; }
        [Range(0.01, double.MaxValue, ErrorMessage = "Price phải lớn hơn 0.")]
        public decimal Price { get; set; }
        public string TeaDescription { get; set; }
        public bool Status { get; set; }
        public string Image { get; set; }
    }
}
