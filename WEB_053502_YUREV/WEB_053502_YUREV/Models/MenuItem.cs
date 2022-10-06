namespace WEB_053502_YUREV.Models;

public class MenuItem
{
    public bool IsPage { get; set; } // является ли вызываемая ссылка страницей (Razor Page) или методом контроллера
    public string Text { get; set; } // текст надписи
    public string Controller { get; set; } // имя контроллера
    public string Action { get; set; } // имя метода
    public string Page { get; set; } // имя страницы
    public string Area { get; set; } // имя области (Area)
    public string Active { get; set; } // имя класса CSS для текущего (активного) пункта меню
}