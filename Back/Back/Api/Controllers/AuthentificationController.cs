using System.Security.Claims;
using Back.Api.Error;
using Back.Application.Services.Interfaces;
using Back.Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Back.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthentificationController : ControllerBase
{
    private readonly IAuthentificationService _authentificationService;
    
    public AuthentificationController(IAuthentificationService authentificationService)
    {
        _authentificationService = authentificationService;
    }
    
    [HttpPost]
    public IActionResult Login([FromBody] Login model)
    {
        try
        {
            var user = _authentificationService.Auth(model.Username, model.Password);
            var token = _authentificationService.GenerateToken(user);
            return Ok(new JsonResult(token));
        }
        catch (Exception e)
        {
            return BadRequest(new ApiResponse(400, e.Message));
        }
    }
}