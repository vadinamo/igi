using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WEB_053502_YUREV.Entities;

namespace WEB_053502_YUREV.Controllers;

public class ListDemo
{
    public int ListItemValue { get; set; }
    public string ListItemText { get; set; }
}

public class HomeController:Controller
{
    private readonly UserManager<ApplicationUser> _userManager;

    public HomeController(UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;
    }

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
    
    public FileResult GetAvatarFromBytes(byte[] bytesAvatar)
    {
        return File(bytesAvatar, "image/*");
    }

    [HttpGet]
    public async Task<IActionResult> GetAvatar()
    {
        var user = await _userManager.GetUserAsync(User);
        if (user == null)
        {
            return null;
        }

        if (user.Avatar == null) return NotFound();
        FileResult imageUserFile = GetAvatarFromBytes(user.Avatar);
        return imageUserFile;
    }
}
