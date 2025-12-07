using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

public class CategoryRequest
{
    [JsonPropertyName("name")]
    [Required]
    [StringLength(50)]
    public string Name { get; set; } = null!;
}
