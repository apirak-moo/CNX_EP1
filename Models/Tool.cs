public class Tool
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string? Logo { get; set; } = null;
    public virtual ICollection<Portfolio> Portfolios { get; set; } = [];
}
