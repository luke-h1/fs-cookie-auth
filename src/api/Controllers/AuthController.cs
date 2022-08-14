using api.Data;
using api.Dtos;
using api.Models;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController : Controller
{
  private readonly IUserRepository _repository;
  
  public AuthController(IUserRepository repository)
  {
   _repository = repository; 
  }

  [HttpPost("register")]
  [ProducesResponseType(200)]
  [ProducesResponseType(400)]
  public IActionResult Register(RegisterDto dto)
  {
    var user = new User
    {
      Name = dto.Name,
      Email = dto.Email,
      Password = BCrypt.Net.BCrypt.HashPassword(dto.Password)
    };
    var newUser = _repository.Create(user);
    return Ok(newUser);
  }

  [HttpPost("login")]
  [ProducesResponseType(200)]
  [ProducesResponseType(400)]
  public IActionResult Login(LoginDto dto)
  {
    var user = _repository.FindByEmail(dto.Email);

    if (user == null)
    {
      BadRequest(new { message = "Invalid credentials" });
    }
    if (!BCrypt.Net.BCrypt.Verify(dto.Password, user.Password))
    {
      BadRequest(new { message = "Invalid credentials" });
    }
    
    return Ok(user);
  }
}
