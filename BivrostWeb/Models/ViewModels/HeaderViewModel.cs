namespace BivrostWeb.Models.ViewModels;

public class HeaderViewModel(string accountColor)
{
    public string AccountColor { get; } = accountColor;
}