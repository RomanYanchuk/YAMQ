using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using MusicalQuiz.Main.Modules.Users.Model.Repository;
using MusicalQuiz.Main.Modules.Users.Storage;
using NSubstitute;
using NUnit.Framework;
using User = MusicalQuiz.Main.Modules.Users.Model.User;

namespace MusicalQuiz.Main.UnitTests.Modules.Users.Model.Repository
{
    [TestFixture]
    public class UsersRepository_When_Adding
    {
        private IUsersRepository _repository;
        private IUsersStorageMapper _storageMapper;
        private UsersStorage _storage;
        private User _modelUser;

        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<UsersStorage>()
                .UseInMemoryDatabase("mq_main")
                .Options;
            _storage = new UsersStorage(options);
            _storageMapper = Substitute.For<IUsersStorageMapper>();
            var storageUser = new Main.Modules.Users.Storage.User
            { Email = "user@email", Username = "username", Password = "password" };
            _storageMapper.Map(Arg.Any<User>()).Returns(storageUser);
            _modelUser = new User("username", "user@email", "password");
            _repository = new UsersRepository(_storage, _storageMapper, Substitute.For<IUsersReconstructionFactory>());
        }

        [Test]
        public async Task Then_StorageMapperCalled()
        {
            // Act
            await _repository.Add(_modelUser);

            // Assert
            _storageMapper.Received(1).Map(_modelUser);
        }

        [Test]
        public async Task Then_UserAdded()
        {
            // Act
            await _repository.Add(_modelUser);

            // Assert
            var result = await _storage.Users.FirstOrDefaultAsync(u => u.Email.Equals("user@email"));
            result.Should().NotBeNull();
        }
    }
}