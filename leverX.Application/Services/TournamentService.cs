using AutoMapper;
using leverX.Application.Helpers;
using leverX.Application.Helpers.Constants;
using leverX.Application.Interfaces.Repositories;
using leverX.Application.Interfaces.Services;
using leverX.Domain.Entities;
using leverX.Domain.Exceptions;
using leverX.DTOs.Tournaments;

namespace leverX.Application.Services
{
    public class TournamentService : ITournamentService
    {
        private readonly ITournamentRepository _tournamentRepository;
        private readonly IPlayerRepository _playerRepository;
        private readonly IMapper _mapper;
        public TournamentService(ITournamentRepository tournamentRepository, IPlayerRepository playerRepository,  IMapper mapper)
        {
            _tournamentRepository = tournamentRepository;
            _playerRepository = playerRepository;
            _mapper = mapper;
        }
        public async Task<TournamentDto> CreateAsync(CreateTournamentDto dto)
        {
            var tournament = _mapper.Map<Tournament>(dto);
            tournament.Id = Guid.NewGuid(); 

            await _tournamentRepository.AddAsync(tournament);
            return _mapper.Map<TournamentDto>(tournament);
        }
        public async Task<TournamentDto?> GetByIdAsync(Guid id)
        {
            var tournament = await _tournamentRepository.GetByIdAsync(id);
            return tournament == null ? null : _mapper.Map<TournamentDto>(tournament);
        }
        public async Task<IEnumerable<TournamentDto>> GetAllAsync()
        {
            var tournaments = await _tournamentRepository.GetAllAsync();
            return tournaments.Select(_mapper.Map<TournamentDto>).ToList();
        }
        public async Task UpdateAsync(Guid id, UpdateTournamentDto dto)
        {
            var tournament = await _tournamentRepository.GetByIdAsync(id);
            if (tournament == null)
                throw new NotFoundException(ExceptionMessages.TournamentNotFound);

            _mapper.Map(dto, tournament);
            await _tournamentRepository.UpdateAsync(tournament);
        }
        public async Task DeleteAsync(Guid id)
        {
            await _tournamentRepository.DeleteAsync(id);
        }
    }
}
