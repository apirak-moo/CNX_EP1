using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

public class ToolRequest
{

    [JsonPropertyName("name")]
    [StringLength(50)]
    [Required]
    public string Name { get; set; } = null!;

    [JsonPropertyName("logo")]
    public IFormFile? Logo { get; set; } = null;

}
