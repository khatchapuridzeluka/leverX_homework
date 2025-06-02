using leverX.DTOs.Players;
namespace leverX.Application.Interfaces.Services
{
    public interface IPlayerService : ICrudService<PlayerDto, CreatePlayerDto, UpdatePlayerDto>
    {
        Task<IEnumerable<PlayerDto>> GetByRatingAsync(int rating);
    }
}
