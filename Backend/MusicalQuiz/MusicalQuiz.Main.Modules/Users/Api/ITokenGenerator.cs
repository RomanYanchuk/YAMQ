using MusicalQuiz.Main.Modules.Users.Model;

namespace MusicalQuiz.Main.Modules.Users.Api
{
    public interface ITokenGenerator
    {
        TokenResponse GenerateToken(User user);
    }
}