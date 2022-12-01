using WEB_053502_YUREV.Entities;

namespace WEB_053502_YUREV.Models;

public class CartItem
{
    public Item Item { get; set; }
    public int Count { get; set; }
}

public class Cart
{
    public Dictionary<Guid,CartItem> Items { get; set; }
    public Cart()
    {            
        Items = new Dictionary<Guid, CartItem>();
    }
    public int Count
    {
        get
        {
            return Items.Sum(item => item.Value.Count);
        }
    }
    public double Price
    {
        get
        {
            return Items.Sum(item => item.Value.Count * item.Value.Item.Price);
        }
    }
    public virtual void AddToCart(Item item)
    {
        if (Items.ContainsKey(item.Id))
            Items[item.Id].Count++;
        else
            Items.Add(item.Id, new CartItem {
                Item = item, Count = 1
            });
    }

    public virtual void RemoveOneFromCart(Item item)
    {
        if (!Items.ContainsKey(item.Id)) return;
        if (Items[item.Id].Count > 1)
            Items[item.Id].Count--;
        else
            Items.Remove(item.Id);
    }

    public virtual void RemoveFromCart(Guid id)
    {
        Items.Remove(id);
    }
    
    public virtual void ClearAll()
    {
        Items.Clear();
    }
}