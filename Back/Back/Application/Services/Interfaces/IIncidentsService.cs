using Back.Domain.Models;

namespace Back.Application.Services.Interfaces;

public interface IIncidentsService
{
    Task<IEnumerable<Incidents>> ListAsync();
    Task<Incidents?> FindAsync(int id);
    Task<Incidents> Add(Incidents incidents);
    Task<Incidents> Update(Incidents incidents);
    void Delete(int id);
}