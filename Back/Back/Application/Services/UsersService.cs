using AutoMapper;
using Back.Api.Error;
using Back.Application.Services.Interfaces;
using Back.Domain.Models;
using Back.Domain.Repositories.Interfaces;

namespace Back.Application.Services;

public class UsersService : IUsersService
{
    private readonly IUsersRepository _usersRepository;
    private readonly IRoleRepository _roleRepository;
    private readonly IMapper _mapper;
    
    public UsersService(IUsersRepository usersRepository, IRoleRepository roleRepository, IMapper mapper)
    {
        _usersRepository = usersRepository;
        _roleRepository = roleRepository;
        _mapper = mapper;
    }
    
    public Task<IEnumerable<Users>> ListAsync() => _usersRepository.ListAsync();
    
    public Task<Users> FindAsync(int id) => _usersRepository.FindAsync(id);

    public async Task<Users> Add(Users users)
    {
        users.Id = _usersRepository.MaxId() + 1;
        var salt = BCrypt.Net.BCrypt.GenerateSalt();
        users.Password = BCrypt.Net.BCrypt.HashPassword(users.Password, salt);
        var role = await _roleRepository.FindAsync(users.RoleId);
        if (role is null) throw new NotFoundException("Role incorrect !");
        users.Role = role;
        if (users.PowerId is 0) users.PowerId = null;
        var user = _mapper.Map<Users>(users);
        _usersRepository.Add(user);
        await _usersRepository.SaveChangesAsync();
        return user;
    }
    
    public async Task<Users> Update(Users users)
    {
        var user = await _usersRepository.FindAsync(users.Id);
        if (user is null) throw new NotFoundException("Utilisateur introuvable !");
        // user = _mapper.Map(users, user);
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

    public async Task<Users> FindByName(string username)
    {
        var result = await _usersRepository.FindByName(username);
        return result;
    }
}