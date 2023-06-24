using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using MusicalQuiz.Main.Modules.Users.Api;
using MusicalQuiz.Main.Modules.Users.Exceptions;
using MusicalQuiz.Main.Modules.Users.Model;
using MusicalQuiz.Main.Modules.Users.Model.Repository;
using NSubstitute;
using NUnit.Framework;

namespace MusicalQuiz.Main.UnitTests.Modules.Users.Api
{
    [TestFixture]
    public class UsersApi_When_LoggingIn
    {
        private IUsersRepository _repository;
        private LoginContract _contract;
        private ITokenGenerator _tokenGenerator;
        private TokenResponse _tokenResponse;

        [SetUp]
        public void Setup()
        {
            _repository = Substitute.For<IUsersRepository>();
            _tokenGenerator = Substitute.For<ITokenGenerator>();
            _tokenResponse = new TokenResponse("token");
            _tokenGenerator.GenerateToken(Arg.Any<User>()).Returns(_tokenResponse);
            var user = new User("username", "user@email", GetHashString("password"), "userId");
            _repository.Find(Arg.Any<string>()).Returns(user);
            _contract = new LoginContract { Email = "user@email", Password = "password" };
        }

        [Test]
        public async Task Then_RepositoryCalled()
        {
            // Arrange
            var api = new UsersApi(_repository, _tokenGenerator);

            // Act
            await api.Login(_contract);

            // Assert
            await _repository.Received(1).Find("user@email");
        }

        [Test]
        public async Task Then_TokenGeneratorCalledAndTokenResponseReturned()
        {
            // Arrange
            var api = new UsersApi(_repository, _tokenGenerator);

            // Act
            var result = await api.Login(_contract);

            // Assert
            _tokenGenerator.Received(1).GenerateToken(Arg.Any<User>());
            result.Token.Should().NotBeNullOrWhiteSpace();
        }

        [Test]
        public async Task If_PasswordIsWrong_Then_ExceptionIsThrown()
        {
            // Arrange
            var api = new UsersApi(_repository, _tokenGenerator);
            var contractWithWrongPassword = new LoginContract { Email = "user@email", Password = "incorrectPassword" };

            // Act
            Func<Task> act = async () => await api.Login(contractWithWrongPassword);

            // Assert
            await act.Should().ThrowExactlyAsync<WrongPasswordException>();
        }

        private static string GetHashString(string password)
        {
            var bytes = Encoding.Unicode.GetBytes(password);
            var csp =
                new MD5CryptoServiceProvider();
            var byteHash = csp.ComputeHash(bytes);
            return byteHash.Aggregate(string.Empty, (current, b) => current + $"{b:x2}");
        }
    }
}