using System;
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
    public class UsersApi_When_Registering
    {
        private IUsersRepository _repository;
        private ITokenGenerator _tokenGenerator;
        private RegistrationContract _contract;
        private TokenResponse _tokenResponse;

        [SetUp]
        public void Setup()
        {
            _repository = Substitute.For<IUsersRepository>();
            _tokenGenerator = Substitute.For<ITokenGenerator>();
            _tokenResponse = new TokenResponse("token");
            _tokenGenerator.GenerateToken(Arg.Any<User>()).Returns(_tokenResponse);
            _repository.IsEmailUsed(Arg.Any<string>()).Returns(false);
            _repository.IsUsernameUsed(Arg.Any<string>()).Returns(false);
            var user = new User("username", "user@email", "passwordHash", "userId");
            _repository.Find(Arg.Any<string>()).Returns(user);
            _contract = new RegistrationContract
            { Username = "username", Email = "user@email", Password = "password" };
        }

        [Test]
        public async Task Then_RepositoryCalled()
        {
            // Arrange
            var api = new UsersApi(_repository, _tokenGenerator);

            // Act
            await api.Register(_contract);

            // Assert
            await _repository.Received(1).IsEmailUsed("user@email");
            await _repository.Received(1).IsUsernameUsed("username");
            await _repository.Received(1).Add(Arg.Any<User>());
            await _repository.Received(1).Find("user@email");
        }

        [Test]
        public async Task Then_TokenGeneratorCalledAndTokenResponseReturned()
        {
            // Arrange
            var api = new UsersApi(_repository, _tokenGenerator);

            // Act
            var result = await api.Register(_contract);

            // Assert
            _tokenGenerator.Received(1).GenerateToken(Arg.Any<User>());
            result.Should().BeEquivalentTo(_tokenResponse);
        }

        [Test]
        public async Task If_EmailIsUsed_Then_ExceptionIsThrown()
        {
            // Arrange
            _repository.IsEmailUsed(Arg.Any<string>()).Returns(true);
            var api = new UsersApi(_repository, _tokenGenerator);

            // Act
            Func<Task> act = async () => await api.Register(_contract);

            // Assert
            await act.Should().ThrowExactlyAsync<UserAlreadyExistsException>()
                .WithMessage($"User with Email {_contract.Email} is already exist.");
        }

        [Test]
        public async Task If_UsernameIsUsed_Then_ExceptionIsThrown()
        {
            // Arrange
            _repository.IsUsernameUsed(Arg.Any<string>()).Returns(true);
            var api = new UsersApi(_repository, _tokenGenerator);

            // Act
            Func<Task> act = async () => await api.Register(_contract);

            // Assert
            await act.Should().ThrowExactlyAsync<UserAlreadyExistsException>()
                .WithMessage($"User with Username {_contract.Username} is already exist.");
        }
    }
}