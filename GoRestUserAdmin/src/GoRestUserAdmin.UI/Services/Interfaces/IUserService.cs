using GoRestUserAdmin.UI.Models;

namespace GoRestUserAdmin.UI.Services.Interfaces;

public interface IUserService
{
    Task<List<UserDto>> GetAllAsync();
    Task<UserDto?> GetByIdAsync(int id);
    Task<bool> CreateAsync(UserDto user);
    Task<bool> UpdateAsync(UserDto user);
    Task<bool> DeleteAsync(int id);
}
