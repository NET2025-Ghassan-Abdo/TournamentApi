using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tournaments.Core.Entities;
using Tournaments.Core.Repositories;
using Tournaments.Data.Data;

namespace Tournaments.Data.Repositories
{
    public class GameRepository : IGameRepository
    {
        private readonly TournamentsApiContext _context;

        public GameRepository(TournamentsApiContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Game>> GetAllAsync()
        {
            return await _context.Games
                .Include(t=> t.TournamentDetails)
                .AsNoTracking()
                .ToListAsync();

        }
        public async Task<Game> GetAsync(int id)
        {
            return await _context.Games
                //.Include(g => g.TournamentDetails)
                .FirstOrDefaultAsync(g => g.Id == id);
        }
        public Task<bool> AnyAsync(int id)
        {
            return _context.Games
                .AnyAsync(g => g.Id == id);
        }

        public void Add(Game game)
        {
            _context.Games.Add(game);
        }
        public void Update(Game game)
        {
            _context.Update(game);
        }
        public void Remove(Game game)
        {
            _context.Remove(game);
        }

        public async Task<IEnumerable<Game>> GetByTitleAsync(string title)
        {
            return await _context.Games
        .AsNoTracking()
        .Where(g => g.Title == title)
        .ToListAsync(); 
        }
    }
}
