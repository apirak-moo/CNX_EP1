using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

public class PortfolioRequest
{

    [JsonPropertyName("title")]
    [Required]
    [StringLength(50)]
    public string Title { get; set; } = null!;

    [JsonPropertyName("description")]
    [Required]
    public string Description { get; set; } = null!;

    [JsonPropertyName("image")]
    [Required]
    public IFormFile? Image { get; set; } = null;

    [JsonPropertyName("link")]
    [Required]
    public string? Link { get; set; } = null;

    [JsonPropertyName("status")]
    [Required]
    public bool Status { get; set; } = false;

    [JsonPropertyName("category")]
    [Required]
    public int CategoryId { get; set; }

    [JsonPropertyName("tools")]
    [Required]
    public List<int> ToolsId { get; set; } = [];
}
