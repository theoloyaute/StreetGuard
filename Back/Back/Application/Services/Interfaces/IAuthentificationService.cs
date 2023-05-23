using System.Security.Claims;
using Back.Domain.Models;

namespace Back.Application.Services.Interfaces;

public interface IAuthentificationService
{
    string GenerateToken(Users user);
    Users Auth(string username, string password);
}