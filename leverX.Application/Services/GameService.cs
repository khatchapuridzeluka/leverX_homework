using leverX.Application.Helpers;
using leverX.Application.Helpers.Constants;
using leverX.Application.Interfaces.Repositories;
using leverX.Application.Interfaces.Services;
using leverX.Domain.Entities;
using leverX.Domain.Exceptions;
using leverX.DTOs.Games;

namespace leverX.Application.Services
{
    public class GameService : IGameService
    {
        private readonly IGameRepository _gameRepository;
        private readonly IPlayerRepository _playerRepository;
        private readonly IOpeningRepository _openingRepository;
        private readonly ITournamentRepository _tournamentRepository;

        public GameService(IGameRepository gameRepository, IPlayerRepository playerRepository, IOpeningRepository openingRepository, ITournamentRepository tournamentRepository)
        {
            _gameRepository = gameRepository;
            _playerRepository = playerRepository;
            _openingRepository = openingRepository;
            _tournamentRepository = tournamentRepository;
        }

        public async Task<GameDto> CreateAsync(CreateGameDto dto)
        {
            var whitePlayer = await GetPlayerOrThrowAsync(dto.WhitePlayerId);
            var blackPlayer = await GetPlayerOrThrowAsync(dto.BlackPlayerId);
            var opening = await GetOpeningOrThrowAsync(dto.OpeningId);
            var tournament = await GetTournamentIfExistsAsync(dto.TournamentId);

            var game = new Game
            {
                Id = Guid.NewGuid(),
                WhitePlayer = whitePlayer,
                BlackPlayer = blackPlayer,
                Result = dto.Result,
                Moves = dto.Moves,
                PlayedOn = dto.PlayedOn,
                Opening = opening,
                Tournament = tournament
            };

            await _gameRepository.AddAsync(game);
            return DtoMapper.MapToDto(game);
        }

        public async Task<GameDto?> GetByIdAsync(Guid id)
        {
            var game = await _gameRepository.GetByIdAsync(id);
            return game == null ? null : DtoMapper.MapToDto(game);
        }

        public async Task<IEnumerable<GameDto>> GetAllAsync()
        {
            var games = await _gameRepository.GetAllAsync();
            return games.Select(DtoMapper.MapToDto).ToList();
        }

        public async Task UpdateAsync(Guid id, UpdateGameDto dto)
        {
            var game = await _gameRepository.GetByIdAsync(id);
            if (game == null)
                throw new Exception("Game not found");

            var whitePlayer = await GetPlayerOrThrowAsync(dto.WhitePlayerId);
            var blackPlayer = await GetPlayerOrThrowAsync(dto.BlackPlayerId);
            var opening = await GetOpeningOrThrowAsync(dto.OpeningId);
            var tournament = await GetTournamentIfExistsAsync(dto.TournamentId);

            game.WhitePlayer = whitePlayer;
            game.BlackPlayer = blackPlayer;
            game.Result = dto.Result;
            game.Moves = dto.Moves;
            game.PlayedOn = dto.PlayedOn;
            game.Opening = opening;
            game.Tournament = tournament;
            await _gameRepository.UpdateAsync(game);
        }

        public async Task DeleteAsync(Guid id)
        {
            await _gameRepository.DeleteAsync(id);
        }
        private async Task<Player> GetPlayerOrThrowAsync(Guid playerId)
        {
            var player = await _playerRepository.GetByIdAsync(playerId);
            if (player == null)
                throw new NotFoundException(ExceptionMessages.PlayerNotFound);
            return player;
        }

        private async Task<Opening> GetOpeningOrThrowAsync(Guid openingId)
        {
            var opening = await _openingRepository.GetByIdAsync(openingId);
            if (opening == null)
                throw new NotFoundException(ExceptionMessages.OpeningNotFound);
            return opening;
        }

        private async Task<Tournament?> GetTournamentIfExistsAsync(Guid? tournamentId)
        {
            if (tournamentId == null)
                return null;

            return await _tournamentRepository.GetByIdAsync(tournamentId.Value);
        }
    }
}
