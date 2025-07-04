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
    public class TournamentRepository : ITournamentRepository
    {
        private readonly TournamentsApiContext _context;

        public TournamentRepository(TournamentsApiContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Tournament>> GetAllAsync(bool IncludeGames=false)
        {
            return await _context.Tournaments
                .Include(t => t.Games)
                .AsNoTracking()
                .ToListAsync();
        }
        public async Task<Tournament> GetAsync(int id)
        {
            return await _context.Tournaments
                
                .FirstOrDefaultAsync(t => t.Id == id);
        }
        public async Task<bool> AnyAsync(int id)
        {
            return await _context.Tournaments
                .AnyAsync(t => t.Id == id);
        }

        public void Add(Tournament tournament)
        {
            _context.Tournaments.Add(tournament);
        }

        public void Update(Tournament tournament)
        {
            _context.Tournaments.Update(tournament);
        }
        public void Remove(Tournament tournament)
        {
            _context.Tournaments.Remove(tournament);
        }

        

        
    }
}
