namespace MusicalQuiz.Main.Modules.Infrastructure.Jwt
{
    public class TokenSetting
    {
        public string PublicKey { get; set; }
        public string PrivateKey { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public int AccessExpiration { get; set; }
    }
}
