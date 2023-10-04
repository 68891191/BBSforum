using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Forum.Models;

public class Message
{
    [Key]
    public int id { get; set; } // Unique identifier for the message.

    [ForeignKey("User")]
    [EmailAddress]
    public string senderEmail { get; set; }
    // Email address of the message sender, associated with the User entity.

    [ForeignKey("User")]
    [EmailAddress]
    public string receiverEmail { get; set; }
    // Email address of the message receiver, associated with the User entity.

    public string content { get; set; } = default!;
    // Content or body of the message.

    [DataType(DataType.Date)]
    [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true)]
    public DateTime createdAt { get; set; } = DateTime.Now;
    // Date and time when the message was created, with specified display format.
}