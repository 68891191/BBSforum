using System.ComponentModel.DataAnnotations;

namespace Forum.Models;

public class User
{
    [Key]
    public int id { get; set; }
    // Unique identifier for the user.

    [Required]
    [StringLength(30)]
    [Display(Name = "First name: ")]
    public string name { get; set; } = default!;
    // User's first name, with required validation and a display name.

    [Required]
    [StringLength(30)]
    [Display(Name = "Last name: ")]
    public string lastName { get; set; } = default!;
    // User's last name, with required validation and a display name.

    [Required]
    [EmailAddress]
    [StringLength(40)]
    [Display(Name = "Email Address: ")]
    public string email { get; set; }
    // User's email address, with required validation, email format, and a display name.

    public string passwordHash { get; set; }
    // Hashed password for user authentication.

    public string? token { get; set; }
    // Authentication token for the user (nullable).

    public string role { get; set; } = "user";
    // User's role, defaulting to "user."
}