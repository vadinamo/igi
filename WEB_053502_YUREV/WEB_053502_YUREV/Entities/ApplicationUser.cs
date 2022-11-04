using Microsoft.AspNetCore.Identity;

namespace WEB_053502_YUREV.Entities;

public class ApplicationUser : IdentityUser
{
    public byte[] Avatar { get; set; }
}