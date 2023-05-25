using Back.Domain.Models;
using Back.Domain.Repositories.Interfaces;
using Back.Infrastructure.Context;

namespace Back.Domain.Repositories;

public class CityRepository : CommonRepository<City>, ICityRepository
{
    public CityRepository(AppDbContext context) : base(context)
    {
    }
}