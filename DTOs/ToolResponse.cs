using System.Text.Json.Serialization;

public class ToolResponse
{

    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; } = null!;

    [JsonPropertyName("logo")]
    public string? Logo { get; set; } = null;

}
