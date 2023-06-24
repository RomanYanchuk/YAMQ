namespace MusicalQuiz.Main.Modules.Users.Exceptions
{
    public class ValueRequiredException : UserModelException
    {
        public ValueRequiredException(string name) : base(
        $"The value {name} is null or invalid.")
        { }
    }
}
