using Microsoft.Extensions.DependencyInjection;
using MusicalQuiz.Main.Modules.Playlists.Storage;
using MusicalQuiz.Main.Modules.Users;

namespace MusicalQuiz.Main.Modules.Infrastructure.Dependencies
{
    public class ModulesDependenciesRegistrar
    {
        private readonly IServiceCollection _serviceCollection;

        public ModulesDependenciesRegistrar(IServiceCollection serviceCollection)
        {
            _serviceCollection = serviceCollection;
        }

        public void Register(string connectionString)
        {
            UsersStorageRegistrar.RegisterStorage(_serviceCollection, connectionString);
            PlayListsStorageRegistrar.RegisterStorage(_serviceCollection, connectionString);
            _serviceCollection.Scan(scan => scan.FromAssemblyOf<DefaultTransientImplementation>()
                .AddClasses(c => c.WithAttribute<DefaultTransientImplementation>())
                .AsImplementedInterfaces()
                .WithTransientLifetime());
        }
    }
}