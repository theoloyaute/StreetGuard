using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Back.Api.Error;
using Back.Application.Services.Interfaces;
using Back.Domain.Models;
using Microsoft.IdentityModel.Tokens;

namespace Back.Application.Services;

public class AuthentificationService : IAuthentificationService
{
    protected readonly IConfiguration _configuration;
    private readonly IUsersService _usersService;
    
    public AuthentificationService(IConfiguration configuration, IUsersService usersService)
    {
        _configuration = configuration;
        _usersService = usersService;
    }
    
    public string GenerateToken(Users user)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
        var claims = new[]
        {
            new Claim(ClaimTypes.Name, user.Username),
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Role, user.Role?.Name!)
        };
        claims = claims.Where(x => x.Value != null).ToArray();

        var token = new JwtSecurityToken(_configuration["Jwt:Issuer"],
            _configuration["Jwt:Audience"],
            claims,
            expires: DateTime.Now.AddMinutes(15),
            signingCredentials: credentials);


        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public Users Auth(string username, string password)
    {
        var user = _usersService.FindByName(username).Result;
        if (user is null) throw new NotFoundException("Utilisateur incorrect !");
        if (!BCrypt.Net.BCrypt.Verify(password, user.Password)) 
            throw new BadRequestException("Mot de passe incorrect !");
        return user;
    }
}