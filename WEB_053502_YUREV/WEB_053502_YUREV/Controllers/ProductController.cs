using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WEB_053502_YUREV.Data;
using WEB_053502_YUREV.Entities;

namespace WEB_053502_YUREV.Controllers
{
    public class ProductController : Controller
    {
        private readonly ApplicationDbContext _context;
        
        public ProductController(ApplicationDbContext context)
        {
            _context = context;
        }
        
        // GET: Product
        public async Task<IActionResult> Index()
        {
            return _context.Items != null ? 
                View(await _context.Items.ToListAsync()) :
                Problem("Entity set is null.");
        }
        
        public ActionResult UserProducts()
        {
            return View();
        }

        // GET: Product/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }
        
        // GET: Product/Create
        public ActionResult Create()
        {
            return View();
        }
        
        // POST: Product/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here
        
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
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
    }
}