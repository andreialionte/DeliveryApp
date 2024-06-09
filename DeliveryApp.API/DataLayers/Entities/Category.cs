using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

public class Category
{
    [Key]
    public int CategoryId { get; set; }

    [MaxLength(50)]
    public string? Name { get; set; }
    [JsonIgnore]
    public ICollection<MenuItem>? MenuItems { get; set; }
}
