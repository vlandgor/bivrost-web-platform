using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models;

public class AccountSignUpViewModel
{
    [Required]
    public string Username { get; set; }
    [Required]
    public string Email { get; set; }
    [Required]
    public string Password { get; set; }
    [Required]
    public string ConfirmPassword { get; set; }
}