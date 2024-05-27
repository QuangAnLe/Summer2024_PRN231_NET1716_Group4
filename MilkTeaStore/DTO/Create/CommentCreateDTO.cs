namespace MilkTeaStore.DTO.Create
{
    public class CommentCreateDTO
    {
        public int CommentID { get; set; }
        public DateTime CommentDate { get; set; }
        public string Content { get; set; }
        public int Rating { get; set; }
        public int TeaID { get; set; }
        public string TeaName { get; set; }
        public int Estimation { get; set; }
        public double Price { get; set; }
        public string TeaDescription { get; set; }
        public string Image { get; set; }
        public int UserID { get; set; }
        public string FullName { get; set; }
    }
}
