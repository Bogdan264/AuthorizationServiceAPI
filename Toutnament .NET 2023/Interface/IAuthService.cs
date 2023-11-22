using Microsoft.AspNetCore.Mvc;
using Toutnament_.NET_2023.Models;

namespace Tournament_.NET_2023.Interface;

public interface IAuthService
{
    Task<IActionResult> AuthenticateWithEmail([FromBody] LoginModel model);
    Task<IActionResult> RegisterWithEmail([FromBody] RegisterModel model);
//    bool AuthenticateWithGoogle(string googleToken);
//    bool AuthenticateWithApple(string appleToken);
}