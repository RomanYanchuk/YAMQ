namespace MusicalQuiz.Main.Modules.Users.Model.Repository
{
    public interface IUsersStorageMapper
    {
        Storage.User Map(User user);
    }
}