using FluentAssertions;
using MusicalQuiz.Main.Modules.Users.Model;
using MusicalQuiz.Main.Modules.Users.Model.Repository;
using NUnit.Framework;

namespace MusicalQuiz.Main.UnitTests.Modules.Users.Model.Repository
{
    [TestFixture]
    public class UsersReconstructionFactory_When_Creating
    {
        [Test]
        public void Then_ModelUserReturned()
        {
            // Arrange
            var storageUser = new Main.Modules.Users.Storage.User
            { Email = "user@email", Username = "username", Password = "password", Id = "userId" };
            var reconstructionFactory = new UsersReconstructionFactory();
            var expectedResult = new User("username", "user@email", "password", "userId");

            // Act
            var result = reconstructionFactory.Create(storageUser);

            // Assert
            result.Should().BeEquivalentTo(expectedResult);
        }
    }
}