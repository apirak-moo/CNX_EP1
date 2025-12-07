using System.Text.Json.Serialization;

public class CategoryResponse
{

    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; } = null!;

}
