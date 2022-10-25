using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using WEB_053502_YUREV.Entities;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;

namespace WEB_053502_YUREV.Controllers;

public class UserController : Controller
{
    UserManager<ApplicationUser> _userManager;
    IHostingEnvironment _env;

    public UserController(UserManager<ApplicationUser> mngr, IHostingEnvironment env)
    {           
        _userManager = mngr;
        _env = env;
    }

    public async Task<IActionResult> GetAvatar()
    {
        var user = await _userManager.GetUserAsync(User);
        if(user.Avatar!=null)
            return File(user.Avatar, user.ImageMimeType);
        else
        {                
            var avatarPath = "/Images/anonymous.png";                
            var extProvider = new FileExtensionContentTypeProvider();
            var mimeType = extProvider.Mappings[".png"];
            return File(_env.WebRootFileProvider.GetFileInfo(avatarPath).CreateReadStream(), 
                mimeType);
        }
    }
}