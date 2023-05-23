using Back.Domain.Models;

namespace Back.Domain.Repositories.Interfaces;

public interface IIncidentsRepository : ICommonRepository<Incidents>
{
    int MaxId();
}