using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Notes.Application.Interfaces;

namespace Notes.Persistence
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPersistence(this IServiceCollection service, IConfiguration configuration)
        {
            var connectionString = configuration["DbConnection"];
            service.AddDbContext<NotesDbContext>(options =>
            {
                options.UseSqlite(connectionString);
            });

            service.AddScoped<INotesDbContext>(provider => provider.GetService<NotesDbContext>()!);
            return service;
        }
    }
}
