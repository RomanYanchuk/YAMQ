using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MusicalQuiz.Main.Modules.Infrastructure.Dependencies;
using SpotifyAPI.Web;

namespace MusicalQuiz.Main.Dependencies
{
    public class DependenciesRegistrar
    {
        private readonly IServiceCollection _serviceCollection;
        private readonly IConfiguration _configuration;

        public DependenciesRegistrar(IServiceCollection serviceCollection,
            IConfiguration configuration)
        {
            _serviceCollection = serviceCollection;
            _configuration = configuration;
        }

        public void Register()
        {
            LoadApiFactory().GetAwaiter().GetResult();
            var connectionString = _configuration.GetConnectionString("DefaultConnection");
            new ModulesDependenciesRegistrar(_serviceCollection).Register(connectionString);
        }

        private async Task LoadApiFactory()
        {
            var clientAuthData = _configuration.GetSection("SpotifyApi");
            var request = new ClientCredentialsRequest(clientAuthData["ClientId"], clientAuthData["ClientSecret"]);
            var config = SpotifyClientConfig.CreateDefault();
            var response = await new OAuthClient(config).RequestToken(request);
            var spotifyClient = new SpotifyClient(config.WithToken(response.AccessToken));
            _serviceCollection.AddSingleton<ISpotifyClient>(spotifyClient);
        }
    }
}