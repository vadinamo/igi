using Microsoft.AspNetCore.Mvc;

namespace WEB_053502_YUREV.Controllers;

public class ProductController:Controller
{
    public IActionResult Index()
    {
        return View();
    }
    
    public IActionResult UserProducts()
    {
        return View();
    }
}