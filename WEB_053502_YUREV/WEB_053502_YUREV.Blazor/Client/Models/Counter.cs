using System.ComponentModel.DataAnnotations;

namespace WEB_053502_YUREV.Blazor.Client.Models;

public class CustomCounter
{
    [Required]
    [Range(0, int.MaxValue, ErrorMessage = "The field CounterValue must be between 0 and 2147483647")]
    public int newCount { get; set; }
}