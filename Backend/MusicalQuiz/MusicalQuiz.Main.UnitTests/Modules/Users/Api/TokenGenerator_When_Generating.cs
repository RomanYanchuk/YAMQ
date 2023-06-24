using System;
using System.Security.Cryptography;
using FluentAssertions;
using MusicalQuiz.Main.Modules.Infrastructure.Jwt;
using MusicalQuiz.Main.Modules.Users.Api;
using MusicalQuiz.Main.Modules.Users.Model;
using NUnit.Framework;

namespace MusicalQuiz.Main.UnitTests.Modules.Users.Api
{
    [TestFixture]
    public class TokenGenerator_When_Generating
    {
        [Test]
        public void Then_TokenReturned()
        {
            // Arrange
            using var rsa = RSA.Create();
            var tokenSetting = new TokenSetting
            {
                PrivateKey = Convert.ToBase64String(rsa.ExportRSAPrivateKey())
            };
            var user = new User("username", "user@email", "password", "userId");
            var tokenGenerator = new TokenGenerator(tokenSetting);

            // Act
            var result = tokenGenerator.GenerateToken(user);

            // Assert
            result.Token.Should().NotBeNullOrWhiteSpace();
        }
    }
}