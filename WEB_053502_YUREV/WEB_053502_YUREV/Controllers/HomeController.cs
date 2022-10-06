using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WEB_053502_YUREV.Controllers;

public class ListDemo
{
    public int ListItemValue { get; set; }
    public string ListItemText { get; set; }
}

public class HomeController:Controller
{
    public IActionResult Index()
    {
        ViewData["Text"] = "Лабораторная работа №2";
        ViewData["ListDemo"] = new SelectList(new List<ListDemo>
        {
            new ListDemo {ListItemValue = 1, ListItemText = "Элемент 1"},
            new ListDemo {ListItemValue = 2, ListItemText = "Элемент 2"},
            new ListDemo {ListItemValue = 3, ListItemText = "Элемент 3"}
        }, "ListItemValue", "ListItemText");
        return View();
    }
}
