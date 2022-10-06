using Microsoft.AspNetCore.Mvc;

namespace WEB_053502_YUREV.Controllers;

public class CartController:Controller
{
    public IActionResult Index()
    {
        return View();
    }
}