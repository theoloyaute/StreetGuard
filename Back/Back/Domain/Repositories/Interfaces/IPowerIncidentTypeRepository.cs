using Back.Domain.Models;

namespace Back.Domain.Repositories.Interfaces;

public interface IPowerIncidentTypeRepository
{
    Task<IEnumerable<PowerIncidentType>> ListAsync();
}