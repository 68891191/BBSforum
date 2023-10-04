using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Forum.Models;

public class Post
{
    [Key]
    public int id { get; set; } // Unique identifier for the post.

    [ForeignKey("User")]
    public int userId { get; set; }
    // Foreign key reference to the User who created the post.

    public string title { get; set; } = default!;
    // Title of the post.

    public string content { get; set; } = default!;
    // Content or body of the post.

    [DataType(DataType.Date)]
    [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true)]
    public DateTime createdAt { get; set; } = DateTime.Now;
    // Date and time when the post was created, with specified display format.

    [ForeignKey("Tag")]
    public int tagId { get; set; }
    // Foreign key reference to the associated Tag for categorization.

    public int likes { get; set; } = 0;
    // Count of likes received by the post.

    public int dislikes { get; set; } = 0;
    // Count of dislikes received by the post.
}