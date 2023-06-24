using System.Threading.Tasks;

namespace MusicalQuiz.Main.Modules.Users.Api
{
    public interface IUsersApi
    {
        Task<TokenResponse> Register(RegistrationContract contract);
        Task<TokenResponse> Login(LoginContract contract);
    }
}