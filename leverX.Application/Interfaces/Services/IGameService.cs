using leverX.DTOs.Games;

namespace leverX.Application.Interfaces.Services
{
    public interface IGameService : ICrudService<GameDto,CreateGameDto, UpdateGameDto>
    {
    }
}
