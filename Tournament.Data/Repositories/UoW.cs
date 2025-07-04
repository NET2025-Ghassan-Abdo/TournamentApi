using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tournaments.Core.Repositories;
using Tournaments.Data.Data;

namespace Tournaments.Data.Repositories
{
    public class UoW:IUoW
    {
        private readonly TournamentsApiContext _context;
        public ITournamentRepository TournamentRepository { get; }
        public IGameRepository GameRepository { get; }

        public UoW(TournamentsApiContext context, TournamentRepository tournamentRepository
            , GameRepository gameRepository)
        {
            _context = context;
            TournamentRepository = tournamentRepository;
            GameRepository = gameRepository;

        }

        

        public async Task CompletAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
