using System.Threading.Tasks;

namespace MusicalQuiz.Main.Modules.Users.Model.Repository
{
    public interface IUsersRepository
    {
        Task Add(User user);
        Task<User> Find(string email);
        Task<bool> IsEmailUsed(string email);
        Task<bool> IsUsernameUsed(string username);
    }
}