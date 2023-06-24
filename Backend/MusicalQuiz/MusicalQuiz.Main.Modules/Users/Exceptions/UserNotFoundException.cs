namespace MusicalQuiz.Main.Modules.Users.Exceptions
{
    public class UserNotFoundException : UserModelException
    {
        public UserNotFoundException(string email) : base(
            $"User with email {email} is not found.")
        { }
    }
}