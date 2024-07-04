namespace MilkTeaStore.DTO.Create
{
    public class OrderDetailDTO
    {
        public int OrderDetailID { get; set; }
        public double TotalPrice { get; set; }
        public int Quantity { get; set; }
        public string CostsIncurred { get; set; }
        public int OrderID { get; set; }
        public int TeaID { get; set; }
    }
}
