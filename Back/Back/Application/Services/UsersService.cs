using Back.Application.Services.Interfaces;
using Back.Domain.Models;
using Back.Domain.Repositories.Interfaces;

namespace Back.Application.Services;

public class UsersService : IUsersService
{
    private readonly IUsersRepository _usersRepository;
    
    public UsersService(IUsersRepository usersRepository)
    {
        _usersRepository = usersRepository;
    }
    
    public Task<IEnumerable<Users>> ListAsync() => _usersRepository.ListAsync();
    
    public Task<Users> FindAsync(int id) => _usersRepository.FindAsync(id);
    
    public Task<Users> Add(Users users) => _usersRepository.Add(users);
    
    public Task<Users> Update(Users users) => _usersRepository.Update(users);
    
    public void Delete(int id) => _usersRepository.Delete(id);
}