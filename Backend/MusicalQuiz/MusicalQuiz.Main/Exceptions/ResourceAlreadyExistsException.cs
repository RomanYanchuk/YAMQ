using System;
using System.Net;

namespace MusicalQuiz.Main.Exceptions
{
    public class ResourceAlreadyExistsException : ServiceException
    {
        private const int ConflictCode = (int)HttpStatusCode.Conflict;

        public ResourceAlreadyExistsException() : base(ConflictCode)
        { }

        public ResourceAlreadyExistsException(Exception exception) : base(exception, ConflictCode, exception.Message)
        { }
    }
}
