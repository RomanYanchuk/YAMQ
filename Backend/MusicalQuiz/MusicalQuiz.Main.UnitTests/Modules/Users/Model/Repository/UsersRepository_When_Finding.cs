using System;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using MusicalQuiz.Main.Modules.Users.Exceptions;
using MusicalQuiz.Main.Modules.Users.Model.Repository;
using MusicalQuiz.Main.Modules.Users.Storage;
using NSubstitute;
using NUnit.Framework;

namespace MusicalQuiz.Main.UnitTests.Modules.Users.Model.Repository
{
    [TestFixture]
    public class UsersRepository_When_Finding
    {
        private IUsersRepository _repository;
        private IUsersReconstructionFactory _reconstructionFactory;
        private User _storageUser;

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
            _storageUser = await storage.Users.FirstOrDefaultAsync(u => u.Email.Equals("user@email"));
            _reconstructionFactory = Substitute.For<IUsersReconstructionFactory>();
            _repository = new UsersRepository(storage, Substitute.For<IUsersStorageMapper>(), _reconstructionFactory);
        }

        [Test]
        public async Task Then_ReconstructionFactoryCalled()
        {
            // Act
            await _repository.Find("user@email");

            // Assert
            _reconstructionFactory.Received().Create(_storageUser);
        }

        [Test]
        public async Task If_UserDoesNotExist_Then_ExceptionIsThrown()
        {
            // Act
            Func<Task> act = async () => await _repository.Find("wrong@email");

            // Assert
            await act.Should().ThrowExactlyAsync<UserNotFoundException>()
                .WithMessage("User with email wrong@email is not found.");
        }
    }
}