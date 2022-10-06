using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace WEB_053502_YUREV.Components;

public class CartViewComponent : ViewComponent
{
    public IViewComponentResult Invoke()
    {
        return View();
    }
}