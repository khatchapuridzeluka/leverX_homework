using leverX.Application.Interfaces.Repositories;
using leverX.Application.Interfaces.Services;
using leverX.Domain.Entities;
using leverX.DTOs.Games;

namespace leverX.Application.Services
{
    public class GameService : IGameService
    {
        private readonly IGameRepository _gameRepository;
        private readonly IPlayerRepository _playerRepository;
        private readonly IOpeningRepository _openingRepository;

        public GameService(IGameRepository gameRepository, IPlayerRepository playerRepository, IOpeningRepository openingRepository)
        {
            _gameRepository = gameRepository;
            _playerRepository = playerRepository;
            _openingRepository = openingRepository;
        }

        public async Task<GameDto> CreateAsync(CreateGameDto dto)
        {
            var whitePlayer = await _playerRepository.GetByIdAsync(dto.WhitePlayerId);
            if (whitePlayer == null)
                throw new Exception("White player not found");

            var blackPlayer = await _playerRepository.GetByIdAsync(dto.BlackPlayerId);
            if (blackPlayer == null)
                throw new Exception("Black player not found");

            var opening = await _openingRepository.GetByIdAsync(dto.OpeningId);
            if (opening == null)
                throw new Exception("Opening not found");

            var game = new Game
            {
                Id = Guid.NewGuid(),
                WhitePlayer = whitePlayer,
                BlackPlayer = blackPlayer,
                Result = dto.Result,
                Moves = dto.Moves,
                PlayedOn = dto.PlayedOn,
                Opening = opening
            };

            await _gameRepository.AddAsync(game);
            return MapToDto(game);
        }

        public async Task<GameDto?> GetByIdAsync(Guid id)
        {
            var game = await _gameRepository.GetByIdAsync(id);
            return game == null ? null : MapToDto(game);
        }

        public async Task<List<GameDto>> GetAllAsync()
        {
            var games = await _gameRepository.GetAllAsync();
            return games.Select(MapToDto).ToList();
        }

        public async Task UpdateAsync(Guid id, UpdateGameDto dto)
        {
            var game = await _gameRepository.GetByIdAsync(id);
            if (game == null)
                throw new Exception("Game not found");

            var whitePlayer = await _playerRepository.GetByIdAsync(dto.WhitePlayerId);
            if (whitePlayer == null)
                throw new Exception("White player not found");

            var blackPlayer = await _playerRepository.GetByIdAsync(dto.BlackPlayerId);
            if (blackPlayer == null)
                throw new Exception("Black player not found");
            var opening = await _openingRepository.GetByIdAsync(dto.OpeningId);

            if (opening == null)
                throw new Exception("Opening not found");

            game.WhitePlayer = whitePlayer;
            game.BlackPlayer = blackPlayer;
            game.Result = dto.Result;
            game.Moves = dto.Moves;
            game.PlayedOn = dto.PlayedOn;
            game.Opening = opening;
            await _gameRepository.UpdateAsync(game);
        }

        public async Task DeleteAsync(Guid id)
        {
            await _gameRepository.DeleteAsync(id);
        }

        private static GameDto MapToDto(Game game) => new()
        {
            Id = game.Id,
            WhitePlayerId = game.WhitePlayer.Id,
            BlackPlayerId = game.BlackPlayer.Id,
            Result = game.Result,
            Moves = game.Moves,
            PlayedOn = game.PlayedOn,
            OpeningId = game.Opening.Id
        };
    }
}
