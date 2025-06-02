using leverX.Domain.Entities;
using leverX.DTOs.Players;
namespace leverX.Application.Interfaces.Repositories
{
    public interface IPlayerRepository : ICrudRepository<Player>
    {
        Task<IEnumerable<Player>> GetByRatingAsync(int rating);
    }
}
