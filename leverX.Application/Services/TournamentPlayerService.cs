using leverX.Application.Helpers;
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
                Score = dto.Score,
            };
            await _tournamentPlayerRepository.AddAsync(tournamentPlayer);

            return DtoMapper.MapToDto(tournamentPlayer);
        }

        public async Task<TournamentPlayerDto?> GetByIdAsync(Guid tournamentId, Guid playerId)
        {
            var tournamentPlayer = await _tournamentPlayerRepository.GetByIdAsync(tournamentId, playerId);
            return tournamentPlayer == null ? null : DtoMapper.MapToDto(tournamentPlayer);
        }

        public async Task<List<TournamentPlayerDto>> GetAllAsync()
        {
            var tournamentPlayers = await _tournamentPlayerRepository.GetAllAsync();
            return tournamentPlayers.Select(DtoMapper.MapToDto).ToList();
        }

        public async Task UpdateAsync(Guid tournamentId, Guid playerId, UpdateTournamentPlayerDto dto)
        {
            var tournamentPlayer = await _tournamentPlayerRepository.GetByIdAsync(tournamentId, playerId);

            if(tournamentPlayer == null)
            {
                //TODO: CREATE A CUSTOM EXCEPTION
                throw new Exception("TournamentPlayer not found");
            }

            tournamentPlayer.FinalRank = dto.FinalRank;
            tournamentPlayer.Score = dto.Score;

            await _tournamentPlayerRepository.UpdateAsync(tournamentPlayer);
        }


        public async Task DeleteAsync(Guid tournamentId, Guid playerId)
        {
           await _tournamentPlayerRepository.DeleteAsync(tournamentId,playerId);
        }
    }
}
