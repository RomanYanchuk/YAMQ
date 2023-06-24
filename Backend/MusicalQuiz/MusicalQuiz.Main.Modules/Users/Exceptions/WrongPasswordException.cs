namespace MusicalQuiz.Main.Modules.Users.Exceptions
{
    public class WrongPasswordException : UserModelException
    {
        public WrongPasswordException() : base(
            "Wrong password.")
        { }
    }
}