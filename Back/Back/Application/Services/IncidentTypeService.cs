using Back.Application.Services.Interfaces;
using Back.Domain.Models;
using Back.Domain.Repositories.Interfaces;

namespace Back.Application.Services;

public class IncidentTypeService : IIncidentTypeService
{
    private readonly IIncidentTypeRepository _incidentTypeRepository;

    public IncidentTypeService(IIncidentTypeRepository incidentTypeRepository)
    {
        _incidentTypeRepository = incidentTypeRepository;
    }
    
    public Task<IEnumerable<TypeIncident>> ListAsync() => _incidentTypeRepository.ListAsync();
    
    public Task<TypeIncident> FindAsync(int id) => _incidentTypeRepository.FindAsync(id);
}