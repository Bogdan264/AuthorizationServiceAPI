using System.ComponentModel.DataAnnotations;
namespace Toutnament_.NET_2023.Models;

public class LoginModel {
    [Required(ErrorMessage = "Не указан Email")]
    public string Email { get; set; }
    
    [Required(ErrorMessage = "Не указан пароль")]
    [DataType(DataType.Password)]
    public string Password { get; set; }

    public bool RememberMe { get; set; }
}