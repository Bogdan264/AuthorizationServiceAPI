using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Tournament_.NET_2023.Models;
using Tournament_.NET_2023.Interface;
using Toutnament_.NET_2023.Models;

namespace Tournament_.NET_2023.Controllers.Auth;

[Route("auth/")]
[ApiController]
public class AuthService : ControllerBase, IAuthService
{
    private ApplicationDBContext _dbContext;

    public AuthService(ApplicationDBContext dbContext)
    {
        _dbContext = dbContext;
    }

    [HttpPost("logout")]
    [Authorize]
    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return Ok(new {message = "Succesfull logout"});
    }

    [HttpPost("login")]
    public async Task<IActionResult> AuthenticateWithEmail([FromBody] LoginModel model)
    {
        if (ModelState.IsValid)
        {
            User user = await _dbContext.Users.FirstOrDefaultAsync(
                u => u.Email == model.Email && u.PasswordHash == Convert.ToHexString(
                    SHA256.Create()
                        .ComputeHash(Encoding.ASCII.GetBytes(
                            model.Password))));
            if (user != null)
            {
                if (!user.IsActive)
                {
                    return Unauthorized(new { message = "Your account has been blocked" });
                }
                var claims = new[]
                {
                    new Claim(ClaimTypes.Name, user.Email),
                    //new Claim(ClaimTypes.Role, "User") // You can customize roles as needed
                };

                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                var principal = new ClaimsPrincipal(identity);

                await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, new AuthenticationProperties
                {
                    IsPersistent = model.RememberMe, // Set whether to persist the cookie
                    ExpiresUtc = model.RememberMe ? DateTime.UtcNow.AddMonths(1) : DateTime.UtcNow.AddMinutes(20) // Set the expiration time
                });

                return Ok(new { message = "Authentication successful" });
            }
            return Unauthorized(new { message = "Invalid email or password" });
        }
        return BadRequest(ModelState);
    }

    [HttpPost("register")]
    public async Task<IActionResult> RegisterWithEmail([FromBody] RegisterModel model)
    {
        if (ModelState.IsValid)
        {
            // Check if the email is already registered
            if (await _dbContext.Users.AnyAsync(u => u.Email == model.Email))
            {
                return BadRequest(new { message = "Email is already registered." });
            }

            // You might want to add additional validation and error handling

            var newUser = new User
            {
                Email = model.Email,
                PasswordHash = Convert.ToHexString(SHA256.Create().ComputeHash(Encoding.ASCII.GetBytes(model.Password))), // In a real application, hash and salt the password
                IsActive = true
            };

            // Add the new user to the database
            _dbContext.Users.Add(newUser);
            await _dbContext.SaveChangesAsync();

            return Ok(new { message = "Registration successful" });
        }

        return BadRequest(ModelState);
    }
}