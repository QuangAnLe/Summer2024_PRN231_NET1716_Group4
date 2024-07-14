using System.ComponentModel.DataAnnotations;

namespace ClientMilkTeamPage.DTO.CommentDTO
{
    public class CommentUpdateDTO
    {
        public int CommentID { get; set; }
        public DateTime CommentDate { get; set; }
        public string Content { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "Rating phải lớn hơn 0.")]
        public int Rating { get; set; }
        public int TeaID { get; set; }
        public int UserID { get; set; }
    }
}
