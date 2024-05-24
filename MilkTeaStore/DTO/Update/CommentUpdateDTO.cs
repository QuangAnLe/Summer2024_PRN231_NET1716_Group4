using System.ComponentModel.DataAnnotations;

namespace MilkTeaStore.DTO.Update
{
    public class CommentUpdateDTO
    {
        public int CommentID { get; set; }
       
        public DateTime CommentDate { get; set; }
        [Required(ErrorMessage = "Content is required.")]
        public string Content { get; set; }
        [Required(ErrorMessage = "Rrating is required.")]
        public int Rating { get; set; }
        [Required(ErrorMessage = "Tea is required.")]
        public int TeaID { get; set; }
        [Required(ErrorMessage = "TeaName is required.")]
        public string TeaName { get; set; }
        
       
        [Required(ErrorMessage = "TeaDescription is required.")]
        public string TeaDescription { get; set; }
        [Required(ErrorMessage = "Image is required.")]
        public string Image { get; set; }
        [Required(ErrorMessage = "User is required.")]
        public int UserID { get; set; }
        [Required(ErrorMessage = "FullName is required.")]
        public string FullName { get; set; }
    }
}
