using System.Data;
using Dapper;
using leverX.Application.Helpers.Constants;
using leverX.Application.Interfaces.Repositories;
using leverX.Domain.Entities;
using leverX.Domain.Exceptions;

namespace leverX.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IDbConnection _dbConnection;

        public UserRepository(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }


        public async Task AddAsync(User user)
        {
            var sql = @"
                INSERT INTO Users (Username, Email, PasswordHash, Role)
                    OUTPUT INSERTED.Id 
                VALUES (@Username, @Email, @PasswordHash, @Role)";

            var parameters = new
            {
                user.Username,
                user.Email,
                user.PasswordHash,
                user.Role
            };

            var generatedId = await _dbConnection.QuerySingleAsync<Guid>(sql, parameters);
            user.Id = generatedId;
        }

        public async Task<bool> ExistsByUsername(string username)
        {
            var sql = "SELECT COUNT(1) FROM Users WHERE Username = @Username";
            var count = await _dbConnection.ExecuteScalarAsync<int>(sql, new { Username = username });
            return count > 0;
        }

        public async Task<IEnumerable<User>> GetAll()
        {
            var sql = "SELECT * FROM Users"; 
            var users = await _dbConnection.QueryAsync<User>(sql);
            return users;
        }

        public async Task<User?> GetById(Guid id)
        {
            var sql = "SELECT * FROM Users WHERE Id = @Id";
            return await _dbConnection.QueryFirstOrDefaultAsync<User>(sql, new { Id = id });
        }

        public async Task<User?> GetByUsername(string username)
        {
            var sql = "SELECT * FROM Users WHERE Username = @Username";
            return await _dbConnection.QueryFirstOrDefaultAsync<User>(sql, new { Username = username });
        }

        public async Task<bool> UpdateAsync(User user)
        {
            var sql = @"
        UPDATE Users
        SET Username = @Username,
            Email = @Email,
            PasswordHash = @PasswordHash,
            Role = @Role
        WHERE Id = @Id";

            var parameters = new
            {
                user.Id,
                user.Username,
                user.Email,
                user.PasswordHash,
                user.Role
            };

            var affectedRows = await _dbConnection.ExecuteAsync(sql, parameters);
            return affectedRows > 0;
        }
    }
}
