using System.ComponentModel.DataAnnotations;

namespace api.Dtos.Comment
{
    public class CommentDto
    {
        public int Id { get; set; }
        [Required]
        [MinLength(5,ErrorMessage ="Title must longer then 5 characters")]
        [MaxLength(140, ErrorMessage ="Title cannot be over 140 characters")]
        public string Title { get; set; } = string.Empty;
        [Required]
        [MinLength(5,ErrorMessage ="Title must longer then 5 characters")]
        [MaxLength(140, ErrorMessage ="Title cannot be over 140 characters")]
        public string Content { get; set; } = string.Empty;
        public DateTime CreatedOn { get; set; } = DateTime.Now;
        public int? StockId { get; set; }
    }
}