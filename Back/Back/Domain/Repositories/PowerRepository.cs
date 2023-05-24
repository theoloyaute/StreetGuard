using Back.Domain.Models;
using Back.Domain.Repositories.Interfaces;
using Back.Infrastructure.Context;

namespace Back.Domain.Repositories;

public class PowerRepository : CommonRepository<Power>, IPowerRepository
{
    public PowerRepository(AppDbContext context) : base(context)
    {
    }
}