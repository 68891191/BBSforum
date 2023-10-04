using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Forum.Models
{
    public class Comment
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; } // Unique identifier for the comment.

        [ForeignKey("User")]
        public int userId { get; set; } // Foreign key referencing the user who created the comment.

        [ForeignKey("Post")]
        public int postId { get; set; } // Foreign key referencing the post to which the comment belongs.

        public string content { get; set; } = default!; // Content of the comment.

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true)]
        public DateTime createdAt { get; set; } = DateTime.Now; // Timestamp indicating when the comment was created.
    }
}
