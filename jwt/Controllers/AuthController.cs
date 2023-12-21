using jwt.Helpers;
using jwt.Models;
using jwt.Repositories;
using jwt.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace jwt.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IUserRepository _repository;
    private readonly JwtService _jwtService;

    public AuthController(IUserRepository repository, JwtService jwtService)
    {
        _repository = repository;
        _jwtService = jwtService;
    }

    [HttpPost("register")]
    public IActionResult Register(Register vm)
    {
        var user = new User
        {
            Name = vm.Name,
            Phone = vm.Phone,
            Password = BCrypt.Net.BCrypt.HashPassword(vm.Password)
        };

        return Created("success", _repository.Create(user));
    }

    [HttpPost("login")]
    public IActionResult Login(LoginVM vm)
    {
        var user = _repository.GetByPhone(vm.Phone);

        if (user == null) return BadRequest(new { message = "Invalid Credentials" });

        if (!BCrypt.Net.BCrypt.Verify(vm.Password, user.Password))
        {
            return BadRequest(new { message = "Invalid Credentials" });
        }

        var jwt = _jwtService.Generate(user.Id);

        Response.Cookies.Append("jwt", jwt, new CookieOptions
        {
            HttpOnly = true
        });

        return Ok(new
        {
            message = "success"
        });
    }

    [HttpGet("user")]
    public IActionResult User()
    {
        try
        {
            var jwt = Request.Cookies["jwt"];

            var token = _jwtService.Verify(jwt);

            int userId = int.Parse(token.Issuer);

            var user = _repository.GetById(userId);

            return Ok(user);
        }
        catch (Exception)
        {
            return Unauthorized();
        }
    }

    [HttpPost("logout")]
    public IActionResult Logout()
    {
        Response.Cookies.Delete("jwt");

        return Ok(new
        {
            message = "success"
        });
    }
}
