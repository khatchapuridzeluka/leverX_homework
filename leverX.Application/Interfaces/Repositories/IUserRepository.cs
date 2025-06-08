using leverX.Domain.Entities;

namespace leverX.Application.Interfaces.Repositories
{
    public interface IUserRepository
    {
        Task<User> GetById(Guid Id);

        Task<IEnumerable<User>> GetAll();
        Task<User> GetByUsername(string username);
        Task AddAsync(User user);
        Task<bool> ExistsByUsername(string username);
    }
}
