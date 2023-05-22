using Back.Api.Error;
using Back.Domain.Models;
using Back.Domain.Repositories.Interfaces;
using Back.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Back.Domain.Repositories;

public class UsersRepository : CommonRepository<Users>, IUsersRepository
{
    private readonly AppDbContext _context;

    public UsersRepository(AppDbContext context) : base(context)
    {
        _context = context;
    }
}