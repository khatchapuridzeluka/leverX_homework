using leverX.Application.Interfaces.Repositories;
using leverX.Domain.Entities;

namespace leverX.Infrastructure.Repositories
{
    public class GameRepository : IGameRepository
    {

        private readonly List<Game> _games = new();
        public Task AddAsync(Game game)
        {
            _games.Add(game);
            return Task.CompletedTask;
        }

        public Task<Game?> GetByIdAsync(Guid id)
        {
            return Task.FromResult(_games.FirstOrDefault(g => g.Id == id));
        }
        public Task<List<Game>> GetAllAsync()
        {
            return Task.FromResult(_games.ToList());
        }


        public Task UpdateAsync(Game game)
        {
            var existing = _games.FirstOrDefault(g => g.Id == game.Id);
            if (existing != null)
            {
                existing.WhitePlayer = game.WhitePlayer;
                existing.BlackPlayer = game.BlackPlayer;
                existing.Result = game.Result;
                existing.Moves = game.Moves;
                existing.PlayedOn = game.PlayedOn;
                existing.Opening = game.Opening;
            }
            return Task.CompletedTask;
        }

        public Task DeleteAsync(Guid id)
        {
            _games.RemoveAll(g => g.Id == id);
            return Task.CompletedTask;
        }
    }
}
