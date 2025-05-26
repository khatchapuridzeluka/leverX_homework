using leverX.Application.Interfaces.Repositories;
using leverX.Application.Interfaces.Services;
using leverX.Domain.Entities;
using leverX.DTOs.TournamentPlayers;

namespace leverX.Application.Services
{
    public class TournamentPlayerService : ITournamentPlayerService
    {
        private readonly ITournamentPlayerRepository _tournamentPlayerRepository;
        public TournamentPlayerService(ITournamentPlayerRepository tournamentPlayerRepository)
        {
            _tournamentPlayerRepository = tournamentPlayerRepository;
        }
        
        public async Task<TournamentPlayerDto> CreateAsync(CreateTournamentPlayerDto dto)
        {
            var tournamentPlayer = new TournamentPlayer
            {
                TournamentId = dto.TournamentId,
                PlayerId = dto.PlayerId,
                FinalRank = dto.FinalRank,
                Score = dto.Score
            };
            await _tournamentPlayerRepository.AddAsync(tournamentPlayer);
            return MapToDto(tournamentPlayer);
        }

        public async Task<TournamentPlayerDto?> GetByIdAsync(Guid id)
        {
            var tournamentPlayer = await _tournamentPlayerRepository.GetByIdAsync(id);
            return tournamentPlayer == null ? null : MapToDto(tournamentPlayer);
        }

        public async Task<List<TournamentPlayerDto>> GetAllAsync()
        {
            var tournamentPlayers = await _tournamentPlayerRepository.GetAllAsync();
            return tournamentPlayers.Select(MapToDto).ToList();
        }

        public async Task<TournamentPlayerDto> UpdateAsync(Guid id, UpdateTournamentPlayerDto updateDto)
        {
            var tournamentPlayer = await _tournamentPlayerRepository.GetByIdAsync(id);
            if (tournamentPlayer == null)
                throw new Exception("Tournament player not found");

            tournamentPlayer.TournamentId = updateDto.TournamentId;
            tournamentPlayer.PlayerId = updateDto.PlayerId;
            tournamentPlayer.FinalRank = updateDto.FinalRank;
            tournamentPlayer.Score = updateDto.Score;

            await _tournamentPlayerRepository.UpdateAsync(tournamentPlayer);

            return MapToDto(tournamentPlayer);

        }

        public async Task DeleteAsync(Guid id)
        {
            await _tournamentPlayerRepository.DeleteAsync(id);
        }

        private static TournamentPlayerDto MapToDto(TournamentPlayer tournamentPlayer) => new()
        {
            TournamentId = tournamentPlayer.TournamentId,
            PlayerId = tournamentPlayer.PlayerId,
            FinalRank = tournamentPlayer.FinalRank,
            Score = tournamentPlayer.Score
        };
    }
}
