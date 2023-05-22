using Back.Domain.Models;

namespace Back.Application.Services.Interfaces;

public interface IUsersService
{
    Task<IEnumerable<Users>> ListAsync();
    Task<Users> FindAsync(int id);
    Task<Users> Add(Users users);
    Task<Users> Update(Users users);
    void Delete(int id);
}