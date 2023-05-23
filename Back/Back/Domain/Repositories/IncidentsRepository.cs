using Back.Domain.Models;
using Back.Domain.Repositories.Interfaces;
using Back.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Back.Domain.Repositories;

public class IncidentsRepository : CommonRepository<Incidents>, IIncidentsRepository
{
    protected readonly AppDbContext _context;
    
    public IncidentsRepository(AppDbContext context) : base(context)
    {
        _context = context;
    }
    
    public new async Task<IEnumerable<Incidents>> ListAsync() => await _context.Incidents
        .Include(x => x.TypeIncident)
        .ToListAsync();
    
    public new async Task<Incidents> FindAsync(int id) => await _context.Incidents
        .Include(x => x.TypeIncident)
        .FirstOrDefaultAsync(x => x.Id == id);

    public int MaxId() => _context.Incidents.Max(x => x.Id);
}