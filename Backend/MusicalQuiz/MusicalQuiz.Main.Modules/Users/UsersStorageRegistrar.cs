using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MusicalQuiz.Main.Modules.Users.Storage;

namespace MusicalQuiz.Main.Modules.Users
{
    public static class UsersStorageRegistrar
    {
        public static void RegisterStorage(IServiceCollection services, string connectionString)
        {

            services.AddDbContextPool<UsersStorage>(
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