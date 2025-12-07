public class Portfolio
{
    public Guid Id { get; set; }
    public string Title { get; set; } = null!;
    public string Description { get; set; } = null!;
    public string? Image { get; set; } = null;
    public string? Link { get; set; } = null;
    public bool Status { get; set; } = false;
    public int CategoryId { get; set; }
    public Category Category { get; set; } = null!;
    public virtual List<Tool> Tools { get; set; } = [];
}
