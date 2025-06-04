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

        public async Task<TournamentPlayerDto> CreateAsync(CreateTournamentPlayerDto dto)
        {
            var tournamentPlayer = _mapper.Map<TournamentPlayer>(dto);
            await _tournamentPlayerRepository.AddAsync(tournamentPlayer);
            return _mapper.Map<TournamentPlayerDto>(tournamentPlayer);
        }

        public async Task<TournamentPlayerDto?> GetByIdAsync(Guid tournamentId, Guid playerId)
        {
            var tournamentPlayer = await _tournamentPlayerRepository.GetByIdAsync(tournamentId, playerId);
            return tournamentPlayer == null ? null : _mapper.Map<TournamentPlayerDto>(tournamentPlayer);
        }

        public async Task<IEnumerable<TournamentPlayerDto>> GetAllAsync()
        {
            var tournamentPlayers = await _tournamentPlayerRepository.GetAllAsync();
            return tournamentPlayers.Select(_mapper.Map<TournamentPlayerDto>).ToList();
        }

        public async Task UpdateAsync(Guid tournamentId, Guid playerId, UpdateTournamentPlayerDto dto)
        {
            var tournamentPlayer = await _tournamentPlayerRepository.GetByIdAsync(tournamentId, playerId);

            if (tournamentPlayer == null)
                throw new NotFoundException(ExceptionMessages.TournamentPlayerNotFound);

            _mapper.Map(dto, tournamentPlayer);
            await _tournamentPlayerRepository.UpdateAsync(tournamentPlayer);
        }

        public async Task DeleteAsync(Guid tournamentId, Guid playerId)
        {
            await _tournamentPlayerRepository.DeleteAsync(tournamentId, playerId);
        }
    }
}
