using Back.Application.Services.Interfaces;
using Back.Domain.Models;
using Back.Domain.Repositories.Interfaces;

namespace Back.Application.Services;

public class CityService : ICityService
{
    private readonly ICityRepository _cityRepository;
    
    public CityService(ICityRepository cityRepository)
    {
        _cityRepository = cityRepository;
    }
    
    public Task<IEnumerable<City>> ListAsync() => _cityRepository.ListAsync();
}