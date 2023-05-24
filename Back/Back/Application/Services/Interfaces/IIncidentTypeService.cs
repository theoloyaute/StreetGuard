using Back.Domain.Models;

namespace Back.Application.Services.Interfaces;

public interface IIncidentTypeService
{
    Task<IEnumerable<IncidentType>> ListAsync();
    Task<IncidentType?> FindAsync(int id);
}