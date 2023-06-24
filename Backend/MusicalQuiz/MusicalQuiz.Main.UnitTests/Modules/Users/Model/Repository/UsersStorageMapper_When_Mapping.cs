using FluentAssertions;
using MusicalQuiz.Main.Modules.Users.Model;
using MusicalQuiz.Main.Modules.Users.Model.Repository;
using NUnit.Framework;

namespace MusicalQuiz.Main.UnitTests.Modules.Users.Model.Repository
{
    [TestFixture]
    public class UsersStorageMapper_When_Mapping
    {
        [Test]
        public void Then_StorageUserReturned()
        {
            // Arrange
            var storageMapper = new UsersStorageMapper();
            var modelUser = new User("username", "user@email", "password", "userId");
            var expectedResult = new Main.Modules.Users.Storage.User
                { Email = "user@email", Username = "username", Password = "password", Id = "userId" };

            // Act
            var result = storageMapper.Map(modelUser);

            // Assert
            result.Should().BeEquivalentTo(expectedResult);
        }
    }
}