using Back.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Back.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CityController : ControllerBase
{
    private readonly ICityService _cityService;
    
    public CityController(ICityService cityService)
    {
        _cityService = cityService;
    }
    
    [HttpGet]
    public async Task<IActionResult> GetAsync()
    {
        var cities = await _cityService.ListAsync();
        return Ok(cities);
    }
}