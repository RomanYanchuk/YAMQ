using System;
using System.Net;
using Microsoft.AspNetCore.Http;
using MusicalQuiz.Main.Exceptions;

namespace MusicalQuiz.Main.Infrastructure.Extensions
{
    public static class HttpResponseExtensions
    {
        public static void TransformExceptionToResponse(this HttpResponse response, Exception exception)
        {
            response.StatusCode = GetStatusCode(exception);
        }


        private static int GetStatusCode(Exception exception)
        {
            if (exception is ServiceException serviceException)
            {
                return serviceException.StatusCode;
            }

            return (int)HttpStatusCode.InternalServerError;
        }
    }
}
