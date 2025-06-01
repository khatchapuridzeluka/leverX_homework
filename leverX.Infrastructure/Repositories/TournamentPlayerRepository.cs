using System.Data;
using Dapper;
using leverX.Application.Interfaces.Repositories;
using leverX.Domain.Entities;

namespace leverX.Infrastructure.Repositories
{
    public class TournamentPlayerRepository : ITournamentPlayerRepository
    {
        private readonly IDbConnection _tournamentPlayers;

        public TournamentPlayerRepository(IDbConnection tournamentPlayers)
        {
            _tournamentPlayers = tournamentPlayers;
        }

        public Task AddAsync(TournamentPlayer entity)
        {
            var sql = @"
                INSERT INTO TournamentPlayers (TournamentId, PlayerId, FinalRank, Score)
                VALUES (@TournamentId, @PlayerId, @FinalRank, @Score)";
            var parameters = new
            {
                entity.TournamentId,
                entity.PlayerId,
                entity.FinalRank,
                entity.Score
            };
            return _tournamentPlayers.ExecuteAsync(sql, parameters);
        }

        public async Task<TournamentPlayer?> GetByIdAsync(Guid tournamentId, Guid playerId)
        {
            var sql = @"
                SELECT * FROM TournamentPlayers 
                WHERE TournamentId = @TournamentId AND PlayerId = @PlayerId";

            var tp = await _tournamentPlayers.QueryFirstOrDefaultAsync<TournamentPlayer>(
                sql,
                new { TournamentId = tournamentId, PlayerId = playerId });

            return tp;
        }

        public async Task<List<TournamentPlayer>> GetAllAsync()
        {
            var sql = "SELECT * FROM TournamentPlayers";
            var tournamentPlayers = await _tournamentPlayers.QueryAsync<TournamentPlayer>(sql);
            return tournamentPlayers.AsList();
        }

        public Task UpdateAsync(TournamentPlayer entity)
        {
            var sql = @"
                UPDATE TournamentPlayers
                SET FinalRank = @FinalRank, Score = @Score
                WHERE TournamentId = @TournamentId AND PlayerId = @PlayerId";
            var parameters = new
            {
                entity.FinalRank,
                entity.Score,
                entity.TournamentId,
                entity.PlayerId
            };
            return _tournamentPlayers.ExecuteAsync(sql, parameters);
        }

        public Task DeleteAsync(Guid tournamentId, Guid playerId)
        {
            var sql = @"
                DELETE FROM TournamentPlayers 
                WHERE TournamentId = @TournamentId AND PlayerId = @PlayerId";
            return _tournamentPlayers.ExecuteAsync(sql, new { TournamentId = tournamentId, PlayerId = playerId });
        }
    }
}
