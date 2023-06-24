using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using Microsoft.IdentityModel.Tokens;
using MusicalQuiz.Main.Modules.Infrastructure.Dependencies;
using MusicalQuiz.Main.Modules.Infrastructure.Jwt;
using MusicalQuiz.Main.Modules.Users.Model;

namespace MusicalQuiz.Main.Modules.Users.Api
{
    [DefaultTransientImplementation]
    public class TokenGenerator : ITokenGenerator
    {
        private readonly TokenSetting _tokenSetting;
        public TokenGenerator(TokenSetting tokenSetting)
        {
            _tokenSetting = tokenSetting;
        }

        public TokenResponse GenerateToken(User user)
        {
            var userIdClaim = new Claim(ClaimTypes.NameIdentifier, user.Id);
            var usernameClaim = new Claim(ClaimTypes.Name, user.Username);
            var emailClaim = new Claim(ClaimTypes.Email, user.Email);
            var claims = new[] { userIdClaim, usernameClaim, emailClaim };

            using var rsa = RSA.Create();
            rsa.ImportRSAPrivateKey(
                Convert.FromBase64String(_tokenSetting.PrivateKey),
                out _);

            var key = new RsaSecurityKey(rsa);
            var credentials = new SigningCredentials(key, SecurityAlgorithms.RsaSha256)
            {
                CryptoProviderFactory = new CryptoProviderFactory { CacheSignatureProviders = false }
            };

            var jwtToken = new JwtSecurityToken(
                _tokenSetting.Issuer,
                _tokenSetting.Audience,
                claims,
                expires: DateTime.UtcNow.AddDays(_tokenSetting.AccessExpiration),
                signingCredentials: credentials
            );
            return new TokenResponse(new JwtSecurityTokenHandler().WriteToken(jwtToken));
        }
    }
}