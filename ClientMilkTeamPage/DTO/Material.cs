namespace ClientMilkTeamPage.DTO
{
    public class Material
    {
        public int MaterialID { get; set; }
        public string MaterialName { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }
        public bool Status { get; set; }
        public List<DetailsMaterial> DetailsMaterials { get; set; }
    }
}
