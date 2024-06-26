namespace ClientMilkTeamPage.ViewModel
{
    public class CartItem
    {
        public int TeaID { get; set; }
        public string TeaName { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }
        public double TotalPrice { get { return Price * Quantity; } }
    }
}
