using AutoMapper;
using Back.Api.Error;
using Back.Application.Services.Interfaces;
using Back.Domain.Models;
using Back.Domain.Repositories.Interfaces;

namespace Back.Application.Services;

public class UsersService : IUsersService
{
    private readonly IUsersRepository _usersRepository;
    private readonly IMapper _mapper;
    
    public UsersService(IUsersRepository usersRepository)
    {
        _usersRepository = usersRepository;
    }
    
    public Task<IEnumerable<Users>> ListAsync() => _usersRepository.ListAsync();
    
    public Task<Users> FindAsync(int id) => _usersRepository.FindAsync(id);
    
    public async Task<Users> Add(Users users)
    {
        var user = _mapper.Map<Users>(users);
        var result = _usersRepository.Add(user);
        await _usersRepository.SaveChangesAsync();
        return result;
    }
    
    public async Task<Users> Update(Users users)
    {
        var user = await _usersRepository.FindAsync(users.Id);
        if (user is null) throw new NotFoundException("Utilisateur introuvable !");
        user = _mapper.Map(users, user);
        await _usersRepository.SaveChangesAsync();
        return user;
    }
    
    public void Delete(int id)
    {
        var user = _usersRepository.FindAsync(id).Result;
        if (user is null) throw new NotFoundException("Utilisateur introuvable !");
        _usersRepository.Remove(user);
        _usersRepository.SaveChanges();
    }
}