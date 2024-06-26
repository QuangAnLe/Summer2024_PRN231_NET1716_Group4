namespace ClientMilkTeamPage.DTO
{
    public class Order
    {
        public int OrderID { get; set; }
        public string ReasonContent { get; set; }
        public string TypeOrder { get; set; }
        public bool Status { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string ShipAddress { get; set; }
        public int UserID { get; set; }
        public User User { get; set; }
        public List<OrderDetail> OrderDetails { get; set; }
        public List<TaskUser> TaskUsers { get; set; }
    }
}
