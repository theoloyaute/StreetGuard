using Back.Domain.Models;
using Back.Domain.Repositories.Interfaces;
using Back.Infrastructure.Context;

namespace Back.Domain.Repositories;

public class RoleRepository : CommonRepository<Role>, IRoleRepository
{
    private readonly AppDbContext _context;
    
    public RoleRepository(AppDbContext context) : base(context)
    {
        _context = context;
    }
}