using Back.Application.Services.Interfaces;
using Back.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace Back.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class IncidentsController : ControllerBase
{
    private readonly IIncidentsService _incidentsService;
    
    public IncidentsController(IIncidentsService incidentsService)
    {
        _incidentsService = incidentsService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await _incidentsService.ListAsync();
        return Ok(result);
    }
    
    [HttpGet("{id:int}")]
    public async Task<IActionResult> Get(int id)
    {
        var result = await _incidentsService.FindAsync(id);
        return Ok(result);
    }
    
    [HttpPost]
    public async Task<IActionResult> Post([FromBody] Incidents incidents)
    {
        var result = await _incidentsService.Add(incidents);
        return Ok(result);
    }
    
    [HttpPut]
    public async Task<IActionResult> Put([FromBody] Incidents incidents)
    {
        var result = await _incidentsService.Update(incidents);
        return Ok(result);
    }
    
    [HttpDelete("{id:int}")]
    public IActionResult Delete(int id)
    {
        _incidentsService.Delete(id);
        return Ok();
    }
    
    [HttpGet("find/{powerId:int}/{longitude:double}/{latitude:double}")]
    public async Task<IActionResult> FindIncidents(int powerId, double longitude, double latitude)
    {
        var result = await _incidentsService.FindIncidents(powerId, longitude, latitude);
        return Ok(result);
    }
}