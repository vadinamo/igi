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