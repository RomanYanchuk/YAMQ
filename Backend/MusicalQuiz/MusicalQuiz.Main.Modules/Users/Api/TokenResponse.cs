namespace MusicalQuiz.Main.Modules.Users.Api
{
    public class TokenResponse
    {
        public string Token { get; }
        public TokenResponse(string token) => Token = token;
    }
}