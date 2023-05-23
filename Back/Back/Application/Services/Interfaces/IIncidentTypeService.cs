using Back.Domain.Models;

namespace Back.Application.Services.Interfaces;

public interface IIncidentTypeService
{
    Task<IEnumerable<TypeIncident>> ListAsync();
    Task<TypeIncident> FindAsync(int id);
}