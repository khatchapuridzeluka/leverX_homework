using leverX.Domain.Entities;
using leverX.DTOs.Players;
namespace leverX.Application.Interfaces.Repositories
{
    public interface IPlayerRepository : ICrudRepository<Player>
    {
        Task<List<Player>> GetByRatingAsync(int rating);
    }
}
