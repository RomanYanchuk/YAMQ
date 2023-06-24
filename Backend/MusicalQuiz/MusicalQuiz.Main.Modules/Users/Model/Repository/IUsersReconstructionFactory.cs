namespace MusicalQuiz.Main.Modules.Users.Model.Repository
{
    public interface IUsersReconstructionFactory
    {
        User Create(Storage.User storageUser);
    }
}