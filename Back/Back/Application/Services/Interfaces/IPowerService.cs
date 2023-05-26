using Back.Domain.Models;

namespace Back.Application.Services.Interfaces;

public interface IPowerService
{
    Task<IEnumerable<Power>> ListAsync();
}