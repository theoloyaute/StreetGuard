using Back.Domain.Models;
using Back.Domain.Repositories.Interfaces;
using Back.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Back.Domain.Repositories;

public class PowerIncidentTypeRepository : IPowerIncidentTypeRepository
{
    protected readonly AppDbContext _context;
    
    public PowerIncidentTypeRepository(AppDbContext context)
    {
        _context = context;
    }
    
    public async Task<IEnumerable<PowerIncidentType>> ListAsync() => await _context.PowerIncidentType
        .Include(x => x.IncidentType)
        .Include(x => x.Power)
        .ToListAsync();
}