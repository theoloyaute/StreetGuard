using Back.Application.Services.Interfaces;
using Back.Domain.Models;
using Back.Domain.Repositories.Interfaces;

namespace Back.Application.Services;

public class PowerService : IPowerService
{
    private readonly IPowerRepository _powerRepository;
    
    public PowerService(IPowerRepository powerRepository)
    {
        _powerRepository = powerRepository;
    }
    
    public async Task<IEnumerable<Power>> ListAsync() => await _powerRepository.ListAsync();
}