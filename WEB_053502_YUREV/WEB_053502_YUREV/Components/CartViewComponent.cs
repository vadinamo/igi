using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using WEB_053502_YUREV.Models;

namespace WEB_053502_YUREV.Components;

public class CartViewComponent : ViewComponent
{
    private Cart _cart;
    
    public CartViewComponent(Cart cart)
    {
        _cart = cart;
    }
    
    public async Task<IViewComponentResult> InvokeAsync()
    {
        return View(_cart);
    }
}