using System;
using System.Linq;
using System.Threading.Tasks;
using Tournaments.Core.Entities;

namespace Tournaments.Data.Data
{
    public static class SeedData
    {
        public static async Task InitializeAsync(TournamentsApiContext context)
        {
            // Se till att databasen är skapad
            context.Database.EnsureCreated();

            // Om vi redan har data, avbryt
            if (context.Tournaments.Any())
                return;

            // Skapa exempelturneringar
            var tournament1 = new Tournament
            {
                Title = "Vårturnering 2025",
                StartDate = new DateTime(2025, 4, 1)
            };
            var tournament2 = new Tournament
            {
                Title = "Sommarcupen 2025",
                StartDate = new DateTime(2025, 6, 15)
            };

            // Lägg till matcher till turneringarna
            tournament1.Games = new[]
            {
                new Game { Title = "Vårpremiär", Time = tournament1.StartDate.AddDays(0).AddHours(10) },
                new Game { Title = "Kvartsfinal", Time = tournament1.StartDate.AddDays(7).AddHours(12) },
                new Game { Title = "Final", Time = tournament1.StartDate.AddDays(14).AddHours(15) }
            };

            tournament2.Games = new[]
            {
                new Game { Title = "Gruppspel #1", Time = tournament2.StartDate.AddDays(0).AddHours(9) },
                new Game { Title = "Semifinal", Time = tournament2.StartDate.AddDays(10).AddHours(11) },
                new Game { Title = "Final", Time = tournament2.StartDate.AddDays(20).AddHours(16) }
            };

            // Lägg till i kontexten
            context.Tournaments.AddRange(tournament1, tournament2);

            // Spara till databasen
            await context.SaveChangesAsync();
        }
    }
}
