namespace ClientMilkTeamPage.DTO.MaterialDTO
{
    public class DetailMateriaUpdateDTO
    {
        public int MaterialID { get; set; }
        public string MaterialName { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public bool Status { get; set; }
    }
}
