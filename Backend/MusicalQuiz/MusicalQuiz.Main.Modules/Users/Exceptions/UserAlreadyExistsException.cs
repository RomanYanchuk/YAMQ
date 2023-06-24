namespace MusicalQuiz.Main.Modules.Users.Exceptions
{
    public class UserAlreadyExistsException : UserModelException
    {
        public UserAlreadyExistsException(string property, string value) : base(
              $"User with {property} {value} is already exist.")
        { }
    }
}