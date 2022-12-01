using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WEB_053502_YUREV.Data;
using WEB_053502_YUREV.Entities;

namespace WEB_053502_YUREV.Controllers
{
    [Authorize(Policy = "AdminOnly")]
    public class ItemController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _hostEnvironment;
        [BindProperty] public InputModel Input { get; set; }

        public ItemController(ApplicationDbContext context, IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            _hostEnvironment = hostEnvironment;
        }

        // GET: Item
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Items.Include(i => i.Category);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Item/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null || _context.Items == null)
            {
                return NotFound();
            }

            var item = await _context.Items
                .Include(i => i.Category)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (item == null)
            {
                return NotFound();
            }

            return View(item);
        }

        // GET: Item/Create
        public IActionResult Create()
        {
            ViewData["CategoryId"] = new SelectList(_context.ItemCategories, "Id", "Id");
            return View();
        }

        // POST: Item/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Description,CategoryId,Price,Image")] Item item)
        {
            if (ModelState.IsValid)
            {
                item.Id = Guid.NewGuid();
                _context.Add(item);
                await _context.SaveChangesAsync();

                if (Input.ImageUpload == null) return RedirectToAction(nameof(Index));

                var image = item.Id + Path.GetExtension(Input.ImageUpload.FormFile.FileName);

                if (!Directory.Exists(Path.Combine(_hostEnvironment.WebRootPath, "images", "items")))
                {
                    Directory.CreateDirectory(Path.Combine(_hostEnvironment.WebRootPath, "images", "items"));
                }

                await using (var stream = new FileStream(Path.Combine(_hostEnvironment.WebRootPath, "images", "items",
                                 image), FileMode.Create))
                {
                    await Input.ImageUpload.FormFile.CopyToAsync(stream);
                }

                item.Image = "/images/items/" + image;
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            ViewData["CategoryId"] = new SelectList(_context.ItemCategories, "Id", "Id", item.CategoryId);
            return View(item);
        }

        // GET: Item/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null || _context.Items == null)
            {
                return NotFound();
            }

            var item = await _context.Items.FindAsync(id);
            if (item == null)
            {
                return NotFound();
            }

            ViewData["CategoryId"] = new SelectList(_context.ItemCategories, "Id", "Id", item.CategoryId);
            return View(item);
        }

        // POST: Item/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Name,Description,CategoryId,Price,Image")] Item item)
        {
            if (id != item.Id)
            {
                return NotFound();
            }

            try
            {
                if (Input.ImageUpload != null)
                {
                    var image = item.Id + Path.GetExtension(Input.ImageUpload.FormFile.FileName);

                    if (item.Image != "")
                    {
                        var file = new DirectoryInfo((Path.Combine(_hostEnvironment.WebRootPath, "images", "items")))
                            .GetFiles(item.Id.ToString() + "*.*");

                        var imageToDelete = "/images/items/" + (file.Length > 0 ? file[0].Name : "");
                        if (System.IO.File.Exists(_hostEnvironment.WebRootPath + imageToDelete))
                        {
                            System.IO.File.Delete(_hostEnvironment.WebRootPath + imageToDelete);
                        }
                    }

                    if (!Directory.Exists(Path.Combine(_hostEnvironment.WebRootPath, "images", "items")))
                    {
                        Directory.CreateDirectory(Path.Combine(_hostEnvironment.WebRootPath, "images", "items"));
                    }

                    await using (var stream = new FileStream(Path.Combine(_hostEnvironment.WebRootPath, "images",
                                     "items",
                                     image), FileMode.Create))
                    {
                        await Input.ImageUpload.FormFile.CopyToAsync(stream);
                    }

                    item.Image = "/images/items/" + image;
                }

                else
                {
                    var image = new DirectoryInfo((Path.Combine(_hostEnvironment.WebRootPath, "images", "items")))
                        .GetFiles(item.Id.ToString() + "*.*");

                    item.Image = "/images/items/" + (image.Length > 0 ? image[0].Name : "");
                }

                _context.Update(item);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ItemExists(item.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToAction(nameof(Index));
        }

        // GET: Item/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null || _context.Items == null)
            {
                return NotFound();
            }

            var item = await _context.Items
                .Include(i => i.Category)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (item == null)
            {
                return NotFound();
            }

            return View(item);
        }

        // POST: Item/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            if (_context.Items == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Items'  is null.");
            }

            var item = await _context.Items.FindAsync(id);
            if (item != null)
            {
                _context.Items.Remove(item);
                if (item.Image != "" && System.IO.File.Exists(_hostEnvironment.WebRootPath + item.Image))
                {
                    System.IO.File.Delete(_hostEnvironment.WebRootPath + item.Image);
                }
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ItemExists(Guid id)
        {
            return (_context.Items?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        public class InputModel
        {
            [BindProperty] public FileUpload ImageUpload { get; set; }
        }
    }
}