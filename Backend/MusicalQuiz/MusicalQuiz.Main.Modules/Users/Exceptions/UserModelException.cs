using System;

namespace MusicalQuiz.Main.Modules.Users.Exceptions
{
    public class UserModelException : Exception
    {
        public UserModelException() { }

        public UserModelException(string message) : base(message)
        { }

        public UserModelException(string message, Exception innerException) : base(message,
            innerException)
        { }
    }
}