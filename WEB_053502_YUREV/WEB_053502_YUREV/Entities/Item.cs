using System.ComponentModel;

namespace WEB_053502_YUREV.Entities;

public class Item
{
    public Guid Id { get; set; } 
    public string? Name { get; set; }
    public string? Description { get; set; }
    public ItemCategory? Category { get; set; }
    public int Price { get; set; }
    public byte[]? Image { get; set; }
}