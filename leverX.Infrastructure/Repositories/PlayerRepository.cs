using System.Data;
using Dapper;
using leverX.Application.Interfaces.Repositories;
using leverX.Domain.Entities;
namespace leverX.Infrastructure.Repositories
{
    public class PlayerRepository : IPlayerRepository
    {
        private readonly IDbConnection _players;

        public PlayerRepository(IDbConnection players)
        {
            _players = players;
        }

        public Task AddAsync(Player player)
        {
            var sql = @"INSERT INTO Players (Id, Name, LastName, Sex, Nationality, FideRating, Title)
                VALUES (@Id, @Name, @LastName, @Sex, @Nationality, @FideRating, @Title)";
            return _players.ExecuteAsync(sql, player);
        }

        public async Task<Player?> GetByIdAsync(Guid id)
        {
            var sql = @"SELECT * FROM Players WHERE Id = @Id";
            return await _players.QueryFirstOrDefaultAsync<Player>(sql, new { Id = id });
        }
        public async Task<List<Player>> GetAllAsync()
        {
            var sql = @"SELECT * FROM Players";
            var players = await _players.QueryAsync<Player>(sql);
            return players.ToList();
        }

        public Task UpdateAsync(Player player)
        {
            var sql = @"UPDATE Players
                SET Name = @Name, LastName = @LastName, Sex = @Sex,
                    Nationality = @Nationality, FideRating = @FideRating, Title = @Title
                WHERE Id = @Id";

            return _players.ExecuteAsync(sql, player);
        }

        public Task DeleteAsync(Guid id)
        {
            var sql = @"DELETE FROM Players WHERE Id = @Id";
            return _players.ExecuteAsync(sql, new { Id = id });
        }

        public async Task<List<Player>> GetByRatingAsync(int rating)
        {
            var sql = @"SELECT * FROM Players WHERE FideRating >= @Rating";

            var result = await _players.QueryAsync<Player>(sql, new { Rating = rating });
            return result.ToList();

        }
    }
}
    