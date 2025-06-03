using AutoMapper;
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
        private readonly IMapper _mapper;

        public GameService(IGameRepository gameRepository, IPlayerRepository playerRepository, IOpeningRepository openingRepository, ITournamentRepository tournamentRepository, IMapper mapper
            )
        {
            _gameRepository = gameRepository;
            _playerRepository = playerRepository;
            _openingRepository = openingRepository;
            _tournamentRepository = tournamentRepository;
            _mapper = mapper;
        }

        // Creates a new game asynchronously because it involves database calls to fetch related entities and save the new game.
        public async Task<GameDto> CreateAsync(CreateGameDto dto)
        {
            var whitePlayer = await GetPlayerOrThrowAsync(dto.WhitePlayerId);
            var blackPlayer = await GetPlayerOrThrowAsync(dto.BlackPlayerId);
            var opening = await GetOpeningOrThrowAsync(dto.OpeningId);
            var tournament = await GetTournamentIfExistsAsync(dto.TournamentId);

            var game = _mapper.Map<Game>(dto);

            game.Id = Guid.NewGuid();
            game.WhitePlayer = whitePlayer;
            game.BlackPlayer = blackPlayer;
            game.Opening = opening;
            game.Tournament = tournament;


            await _gameRepository.AddAsync(game);
            return DtoMapper.MapToDto(game);
        }

        // Involves Fetching from the database.
        public async Task<GameDto?> GetByIdAsync(Guid id)
        {
            var game = await _gameRepository.GetByIdAsync(id);
            return game == null ? null : _mapper.Map<GameDto>(game);
        }

        // Fetches all the games frm the database - needs async/await to avoid blocking the thread.
        public async Task<IEnumerable<GameDto>> GetAllAsync()
        {
            var games = await _gameRepository.GetAllAsync();
            return games.Select(_mapper.Map<GameDto>).ToList();
        }

        //Updates an existing game - involves fetching the game that needs async/await
        public async Task UpdateAsync(Guid id, UpdateGameDto dto)
        {
            var game = await _gameRepository.GetByIdAsync(id);
            if (game == null)
                throw new NotFoundException(ExceptionMessages.GameNotFound);

            var whitePlayer = await GetPlayerOrThrowAsync(dto.WhitePlayerId);
            var blackPlayer = await GetPlayerOrThrowAsync(dto.BlackPlayerId);
            var opening = await GetOpeningOrThrowAsync(dto.OpeningId);
            var tournament = await GetTournamentIfExistsAsync(dto.TournamentId);

            _mapper.Map(dto, game);
            game.WhitePlayer = whitePlayer;
            game.BlackPlayer = blackPlayer;
            game.Opening = opening;
            game.Tournament = tournament;

            await _gameRepository.UpdateAsync(game);
        }

        // Deletes the game by id - needs async/await to avoid blocking the thread.
        public async Task DeleteAsync(Guid id)
        {
            await _gameRepository.DeleteAsync(id);
        }

        //get player by id - needs async/await to avoid blocking the thread.
        private async Task<Player> GetPlayerOrThrowAsync(Guid playerId)
        {
            var player = await _playerRepository.GetByIdAsync(playerId);
            if (player == null)
                throw new NotFoundException(ExceptionMessages.PlayerNotFound);
            return player;
        }

        //get opening by id - needs async/await to avoid blocking the thread.
        private async Task<Opening> GetOpeningOrThrowAsync(Guid openingId)
        {
            var opening = await _openingRepository.GetByIdAsync(openingId);
            if (opening == null)
                throw new NotFoundException(ExceptionMessages.OpeningNotFound);
            return opening;
        }

        //get tournament by id - needs async/await to avoid blocking the thread.
        private async Task<Tournament?> GetTournamentIfExistsAsync(Guid? tournamentId)
        {
            if (tournamentId == null)
                return null;

            return await _tournamentRepository.GetByIdAsync(tournamentId.Value);
        }
    }
}
