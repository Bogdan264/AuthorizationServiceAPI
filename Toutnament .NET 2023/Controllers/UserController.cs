using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using Tournament_.NET_2023.Interface;
using Toutnament_.NET_2023.Models;

namespace Toutnament_.NET_2023.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UsersController : ControllerBase
{
    private readonly ApplicationDBContext _dbContext;

    public UsersController(ApplicationDBContext dbContext)
    {
        _dbContext = dbContext;
    }

    /// <summary>
    /// Get all users.
    /// </summary>
    [HttpGet]
    public async Task<IActionResult> GetUsers()
    {
        // Retrieve users from the database or wherever they are stored.
        // For demonstration purposes, I'm returning an empty list.
        var users = await _dbContext.Users.ToListAsync();
        return Ok(users);
    }

    // Other actions for registration, authentication, user management, etc.
}