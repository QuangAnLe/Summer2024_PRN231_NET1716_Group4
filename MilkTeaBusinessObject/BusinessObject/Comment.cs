namespace MilkTeaBusinessObject.BusinessObject
{
    public class Comment
    {
        public int CommentID { get; set; }
        public DateTime CommentDate { get; set; }
        public string Content { get; set; }
        public int Rating { get; set; }
        public int TeaID { get; set; }
        public Tea Tea { get; set; }

        public int UserID { get; set; }
        public User User { get; set; }
    }
}
