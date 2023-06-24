using System;
using FluentAssertions;
using MusicalQuiz.Main.Exceptions;
using NUnit.Framework;

namespace MusicalQuiz.Main.UnitTests.Application.Exceptions
{
    [TestFixture]
    class ServiceException_When_Create
    {
        [Test]
        public void If_StatusCodeParameterInConstructorIsMissing_Then_PropertyStatusCodeDefault()
        {
            // Arrange
            var exception = new ServiceException();

            // Act
            var result = exception.StatusCode;

            // Assert
            result.Should().Be(400);
        }

        [Test]
        public void If_StatusCodeParameterInConstructorIsNotBetween400And499_Then_ExceptionIsThrown()
        {
            // Act
            Action act = () => { _ = new ServiceException(500); };

            // Assert
            act.Should().ThrowExactly<ArgumentOutOfRangeException>()
                .WithMessage($"Status code 500 is out of range [400-499]. (Parameter 'StatusCode')");
        }

        [Test]
        public void If_StatusCodeParameterIsBetween400And499_Then_PropertyStatusCodeIsEqualToInitial()
        {
            // Arrange
            const int statusCode = 404;
            var exception = new ServiceException(statusCode);

            // Act
            var result = exception.StatusCode;

            // Assert
            result.Should().Be(statusCode);
        }

        [Test]
        public void If_ErrorMessageIsNotEmpty_Then_NoException()
        {
            // Arrange
            const string errorMessage = "non-empty";

            // Act
            Action act = () => _ = new ServiceException(errorMessage: errorMessage);

            // Assert
            act.Should().NotThrow();
        }

        [TestCase("")]
        [TestCase(" ")]
        [TestCase(null)]
        public void If_ErrorMessageIsEmpty_Then_ExceptionIsThrown(string errorMessage)
        {
            // Act
            Action act = () => _ = new ServiceException(errorMessage: errorMessage);

            // Assert
            act.Should().ThrowExactly<ArgumentException>()
                .WithMessage("ErrorMessage (Parameter 'Error message should be specified.')");
        }
    }
}
