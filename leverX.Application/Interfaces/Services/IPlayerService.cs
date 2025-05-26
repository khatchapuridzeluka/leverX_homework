using leverX.DTOs.Players;
namespace leverX.Application.Interfaces.Services
{
    public interface IPlayerService : ICrudService<PlayerDto, CreatePlayerDto, UpdatePlayerDto>
    {
        Task<List<PlayerDto>> GetByRatingAsync(int rating);
    }
}
