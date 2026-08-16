using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/auth")]
public sealed class AuthController(EComDbContext db, AuthService auth) : ControllerBase
{
    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.Name) || string.IsNullOrWhiteSpace(request.Email) || request.Password.Length < 8)
            return BadRequest(new { message = "Name, email and a password of at least 8 characters are required." });

        var email = request.Email.Trim().ToLowerInvariant();
        if (await db.Users.AnyAsync(x => x.Email == email)) return Conflict(new { message = "An account with this email already exists." });

        var user = new User { Name = request.Name.Trim(), Email = email, PasswordHash = auth.HashPassword(request.Password), Role = "CUSTOMER" };
        db.Users.Add(user);
        await db.SaveChangesAsync();
        return Ok(new { token = auth.CreateToken(user), user = new { user.Id, user.Name, user.Email, user.Role } });
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginRequest request)
    {
        var email = request.Email.Trim().ToLowerInvariant();
        var user = await db.Users.SingleOrDefaultAsync(x => x.Email == email && x.IsActive);
        if (user is null || !auth.VerifyPassword(request.Password, user.PasswordHash)) return Unauthorized(new { message = "Invalid email or password." });
        return Ok(new { token = auth.CreateToken(user), user = new { user.Id, user.Name, user.Email, user.Role } });
    }
}

public sealed record RegisterRequest(string Name, string Email, string Password);
public sealed record LoginRequest(string Email, string Password);
