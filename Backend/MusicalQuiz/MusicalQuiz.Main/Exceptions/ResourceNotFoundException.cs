using System;
using System.Net;

namespace MusicalQuiz.Main.Exceptions
{
    public class ResourceNotFoundException : ServiceException
    {
        private const int NotFoundCode = (int) HttpStatusCode.NotFound;

        public ResourceNotFoundException() : base(NotFoundCode)
        { }

        public ResourceNotFoundException(Exception exception) : base(exception, NotFoundCode, exception.Message)
        { }
    }
}
