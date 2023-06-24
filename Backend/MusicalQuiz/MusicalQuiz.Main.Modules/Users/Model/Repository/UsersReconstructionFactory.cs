using MusicalQuiz.Main.Modules.Infrastructure.Dependencies;

namespace MusicalQuiz.Main.Modules.Users.Model.Repository
{
    [DefaultTransientImplementation]
    public class UsersReconstructionFactory : IUsersReconstructionFactory
    {
        public User Create(Storage.User storageUser) =>
            new(storageUser.Username,
                storageUser.Email,
                storageUser.Password,
                storageUser.Id);
    }
}