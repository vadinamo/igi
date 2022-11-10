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

    public double Count()
    {
        return Items.Sum(i => i.Value.Count);
    }
    
    public double Price()
    {
        return Items.Sum(i => i.Value.Count * i.Value.Item.Price);
    }

    public void Add(Item item)
    {
        if (Items.ContainsKey(item.Id))
            Items[item.Id].Count++;
        // иначе - добавть объект в корзину
        else
            Items.Add(item.Id, new CartItem {
                Item = item,
                Count = 1
            });
    }
    
    public virtual void Remove(Guid id)
    {
        Items.Remove(id);
    }
    
    public virtual void Clear()
    {
        Items.Clear();
    }
}