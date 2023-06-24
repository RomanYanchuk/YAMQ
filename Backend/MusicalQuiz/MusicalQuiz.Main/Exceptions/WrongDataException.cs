using System;
using System.Net;

namespace MusicalQuiz.Main.Exceptions
{
    public class WrongDataException : ServiceException
    {
        private const int UnprocessableEntity = (int)HttpStatusCode.UnprocessableEntity;

        public WrongDataException() : base(UnprocessableEntity)
        { }

        public WrongDataException(Exception exception) : base(exception, UnprocessableEntity, exception.Message)
        { }
        public WrongDataException(string message) : base(UnprocessableEntity, message)
        { }
    }
}
