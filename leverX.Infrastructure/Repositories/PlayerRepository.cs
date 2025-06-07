using System.Data;
using leverX.Application.Helpers.Constants;
using leverX.Domain.Exceptions;
using Dapper;
using leverX.Application.Interfaces.Repositories;
using leverX.Domain.Entities;
using System.Data.Common;
namespace leverX.Infrastructure.Repositories
{
    public class PlayerRepository : IPlayerRepository
    {
        private readonly IDbConnection _players;

        public PlayerRepository(IDbConnection players)
        {
            _players = players;
        }

        public async Task AddAsync(Player player)
        {
            var sql = @"INSERT INTO Players (Name, LastName, Sex, Nationality, FideRating, Title)
            OUTPUT INSERTED.Id
            VALUES (@Name, @LastName, @Sex, @Nationality, @FideRating, @Title)";

            var generatedId = await _players.QuerySingleAsync<Guid>(sql, player);
            player.Id = generatedId;


            int affectedRows = await _players.ExecuteAsync(sql, player);

            if (affectedRows == 0)
            {
                throw new InsertFailedException(ExceptionMessages.InsertFailed);
            }

        }

        public async Task<Player?> GetByIdAsync(Guid id)
        {
            var sql = "SELECT Id, Name, LastName, Sex, Nationality, FideRating, Title FROM Players WHERE Id = @Id";
            var player = await _players.QuerySingleAsync<Player>(sql, new { Id = id });

            var gamesAsWhiteSql = "SELECT Id FROM Games WHERE WhitePlayerId = @PlayerId";
            var gamesAsWhite = await _players.QueryAsync<Game>(gamesAsWhiteSql, new { PlayerId = id });
            player.GamesAsWhite = gamesAsWhite.ToList();

            var gamesAsBlackSql = "SELECT Id FROM Games WHERE BlackPlayerId = @PlayerId";
            var gamesAsBlack = await _players.QueryAsync<Game>(gamesAsBlackSql, new { PlayerId = id });
            player.GamesAsBlack = gamesAsBlack.ToList();

            return player;
        }

        public async Task<IEnumerable<Player>> GetAllAsync()
        {
            var sql = "SELECT Id, Name, LastName, Sex, Nationality, FideRating, Title FROM Players";
            var players = await _players.QueryAsync<Player>(sql);

            foreach (var player in players)
            {
                var gamesAsWhiteSql = "SELECT Id FROM Games WHERE WhitePlayerId = @PlayerId";
                var gamesAsWhite = await _players.QueryAsync<Game>(gamesAsWhiteSql, new { PlayerId = player.Id });
                player.GamesAsWhite = gamesAsWhite.ToList();

                var gamesAsBlackSql = "SELECT Id FROM Games WHERE BlackPlayerId = @PlayerId";
                var gamesAsBlack = await _players.QueryAsync<Game>(gamesAsBlackSql, new { PlayerId = player.Id });
                player.GamesAsBlack = gamesAsBlack.ToList();
            }

            return players.ToList();

        }

        public async Task UpdateAsync(Player player)
        {
            var sql = @"UPDATE Players
                SET Name = @Name, LastName = @LastName, Sex = @Sex,
                    Nationality = @Nationality, FideRating = @FideRating, Title = @Title
                WHERE Id = @Id";

            int affectedRows = await _players.ExecuteAsync(sql, player);

            if (affectedRows == 0)
            {
                throw new NotFoundException(ExceptionMessages.PlayerNotFound);
            }
        }
        public async Task DeleteAsync(Guid id)
        {
            var sql = @"DELETE FROM Players WHERE Id = @Id";
            int affectedRows = await _players.ExecuteAsync(sql, new { Id = id });

            if (affectedRows == 0)
            {
                throw new NotFoundException(ExceptionMessages.PlayerNotFound);
            }
        }

        public async Task<IEnumerable<Player>> GetByRatingAsync(int rating)
        {
            var sql = @"SELECT * FROM Players WHERE FideRating >= @Rating";

            var result = await _players.QueryAsync<Player>(sql, new { Rating = rating });
            return result.ToList();

        }
    }
}
    