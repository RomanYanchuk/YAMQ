using System;
using System.Threading.Tasks;
using FluentAssertions;
using MusicalQuiz.Main.Controllers;
using MusicalQuiz.Main.Exceptions;
using MusicalQuiz.Main.Modules.Users.Api;
using MusicalQuiz.Main.Modules.Users.Exceptions;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using NUnit.Framework;

namespace MusicalQuiz.Main.UnitTests.Application.Controllers
{
    [TestFixture]
    public class ProfileController_When
    {
        [Test]
        public async Task Login_If_ApiValueRequiredException_Then_ServiceExceptionIsThrown()
        {
            // Arrange
            var usersApi = Substitute.For<IUsersApi>();
            usersApi.Login(Arg.Any<LoginContract>()).Throws(new ValueRequiredException("name"));
            var controller = new ProfileController(usersApi);

            // Act
            Func<Task> act = async () => await controller.Login(new LoginContract());

            // Assert
            await act.Should().ThrowExactlyAsync<ServiceException>();
        }

        [Test]
        public async Task Login_If_ApiUserNotFoundException_Then_ResourceNotFoundExceptionIsThrown()
        {
            // Arrange
            var usersApi = Substitute.For<IUsersApi>();
            usersApi.Login(Arg.Any<LoginContract>()).Throws(new UserNotFoundException("username"));
            var controller = new ProfileController(usersApi);

            // Act
            Func<Task> act = async () => await controller.Login(new LoginContract());

            // Assert
            await act.Should().ThrowExactlyAsync<ResourceNotFoundException>();
        }

        [Test]
        public async Task Login_If_ApiUserModelException_Then_ServiceExceptionIsThrown()
        {
            // Arrange
            var usersApi = Substitute.For<IUsersApi>();
            usersApi.Login(Arg.Any<LoginContract>()).Throws(new UserModelException("message"));
            var controller = new ProfileController(usersApi);

            // Act
            Func<Task> act = async () => await controller.Login(new LoginContract());

            // Assert
            await act.Should().ThrowExactlyAsync<ServiceException>();
        }

        [Test]
        public async Task Register_If_ApiValueRequiredException_Then_ServiceExceptionIsThrown()
        {
            // Arrange
            var usersApi = Substitute.For<IUsersApi>();
            usersApi.Register(Arg.Any<RegistrationContract>()).Throws(new ValueRequiredException("name"));
            var controller = new ProfileController(usersApi);

            // Act
            Func<Task> act = async () => await controller.Register(new RegistrationContract());

            // Assert
            await act.Should().ThrowExactlyAsync<ServiceException>();
        }

        [Test]
        public async Task Register_If_ApiUserAlreadyExistsException_Then_ResourceAlreadyExistsExceptionIsThrown()
        {
            // Arrange
            var usersApi = Substitute.For<IUsersApi>();
            usersApi.Register(Arg.Any<RegistrationContract>()).Throws(new UserAlreadyExistsException("username", "name"));
            var controller = new ProfileController(usersApi);

            // Act
            Func<Task> act = async () => await controller.Register(new RegistrationContract());

            // Assert
            await act.Should().ThrowExactlyAsync<ResourceAlreadyExistsException>();
        }

        [Test]
        public async Task Register_If_ApiUserModelException_Then_ServiceExceptionIsThrown()
        {
            // Arrange
            var usersApi = Substitute.For<IUsersApi>();
            usersApi.Register(Arg.Any<RegistrationContract>()).Throws(new UserModelException("message"));
            var controller = new ProfileController(usersApi);

            // Act
            Func<Task> act = async () => await controller.Register(new RegistrationContract());

            // Assert
            await act.Should().ThrowExactlyAsync<ServiceException>();
        }
    }
}