using System.Text.Json.Serialization;

public class PortfolioResponse
{

    [JsonPropertyName("id")]
    public Guid Id { get; set; }

    [JsonPropertyName("title")]
    public string Title { get; set; } = null!;

    [JsonPropertyName("description")]
    public string Description { get; set; } = null!;

    [JsonPropertyName("image")]
    public string? Image { get; set; } = null;

    [JsonPropertyName("link")]
    public string? Link { get; set; } = null;

    [JsonPropertyName("status")]
    public bool Status { get; set; } = false;

    [JsonPropertyName("category")]
    public CategoryResponse Category { get; set; } = null!;

    [JsonPropertyName("tools")]
    public List<ToolResponse> Tools { get; set; } = [];

}