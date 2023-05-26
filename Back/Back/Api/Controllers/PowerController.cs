using Back.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Back.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PowerController : ControllerBase
{
    private readonly IPowerService _powerService;
    
    public PowerController(IPowerService powerService)
    {
        _powerService = powerService;
    }
    
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await _powerService.ListAsync();
        return Ok(result);
    }
}