using Back.Domain.Models;
using Back.Domain.Repositories.Interfaces;
using Back.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Back.Domain.Repositories;

public class UsersRepository : CommonRepository<Users>, IUsersRepository
{
    protected readonly AppDbContext _context;

    public UsersRepository(AppDbContext context) : base(context)
    {
        _context = context;
    }
    
    public new async Task<IEnumerable<Users>> ListAsync() => await _context.Users
        .Include(x => x.Role)
        .Include(x => x.Power)
        .ToListAsync();
    
    public int MaxId() => _context.Users.Max(x => x.Id);

    public async Task<Users> FindByName(string username) => 
        (await _context.Users.Where(x => x.Username == username)
            .Include(x => x.Role)
            .FirstOrDefaultAsync())!;
}