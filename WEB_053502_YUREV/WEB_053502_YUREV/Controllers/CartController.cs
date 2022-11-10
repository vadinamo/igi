using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WEB_053502_YUREV.Data;
using WEB_053502_YUREV.Models;

namespace WEB_053502_YUREV.Controllers;

public class CartController:Controller
{
    private ApplicationDbContext _context;
    private Cart _cart;

    public CartController (ApplicationDbContext context, Cart cart)
    {
        _context = context;
        _cart = cart;
    }
    
    public IActionResult Index()
    {
        return View();
    }
    
    [Authorize]
    public IActionResult Add(Guid id, string returnUrl)
    {
        var item = _context.Items.Find(id);
        
        if(item!=null)
            _cart.Add(item);
        
        return Redirect(returnUrl);
    }
}