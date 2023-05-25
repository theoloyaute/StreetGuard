using Back.Domain.Models;

namespace Back.Application.Services.Interfaces;

public interface ICityService
{
    Task<IEnumerable<City>> ListAsync();
}