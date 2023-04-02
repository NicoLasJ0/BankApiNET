using Microsoft.AspNetCore.Mvc;
using BankApi.Data.DTOs;
using BankApi.Services;
using BankApi.Data.BankModels;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;


using System.Text;
using System.IdentityModel.Tokens.Jwt;

namespace BankApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class LoginController : ControllerBase
{
    private readonly LoginService _service;
    private readonly IConfiguration config;
    public LoginController(LoginService service, IConfiguration configuration)
    {
        config= configuration;
        _service= service;
    }
    [HttpPost("auth")]
    public async Task<IActionResult> Login(AdminDto admin)
    {
        var administrator= await _service.GetAdmin(admin);
        if(administrator is null)
        {
            return BadRequest(new {message= "Invalid credentials"});
        }
        string token= GenerateToken(administrator);
        return Ok(new {token= token});
    }
    private string GenerateToken(Administrator administrator)
    {
        var claims= new[]
        {
            new Claim(ClaimTypes.Name, administrator.Name),
            new Claim(ClaimTypes.Email, administrator.Email),
            new Claim("AdminType", administrator.AdminType)
        };
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config.GetSection("JWT:Key").Value));
        var creds= new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
        var securityToken= new JwtSecurityToken(
            claims: claims,
            expires: DateTime.Now.AddMinutes(60),
            signingCredentials: creds
        );
        string token= new JwtSecurityTokenHandler().WriteToken(securityToken);
        return token;
    }
}