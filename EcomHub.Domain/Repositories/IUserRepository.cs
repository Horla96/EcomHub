using EcomHub.Domain.Entities;

namespace EcomHub.Domain.Repositories;

public interface IUserRepository
{
    Task<User> GetUserByEmailAsync(string email);
    Task<User> GetUserByIdAsync(Guid Id);
    Task<IEnumerable<User>> GetAllUserAsync();
    Task UpdateUserAsync(User user);
    Task DeleteUserAsync(User user);
}
