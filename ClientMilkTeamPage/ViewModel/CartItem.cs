namespace ClientMilkTeamPage.ViewModel
{
    public class CartItem
    {
        public int TeaID { get; set; }
        public string TeaName { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }
        public List<SelectedMaterial> SelectedMaterials { get; set; } = new List<SelectedMaterial>();
        public double TotalPrice
        {
            get
            {
                return (Price + SelectedMaterials.Sum(m => m.Price)) * Quantity;
            }
        }
    }

    public class SelectedMaterial
    {
        public int MaterialID { get; set; }
        public string MaterialName { get; set; }
        public double Price { get; set; }
    }
}
