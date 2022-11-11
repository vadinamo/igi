using Microsoft.AspNetCore.Mvc;
using WEB_053502_YUREV.Data;
using WEB_053502_YUREV.Entities;
using WEB_053502_YUREV.Models;

namespace WEB_053502_YUREV.Controllers;

public class ProductController : Controller
{
     ApplicationDbContext _context;
    int _pageSize;
        
    public ProductController(ApplicationDbContext context)
    {
        _pageSize = 3;
        _context = context;
    }

    [Route("Catalog")]
    [Route("Catalog/Page_{page=1}")]
    public IActionResult Index(Guid? category, int page)
    {
        var filteredItems = _context.Items
            .Where(i => !category.HasValue || i.CategoryId == category.Value);
        
        ViewData["Categories"] = _context.ItemCategories;
        ViewData["CurrentCategory"] = category ?? Guid.Empty;
            
        return _context.Items != null ? 
            View(ListViewModel<Item>.GetModel(filteredItems, page, _pageSize)) :
            Problem("Entity set is null.");
    }
}