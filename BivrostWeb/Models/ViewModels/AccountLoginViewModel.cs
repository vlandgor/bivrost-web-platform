using System.ComponentModel.DataAnnotations;

namespace BivrostWeb.Models.ViewModels;

public class AccountLoginViewModel
{
    [Required]
    public string Email { get; set; }
    [Required]
    public string Password { get; set; }
    
    public bool RememberMe { get; set; }
}