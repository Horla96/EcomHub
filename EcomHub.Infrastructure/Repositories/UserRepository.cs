using EcomHub.Domain.Entities;
using EcomHub.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace EcomHub.Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
    private readonly AppDbContext _context;
    public UserRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<User>> GetAllUserAsync()
        => await _context.Users
            .Where(u => !u.IsDeleted)
            .ToListAsync();

    public async Task<User> GetUserByEmailAsync(string email)
        => await _context.Users
            .FirstOrDefaultAsync(u => u.Email == email && !u.IsDeleted);

    public async Task<User> GetUserByIdAsync(Guid Id)
        => await _context.Users
            .FirstOrDefaultAsync(u => u.Id == Id && !u.IsDeleted);

    public async Task DeleteUserAsync(User user)
    {
        user.IsDeleted = true;
        user.IsActive = false;
        user.UpdatedAt = DateTime.UtcNow;

        _context.Users.Update(user);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateUserAsync(User user)
    {
        _context.Users.Update(user);
        await _context.SaveChangesAsync();
    }

    public async Task<Role> GetRoleByName(string name)
    => await _context.Roles
        .FirstOrDefaultAsync(x => x.Name.Equals(name));
}
