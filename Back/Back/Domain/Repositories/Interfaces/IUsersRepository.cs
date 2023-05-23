using Back.Domain.Models;

namespace Back.Domain.Repositories.Interfaces;

public interface IUsersRepository : ICommonRepository<Users>
{
    int MaxId();
    Task<Users> FindByName(string username);
}