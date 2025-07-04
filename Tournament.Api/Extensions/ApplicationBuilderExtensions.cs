using Tournaments.Data.Data;

namespace Tournaments.Api.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public static async Task SeedDataAsync(this IApplicationBuilder application)
        {
            //Skapa ett scope för att få fram DbContext
            using var scope = application.ApplicationServices.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<TournamentsApiContext>();

            await SeedData.InitializeAsync(context);
        }
    }
}
