using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WEB_053502_YUREV.Data;
using WEB_053502_YUREV.Entities;
using WEB_053502_YUREV.Models;

namespace WEB_053502_YUREV.Controllers
{
    public class ProductController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _hostEnvironment;
        int _pageSize;
        [BindProperty] public InputModel Input { get; set; }
        
        public ProductController(ApplicationDbContext context, IWebHostEnvironment hostEnvironment)
        {
            _pageSize = 3;
            _context = context;
            _hostEnvironment = hostEnvironment;
        }
        
        // GET: Product
        [Route("Catalog")]
        [Route("Catalog/Page_{pageNo=1}")]
        public async Task<IActionResult> Index(Guid category, int pageNo)
        {
            var filteredItems = _context.Items.Where(i => category == Guid.Empty || i.CategoryId == category);
        
            ViewData["Groups"] = _context.ItemCategories;
            var currentGroup = category != Guid.Empty ? category : Guid.Empty;
            ViewData["CurrentGroup"] = currentGroup;
            
            return _context.Items != null ? 
                View(ListViewModel<Item>.GetModel(filteredItems, pageNo, _pageSize)) :
                Problem("Entity set is null.");
        }

        // GET: Product/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }
        
        // GET: Product/Create
        public IActionResult Create()
        {
            ViewData["Categories"] = _context.ItemCategories.ToList();
            return View();
        }
        
        // POST: Product/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Description,Category,Price,Image")] Item item)
        {
            
            if (ModelState.IsValid)
            {
                item.Id = Guid.NewGuid();
                if (Input.ImageUpload != null)
                {
                    using (var stream = new FileStream(Path.Combine(_hostEnvironment.WebRootPath, "cars",
                               item.Id.ToString("N")), FileMode.Create))
                    {
                        await Input.ImageUpload.FormFile.CopyToAsync(stream);
                    }

                    item.Image = item.Id.ToString("N") + Input.ImageUpload.FormFile.FileName.Split('.')[1];
                }
                
                item.Category = _context.ItemCategories.
                    FirstOrDefaultAsync(m => m.Id == item.Id).Result;

                _context.Add(item);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(item);
        }
        
        // GET: Product/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }
        
        // POST: Product/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here
        
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
        
        // GET: Product/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }
        
        // POST: Product/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here
        
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
        
        public class InputModel
        {
            [BindProperty] public FileUpload ImageUpload { get; set; }
        }
    }
}