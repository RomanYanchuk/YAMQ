using System;

namespace MusicalQuiz.Main.Exceptions
{
    public class ServiceException : Exception
    {
        private const string ErrorMessageText = "operation-not-allowed";
        private int _statusCode;
        private string _errorMessage;

        public int StatusCode
        {
            get => _statusCode;

            protected set
            {
                if (value < 500 && value >= 400)
                {
                    _statusCode = value;
                }
                else
                {
                    throw new ArgumentOutOfRangeException(nameof(StatusCode),
                        $"Status code {value} is out of range [400-499].");
                }
            }
        }

        public string ErrorMessage
        {
            get => _errorMessage;

            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException(nameof(ErrorMessage),
                        $"Error message should be specified.");
                }

                _errorMessage = value;
            }
        }

        public ServiceException(int statusCode = 400, string errorMessage = ErrorMessageText)
        {
            StatusCode = statusCode;
            ErrorMessage = errorMessage;
        }

        public ServiceException(Exception exception,
            int statusCode = 400,
            string errorMessage = ErrorMessageText) : base(errorMessage, exception)
        {
            StatusCode = statusCode;
            ErrorMessage = errorMessage;
        }
    }
}
