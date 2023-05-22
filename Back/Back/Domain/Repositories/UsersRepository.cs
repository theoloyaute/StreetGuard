using Back.Api.Error;
using Back.Domain.Models;
using Back.Domain.Repositories.Interfaces;
using Back.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Back.Domain.Repositories;

public class UsersRepository : IUsersRepository
{
    private readonly AppDbContext _context;

    public UsersRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Users>> ListAsync() => await _context.Users.OrderBy(x => x.Id).ToListAsync();

    public async Task<Users> FindAsync(int id)
    {
        var user = await _context.Users.FindAsync(id);
        if (user is null) throw new NotFoundException("Utilisateur introuvable !");
        return user;
    }

    public async Task<Users> Add(Users users)
    {
        users.Id = _context.Users.Max(x => x.Id) + 1;
        var salt = BCrypt.Net.BCrypt.GenerateSalt();
        users.Password = BCrypt.Net.BCrypt.HashPassword(users.Password, salt);
        var result = _context.Users.Add(users);
        await _context.SaveChangesAsync();
        return result.Entity;
    }

    public async Task<Users> Update(Users users)
    {
        var user = await _context.Users.FindAsync(users.Id);
        if (user is null) throw new NotFoundException("Utilisateur introuvable !");
        user.Username = users.Username;
        user.Firstname = users.Firstname;
        user.Lastname = users.Lastname;
        user.Email = users.Email;
        user.Phone = users.Phone;
        var salt = BCrypt.Net.BCrypt.GenerateSalt();
        user.Password = BCrypt.Net.BCrypt.HashPassword(users.Password, salt);
        user.Longitude = users.Longitude;
        user.Latitude = users.Latitude;
        user.RoleId = users.RoleId;
        user.PowerId = users.PowerId;
        _context.Users.Update(user);
        await _context.SaveChangesAsync();
        return user;
    }

    public async void Delete(int id)
    {
        var user = await _context.Users.FindAsync(id);
        if (user is null) throw new NotFoundException("Utilisateur introuvable !");
        _context.Users.Remove(user);
        await _context.SaveChangesAsync();
    }
}