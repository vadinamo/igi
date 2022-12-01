using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WEB_053502_YUREV.Data;
using WEB_053502_YUREV.Models;
using WEB_053502_YUREV.Extensions;

namespace WEB_053502_YUREV.Controllers;

public class CartController : Controller
{
    private ApplicationDbContext _context;
    private Cart _cart;
    
    public CartController(ApplicationDbContext context, Cart cart)
    {
        _context = context;
        _cart = cart;
    }
    
    // GET
    [Route("Cart")]
    public IActionResult Index()
    {
        return View(_cart.Items.Values);
    }
    
    [Authorize]
    public IActionResult Add(Guid id, string returnUrl)
    {
        var cart = HttpContext.Session.Get<Cart>("cart") ?? new Cart();
        var item = _context.Items.Find(id);
        if(item!=null)
        {
            cart.AddToCart(item);
            HttpContext.Session.Set<Cart>("cart",cart);
        }
        return Redirect(returnUrl);
    }

    public IActionResult RemoveOne(Guid id, string returnUrl)
    {
        var cart = HttpContext.Session.Get<Cart>("cart") ?? new Cart();
        var item = _context.Items.Find(id);
        if(item!=null)
        {
            cart.RemoveOneFromCart(item);
            HttpContext.Session.Set<Cart>("cart",cart);
        }
        return Redirect(returnUrl);
    }

    public IActionResult Delete(Guid id)
    {
        _cart.RemoveFromCart(id);
        return RedirectToAction("Index");
    }

    public IActionResult ClearAll()
    {
        _cart.ClearAll();
        return RedirectToAction("Index");
    }

    public int Count() => _cart.Count;

    public double Price() => _cart.Price;
}