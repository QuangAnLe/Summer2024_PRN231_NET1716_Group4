namespace ClientMilkTeamPage.DTO.TeaDTO
{
    public class TeaUpdateDTO
    {
        public int TeaID { get; set; }
        public string TeaName { get; set; }
        public int Estimation { get; set; }
        public decimal Price { get; set; }
        public string TeaDescription { get; set; }
        public bool Status { get; set; }
        public string Image { get; set; }
    }
}
