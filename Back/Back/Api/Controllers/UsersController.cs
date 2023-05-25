using Back.Application.Services.Interfaces;
using Back.Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Back.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private readonly IUsersService _usersService;
    
    public UsersController(IUsersService usersService)
    {
        _usersService = usersService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await _usersService.ListAsync();
        return Ok(result);
    }
    
    [Authorize]
    [HttpGet("{id:int}")]
    public async Task<IActionResult> Get(int id)
    {
        var result = await _usersService.FindAsync(id);
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] Users users)
    {
        var result = await _usersService.Add(users);
        return Ok(result);
    }

    [HttpPut]
    public async Task<IActionResult> Put([FromBody] Users users)
    {
        var result = await _usersService.Update(users);
        return Ok(result);
    }
    
    [HttpDelete("{id:int}")]
    public IActionResult Delete(int id)
    {
        _usersService.Delete(id);
        return Ok();
    }
}