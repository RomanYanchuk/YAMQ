using MusicalQuiz.Main.Modules.Infrastructure.Dependencies;

namespace MusicalQuiz.Main.Modules.Users.Model.Repository
{
    [DefaultTransientImplementation]
    public class UsersStorageMapper : IUsersStorageMapper
    {
        public Storage.User Map(User user)
        {
            return new Storage.User
            {
                Id = user.Id,
                Email = user.Email,
                Username = user.Username,
                Password = user.Password
            };
        }
    }
}