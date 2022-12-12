namespace WEB_053502_YUREV.Blazor.Shared;

public class Item
{
    public Guid Id { get; set; } 
    public string? Name { get; set; }
    public string? Description { get; set; }
    public Guid CategoryId { get; set; }
    public ItemCategory? Category { get; set; }
    public double Price { get; set; }
    public string? Image { get; set; }
}