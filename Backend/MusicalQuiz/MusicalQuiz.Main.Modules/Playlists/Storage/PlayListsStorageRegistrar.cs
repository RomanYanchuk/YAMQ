using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace MusicalQuiz.Main.Modules.Playlists.Storage
{
    public static class PlayListsStorageRegistrar
    {
        public static void RegisterStorage(IServiceCollection services, string connectionString)
        {
            services.AddDbContextPool<PlaylistsStorage>(
                options => options.UseMySql(connectionString,
                    new MySqlServerVersion(new Version(8, 0, 31)),
                    mySqlOptions =>
                    {
                        mySqlOptions.MigrationsAssembly("MusicalQuiz.Main.Modules");
                        mySqlOptions.DisableBackslashEscaping();
                        mySqlOptions.EnableRetryOnFailure(
                            15,
                            TimeSpan.FromMilliseconds(2000),
                            null!);
                    }));
        }
    }
}