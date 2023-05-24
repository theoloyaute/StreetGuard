using Back.Domain.Models;
using Back.Domain.Repositories.Interfaces;
using Back.Infrastructure.Context;

namespace Back.Domain.Repositories;

public class IncidentTypeRepository : CommonRepository<IncidentType>, IIncidentTypeRepository
{
    public IncidentTypeRepository(AppDbContext context) : base(context)
    {
    }
}