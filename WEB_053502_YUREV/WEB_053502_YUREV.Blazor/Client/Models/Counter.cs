using System.ComponentModel.DataAnnotations;

namespace WEB_053502_YUREV.Blazor.Client.Models;

public class CustomCounter
{
    [Required]
    [Range(int.MinValue, int.MaxValue, ErrorMessage = "Required numbers only in -2,147,483,648...2,147,483,647 interval")]
    public int newCount { get; set; }
}