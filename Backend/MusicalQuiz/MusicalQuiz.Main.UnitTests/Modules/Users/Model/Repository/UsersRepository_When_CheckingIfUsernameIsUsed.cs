using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using MusicalQuiz.Main.Modules.Users.Model.Repository;
using MusicalQuiz.Main.Modules.Users.Storage;
using NSubstitute;
using NUnit.Framework;

namespace MusicalQuiz.Main.UnitTests.Modules.Users.Model.Repository
{
    [TestFixture]
    public class UsersRepository_When_CheckingIfUsernameIsUsed
    {
        private IUsersRepository _repository;

        [SetUp]
        public async Task Setup()
        {
            var options = new DbContextOptionsBuilder<UsersStorage>()
                .UseInMemoryDatabase("mq_main")
                .Options;
            var storage = new UsersStorage(options);
            var storageUser = new User { Email = "user@email", Username = "username", Password = "password" };
            await storage.AddAsync(storageUser);
            await storage.SaveChangesAsync();
            _repository = new UsersRepository(storage, Substitute.For<IUsersStorageMapper>(),
                Substitute.For<IUsersReconstructionFactory>());
        }

        [Test]
        public async Task If_UsernameExists_Then_TrueReturned()
        {
            // Act
            var result = await _repository.IsUsernameUsed("username");

            // Assert
            result.Should().Be(true);
        }

        [Test]
        public async Task If_UsernameNotExists_Then_FalseReturned()
        {
            // Act
            var result = await _repository.IsUsernameUsed("wrongUsername");

            // Assert
            result.Should().Be(false);
        }
    }
}