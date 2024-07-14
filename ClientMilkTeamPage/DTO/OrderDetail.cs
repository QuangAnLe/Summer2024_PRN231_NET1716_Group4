using System.ComponentModel.DataAnnotations;

namespace ClientMilkTeamPage.DTO
{
    public class OrderDetail
    {
        public int OrderDetailID { get; set; }
        [Range(0.01, double.MaxValue, ErrorMessage = "TotalPrice phải lớn hơn 0.")]
        public double TotalPrice { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "Quantity phải lớn hơn 0.")]
        public int Quantity { get; set; }
        public string CostsIncurred { get; set; }
        public int OrderID { get; set; }
        public Order Order { get; set; }
        public int TeaID { get; set; }
        public Tea Tea { get; set; }
    }
}
