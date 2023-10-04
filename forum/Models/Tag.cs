using System.ComponentModel.DataAnnotations;

namespace Forum.Models;

public class Tag
{
    [Key]
    public int id { get; set; } // Unique identifier for the tag.


    public string name { get; set; } = default!;    // Name of the tag.

}