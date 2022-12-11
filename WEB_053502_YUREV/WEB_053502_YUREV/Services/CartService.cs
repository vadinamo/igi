using Newtonsoft.Json;
using WEB_053502_YUREV.Entities;
using WEB_053502_YUREV.Extensions;
using WEB_053502_YUREV.Models;

namespace WEB_053502_YUREV.Services;

public class CartService : Cart
{
    private string sessionKey = "cart";

    [JsonIgnore]
    ISession Session { get; set; }

    public static Cart GetCart(IServiceProvider sp)
    {
        var session = sp.GetRequiredService<IHttpContextAccessor>().HttpContext.Session;

        var cart = session?.Get<CartService>("cart")
                   ?? new CartService();
        cart.Session = session;
        return cart;
    }
    public override void AddToCart(Item item)
    {
        base.AddToCart(item);
        Session?.Set<CartService>(sessionKey, this);
    }

    public override void RemoveOneFromCart(Item item)
    {
        base.RemoveOneFromCart(item);
        Session?.Set<CartService>(sessionKey, this);
    }
    public override void RemoveFromCart(Guid id)
    {
        base.RemoveFromCart(id);
        Session?.Set<CartService>(sessionKey, this);
    }
    public override void ClearAll()
    {
        base.ClearAll();
        Session?.Set<CartService>(sessionKey, this);
    }
}