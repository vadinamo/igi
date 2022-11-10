using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using WEB_053502_YUREV.Entities;

namespace WEB_053502_YUREV.Data;

public interface IDbInitializer
{
    void Initialize();
}

public class DbInitializer : IDbInitializer
{
    private readonly ApplicationDbContext _context;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    
    public DbInitializer(
        ApplicationDbContext context,
        UserManager<ApplicationUser> userManager,
        RoleManager<IdentityRole> roleManager)
    {
        _context = context;
        _userManager = userManager;
        _roleManager = roleManager;
    }
    
    public async void Initialize()
    {
        await _context.Database.EnsureCreatedAsync();
        
        if (!_context.ItemCategories.Any())
        {
            var itemCategoryList = new List<ItemCategory>()
            {
                new ItemCategory() { Id = Guid.NewGuid(), CategoryName = "Shirt" },
                new ItemCategory() {Id = Guid.NewGuid(), CategoryName = "Headdress"},
                new ItemCategory() {Id = Guid.NewGuid(), CategoryName = "Jacket"},
                new ItemCategory() {Id = Guid.NewGuid(), CategoryName = "Pants"},
                new ItemCategory() {Id = Guid.NewGuid(), CategoryName = "Shoes"}
            };
            
            foreach (var category in itemCategoryList)
            {
                _context.ItemCategories.Add(category);
                await _context.SaveChangesAsync();
            }
        }
        
        if (!_context.Items.Any())
        {
            var items = new List<Item>()
            {
                new Item()
                {
                    Id = Guid.NewGuid(), Name = "Shirt", Description = "shirt description", Image = "/images/shirt.jpg",
                    CategoryId = _context.ItemCategories.FirstOrDefault(i => i.CategoryName == "Shirt").Id, Price = 1
                },
                new Item()
                {
                    Id = Guid.NewGuid(), Name = "Pants", Description = "pants description", Image = "/images/pants.jpg",
                    CategoryId = _context.ItemCategories.FirstOrDefault(i => i.CategoryName == "Pants").Id, Price = 2
                },
                new Item()
                {
                    Id = Guid.NewGuid(), Name = "Headdress", Description = "headdress description", Image = "/images/cap.jpg",
                    CategoryId = _context.ItemCategories.FirstOrDefault(i => i.CategoryName == "Headdress").Id, Price = 3
                },
                new Item()
                {
                    Id = Guid.NewGuid(), Name = "Jacket", Description = "jacket description", Image = "/images/jacket.jpg",
                    CategoryId = _context.ItemCategories.FirstOrDefault(i => i.CategoryName == "Jacket").Id, Price = 4
                },
                new Item()
                {
                    Id = Guid.NewGuid(), Name = "Shoes", Description = "shoes description", Image = "/images/shoe.jpg",
                    CategoryId = _context.ItemCategories.FirstOrDefault(i => i.CategoryName == "Shoes").Id, Price = 6
                }
            };

            foreach (var item in items)
            {
                _context.Items.Add(item);
                await _context.SaveChangesAsync();
            }
        }

        if (_context.Users.Any())
            return;
        
        await _roleManager.CreateAsync(new IdentityRole("admin"));
        await _roleManager.CreateAsync(new IdentityRole("user"));
        
        ApplicationUser admin = new ApplicationUser {
            UserName = "Admin",
            Email = "admin@gmail.com",
            EmailConfirmed = true               
        };
        
        ApplicationUser user = new ApplicationUser {
            UserName = "User",
            Email = "user@gmail.com",
            EmailConfirmed = true               
        };
        
        string password = "12345678";
        
        var result = await _userManager.CreateAsync(admin, password);
        if (result.Succeeded)
        {
            await _userManager.AddToRoleAsync(admin, "admin");
        }

        result = await _userManager.CreateAsync(user, password);
        if (result.Succeeded)
        {
            await _userManager.AddToRoleAsync(user, "user");
        }

        await _context.SaveChangesAsync();
    }
}