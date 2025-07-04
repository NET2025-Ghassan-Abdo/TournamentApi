using Microsoft.Build.Framework;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;
using Tournaments.Api.Extensions;
using Tournaments.Core.Repositories;
using Tournaments.Data.Data;
using Tournaments.Data.Mappings;
using Tournaments.Data.Repositories;

namespace Tournaments.Api
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddDbContext<TournamentsApiContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("TournamentDatabase")));

            // Add services to the container.

            builder.Services.AddControllers()
                .AddNewtonsoftJson(options =>
                {
                    options.SerializerSettings.ReferenceLoopHandling =
                    Newtonsoft.Json.ReferenceLoopHandling.Ignore;
                })
                .AddXmlSerializerFormatters();

                
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddScoped<TournamentRepository>();
            builder.Services.AddScoped<GameRepository>();
            builder.Services.AddScoped<IUoW, UoW>();
            builder.Services.AddAutoMapper(typeof(TournamentMappings));

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            await app.SeedDataAsync();
            app.MapControllers();

            app.Run();
        }
    }
}
