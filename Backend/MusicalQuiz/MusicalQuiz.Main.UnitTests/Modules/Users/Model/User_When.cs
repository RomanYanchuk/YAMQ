using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using FluentAssertions;
using MusicalQuiz.Main.Modules.Users.Exceptions;
using MusicalQuiz.Main.Modules.Users.Model;
using NUnit.Framework;

namespace MusicalQuiz.Main.UnitTests.Modules.Users.Model
{
    [TestFixture]
    public class User_When
    {
        [Test]
        public void CreatingWithoutId_Then_UserCreatedWithHashedPassword()
        {
            // Act
            var result = new User("username", "user@email", "password");

            // Assert
            result.Username.Should().Be("username");
            result.Email.Should().Be("user@email");
            result.Password.Should().Be(GetHashString("password"));

        }

        [Test]
        public void CreatingWithId_Then_UserCreatedWithAppropriatePassword()
        {
            // Act
            var result = new User("username", "user@email", "passwordHash", "userId");

            // Assert
            result.Username.Should().Be("username");
            result.Email.Should().Be("user@email");
            result.Password.Should().Be("passwordHash");
        }

        [TestCase(null)]
        [TestCase("")]
        [TestCase(" ")]
        [TestCase("123")]
        [TestCase("!@#$")]
        public void CreatingAndUsernameInvalid_Then_ExceptionIsThrown(string username)
        {
            // Act
            Action act = () => _ = new User(username, "user@email", "password");

            // Assert
            act.Should().ThrowExactly<ValueRequiredException>().WithMessage("The value Username is null or invalid.");
        }

        [TestCase(null)]
        [TestCase("")]
        [TestCase(" ")]
        [TestCase("user@")]
        [TestCase("@email")]
        public void CreatingAndEmailInvalid_Then_ExceptionIsThrown(string email)
        {
            // Act
            Action act = () => _ = new User("username", email, "password");

            // Assert
            act.Should().ThrowExactly<ValueRequiredException>().WithMessage("The value Email is null or invalid.");
        }

        [TestCase(null)]
        [TestCase("")]
        [TestCase(" ")]
        [TestCase("text")]
        public void CreatingAndPasswordInvalid_Then_ExceptionIsThrown(string password)
        {
            // Act
            Action act = () => _ = new User("username", "user@email", password);

            // Assert
            act.Should().ThrowExactly<ValueRequiredException>().WithMessage("The value Password is null or invalid.");
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