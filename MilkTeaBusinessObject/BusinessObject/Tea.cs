namespace MilkTeaBusinessObject.BusinessObject
{
    public class Tea
    {
        public int TeaID { get; set; }
        public string TeaName { get; set; }
        public int Estimation { get; set; }
        public double Price { get; set; }
        public string TeaDescription { get; set; }
        public bool Status { get; set; }
        public string Image { get; set; }
        public List<Comment> Comments { get; set; }
        public List<OrderDetail> OrderDetails { get; set; }

        public List<DetailsMaterial> DetailsMaterials { get; set; }
    }
}
