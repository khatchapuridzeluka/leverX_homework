using leverX.Dtos.DTOs.Users;


namespace leverX.Application.Interfaces.Services
{
    public interface IUserService
    {
        Task<bool> RegisterAsync(RegisterUserDto dto);
        Task<LoginResponseDto?> LoginAsync(LoginUserDto dto);
        Task<UserDto?> GetByIdAsync(Guid id);

        Task<IEnumerable<UserDto>> GetAllAsync();
        Task<bool> UserExistsAsync(string username);

        Task<bool> ChangeRoleAsync(Guid userId, string newRole);
    }
}