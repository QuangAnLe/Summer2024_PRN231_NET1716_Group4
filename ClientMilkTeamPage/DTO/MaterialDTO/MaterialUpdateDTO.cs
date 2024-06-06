namespace MilkTeaStore.DTO.Update
{
    public class MaterialUpdateDTO
    {
        public int MaterialID { get; set; }
        public string MaterialName { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public bool Status { get; set; }
    }
}
