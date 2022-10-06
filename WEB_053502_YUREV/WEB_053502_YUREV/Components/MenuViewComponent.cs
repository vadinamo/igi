using Microsoft.AspNetCore.Mvc;
using WEB_053502_YUREV.Models;

namespace WEB_053502_YUREV.Components;

public class MenuViewComponent : ViewComponent
{
    private List<MenuItem> _menuItems = new List<MenuItem>
    {
        new MenuItem { 
            Controller="Home", 
            Action="Index", 
            Text="Lab 2"
        },
        new MenuItem { 
            Controller="Product", 
            Action="Index",
            Text="Каталог"
        },
        new MenuItem { 
            IsPage=true, 
            Area="Admin", 
            Page="/Index",
            Text="Администрирование"
        }
    };

    public IViewComponentResult Invoke()
    {
        var currentController = ViewContext.RouteData.Values["controller"];
        var currentPage = ViewContext.RouteData.Values["page"];
        var currentArea = ViewContext.RouteData.Values["area"];

        foreach (var item in _menuItems)
        {
            bool areaCheck = currentArea != null && currentArea.Equals(item.Area);
            bool controllerCheck = currentArea != null && currentArea.Equals(item.Controller);
            if (areaCheck || controllerCheck)
            {
                item.Active = "active";
            }
        }
        return View(_menuItems);
    }
}