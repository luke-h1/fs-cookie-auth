using api.Data;
using api.Dtos;
using api.helpers;
using api.Models;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController : Controller
{
  private readonly IUserRepository _repository;
  private readonly JwtService _jwtService;

  public AuthController(IUserRepository repository, JwtService jwtService)
  {
    _repository = repository;
    _jwtService = jwtService;
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
    
    var jwt = _jwtService.Generate(user.Id);
    
    Response.Cookies.Append("token", jwt, new CookieOptions
    {
      HttpOnly = true,
      // Expires = DateTime.Now.AddDays(7)
      // Secure = // probably would get this from config i.e. frontendUrl = 'deployed url' ? true : false
    });
    
    return Ok(new { message = "Succesfully logged in" });
  }

  [HttpGet("user")]
  [ProducesResponseType(200)]
  [ProducesResponseType(401)]
  public IActionResult User()
  {
    try
    {
      var jwt = Request.Cookies["token"];
      var token = _jwtService.Verify(jwt);

      int userId = int.Parse(token.Issuer);

      var user = _repository.FindById(userId);
      
      return Ok(user);
    }
    catch (Exception e)
    { 
      return Unauthorized();
    }
  }
}
