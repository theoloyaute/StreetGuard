using Back.Application.Services.Interfaces;
using Back.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace Back.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class IncidentTypeController : ControllerBase
{
    private readonly IIncidentTypeService _incidentTypeService;
    
    public IncidentTypeController(IIncidentTypeService incidentTypeService)
    {
        _incidentTypeService = incidentTypeService;
    }
    
    [HttpGet]
    public async Task<IEnumerable<IncidentType>> ListAsync() => await _incidentTypeService.ListAsync();
    
    [HttpGet("{id}")]
    public async Task<ActionResult<IncidentType>> FindAsync(int id)
    {
        var incidentType = await _incidentTypeService.FindAsync(id);
        if (incidentType == null)
        {
            return NotFound();
        }

        return incidentType;
    }
}