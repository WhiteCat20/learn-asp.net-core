using System.ComponentModel.DataAnnotations;

namespace api.Dtos.Comment
{
    public class UpdateCommentRequestDto
    {
        [Required]
        [MinLength(5, ErrorMessage = "Title must be at least 5 characters long!")]
        [MaxLength(50, ErrorMessage = "Title must be at most 50 characters long!")]
        public string Title { get; set; } = string.Empty;

        [Required]
        [MinLength(5, ErrorMessage = "Content must be at least 5 characters long!")]
        [MaxLength(50, ErrorMessage = "Content must be at most 50 characters long!")]
        public string Content { get; set; } = string.Empty;
    }
}