using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Tournaments.Core.Entities;

namespace Tournaments.Data.Data
{
    public class TournamentsApiContext : DbContext
    {
        public TournamentsApiContext (DbContextOptions<TournamentsApiContext> options)
            : base(options)
        {
        }

        public DbSet<Tournament> Tournaments { get; set; } = default!;
        public DbSet<Game> Games { get; set; } = default!;
    }
}
