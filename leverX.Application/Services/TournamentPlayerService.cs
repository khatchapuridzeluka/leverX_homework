using AutoMapper;
using leverX.Application.Helpers.Constants;
using leverX.Application.Interfaces.Repositories;
using leverX.Application.Interfaces.Services;
using leverX.Domain.Entities;
using leverX.Domain.Exceptions;
using leverX.DTOs.TournamentPlayers;

namespace leverX.Application.Services
{
    public class TournamentPlayerService : ITournamentPlayerService
    {
        private readonly ITournamentPlayerRepository _tournamentPlayerRepository;
        private readonly IMapper _mapper;

        public TournamentPlayerService(
            ITournamentPlayerRepository tournamentPlayerRepository,
            IMapper mapper)
        {
            _tournamentPlayerRepository = tournamentPlayerRepository;
            _mapper = mapper;
        }

        //creates new tournamentplayer asynchronously - needs async/await to avoid blocking the thread.
        public async Task<TournamentPlayerDto> CreateAsync(CreateTournamentPlayerDto dto)
        {
            var tournamentPlayer = _mapper.Map<TournamentPlayer>(dto);
            await _tournamentPlayerRepository.AddAsync(tournamentPlayer);
            return _mapper.Map<TournamentPlayerDto>(tournamentPlayer);
        }

        // Fetches a tournamentPlayer by ids asynchronously - needs async/await to avoid blocking the thread.
        public async Task<TournamentPlayerDto?> GetByIdAsync(Guid tournamentId, Guid playerId)
        {
            var tournamentPlayer = await _tournamentPlayerRepository.GetByIdAsync(tournamentId, playerId);
            return tournamentPlayer == null ? null : _mapper.Map<TournamentPlayerDto>(tournamentPlayer);
        }

        // Fetches all tournamentPlayers asynchronously - needs async/await to avoid blocking the thread.
        public async Task<IEnumerable<TournamentPlayerDto>> GetAllAsync()
        {
            var tournamentPlayers = await _tournamentPlayerRepository.GetAllAsync();
            return tournamentPlayers.Select(_mapper.Map<TournamentPlayerDto>).ToList();
        }

        // Updates an existing tournamentPlayer by ids asynchronously - needs async/await to avoid blocking the thread.
        public async Task UpdateAsync(Guid tournamentId, Guid playerId, UpdateTournamentPlayerDto dto)
        {
            var tournamentPlayer = await _tournamentPlayerRepository.GetByIdAsync(tournamentId, playerId);

            if (tournamentPlayer == null)
                throw new NotFoundException(ExceptionMessages.TournamentPlayerNotFound);

            _mapper.Map(dto, tournamentPlayer);
            await _tournamentPlayerRepository.UpdateAsync(tournamentPlayer);
        }

        // Deletes a tournamentPlayer by ids asynchronously - needs async/await to avoid blocking the thread.
        public async Task DeleteAsync(Guid tournamentId, Guid playerId)
        {
            await _tournamentPlayerRepository.DeleteAsync(tournamentId, playerId);
        }
    }
}
