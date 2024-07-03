namespace MilkTeaStore.DTO.Create
{
    public class OrderDTO
    {
        public int OrderID { get; set; }
        public string ReasonContent { get; set; }
        public string TypeOrder { get; set; }
        public bool? Status { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string ShipAddress { get; set; }
        public int UserID { get; set; }
    }
}
