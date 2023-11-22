using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tournament_.NET_2023.Interface;
using Toutnament_.NET_2023.Models;
using static Toutnament_.NET_2023.Models.Management;

namespace Tournament_.NET_2023.Controllers.Auth;

[Route("manage/")]
[ApiController]
[Authorize]
public class AcoountManagement :  ControllerBase, IUserManagementService
{
    private readonly ApplicationDBContext _dbContext;

    public AcoountManagement(ApplicationDBContext dbContext)
    {
        _dbContext = dbContext;
    }

    // Other methods...

    [HttpPost("change-password")]
    public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordModel model)
    {
        // Ensure the current user is the one making the request
        var userId = User.FindFirst(ClaimTypes.Name)?.Value;
        if (userId == null || userId != model.UserId)
        {
            return Forbid();
        }

        // Your logic to change the password
        // ...

        return Ok(new { message = "Password changed successfully" });
    }

    [HttpPost("change-email")]
    public async Task<IActionResult> ChangeEmail([FromBody] ChangeEmailModel model)
    {
        // Ensure the current user is the one making the request
        var userId = User.FindFirst(ClaimTypes.Name)?.Value;
        if (userId == null || userId != model.UserId)
        {
            return Forbid();
        }

        // Your logic to change the email
        // ...

        return Ok(new { message = $"Email changed successfully{userId}" });
    }

    [HttpPost("delete-user")]
    public async Task<IActionResult> DeleteUser([FromBody] DeleteUserModel model)
    {
        // Ensure the current user is the one making the request
        var userId = User.FindFirst(ClaimTypes.Name)?.Value;
        if (userId == null || userId != model.UserId)
        {
            return Forbid();
        }

        // Your logic to delete the user
        // ...

        return Ok(new { message = "User deleted successfully" });
    }
}