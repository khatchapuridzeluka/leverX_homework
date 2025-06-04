using Dapper;
using leverX.Application.Helpers.Constants;
using leverX.Domain.Exceptions;
using leverX.Application.Interfaces.Repositories;
using leverX.Domain.Entities;
using System.Data;

public class TournamentRepository : ITournamentRepository
{
    private readonly IDbConnection _dbConnection;

    public TournamentRepository(IDbConnection dbConnection)
    {
        _dbConnection = dbConnection;
    }

    public async Task AddAsync(Tournament tournament)
    {
        var sql = @"INSERT INTO Tournaments (Id, Name, StartDate, EndDate, Location)
                    VALUES (@Id, @Name, @StartDate, @EndDate, @Location)";

        var parameters = new
        {
            tournament.Id,
            tournament.Name,
            tournament.StartDate,
            tournament.EndDate,
            tournament.Location
        };

        int affectedRows = await _dbConnection.ExecuteAsync(sql, parameters);
        if(affectedRows == 0)
        {
            throw new InsertFailedException(ExceptionMessages.InsertFailed);
        }
    }


    public async Task<Tournament?> GetByIdAsync(Guid id)
    {
        var sql = "SELECT * FROM Tournaments WHERE Id = @Id";
        return await _dbConnection.QueryFirstOrDefaultAsync<Tournament>(sql, new { Id = id });
    }

    public async Task<IEnumerable<Tournament>> GetAllAsync()
    {
        var sql = "SELECT * FROM Tournaments";
        var rows = await _dbConnection.QueryAsync<Tournament>(sql);
        return rows.ToList();
    }

    public async Task UpdateAsync(Tournament tournament)
    {
        var sql = @"UPDATE Tournaments
                    SET Name = @Name, StartDate = @StartDate, EndDate = @EndDate, Location = @Location
                    WHERE Id = @Id";

        var parameters = new
        {
            tournament.Id,
            tournament.Name,
            tournament.StartDate,
            tournament.EndDate,
            tournament.Location
        };

        int affectedRows = await _dbConnection.ExecuteAsync(sql, parameters);

        if (affectedRows == 0)
        {
            throw new NotFoundException(ExceptionMessages.TournamentNotFound);
        }
    }

    public async Task DeleteAsync(Guid id)
    {
        var sql = "DELETE FROM Tournaments WHERE Id = @Id";
        int affectedRows = await _dbConnection.ExecuteAsync(sql, new { Id = id });

        if(affectedRows == 0)
        {
            throw new NotFoundException(ExceptionMessages.TournamentNotFound);
        }
    }
}
