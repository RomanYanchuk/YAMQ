using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MusicalQuiz.Main.Modules.Infrastructure.Dependencies;
using MusicalQuiz.Main.Modules.Users.Exceptions;
using MusicalQuiz.Main.Modules.Users.Storage;

namespace MusicalQuiz.Main.Modules.Users.Model.Repository
{
    [DefaultTransientImplementation]
    public class UsersRepository : IUsersRepository
    {
        private readonly UsersStorage _storage;
        private readonly IUsersStorageMapper _storageMapper;
        private readonly IUsersReconstructionFactory _reconstructionFactory;

        public UsersRepository(UsersStorage storage,
            IUsersStorageMapper storageMapper,
            IUsersReconstructionFactory reconstructionFactory)
        {
            _storage = storage;
            _storageMapper = storageMapper;
            _reconstructionFactory = reconstructionFactory;
        }

        public async Task Add(User user)
        {
            var storageUser = _storageMapper.Map(user);
            await _storage.Users.AddAsync(storageUser);
            await _storage.SaveChangesAsync();
        }

        public async Task<User> Find(string email)
        {
            var storageUser = await _storage.Users.FirstOrDefaultAsync(u => u.Email.Equals(email));
            if (storageUser == null)
            {
                throw new UserNotFoundException(email);
            }

            return _reconstructionFactory.Create(storageUser);
        }

        public async Task<bool> IsEmailUsed(string email) =>
            await _storage.Users.AnyAsync(u => u.Email.Equals(email));

        public async Task<bool> IsUsernameUsed(string username) =>
            await _storage.Users.AnyAsync(u => u.Username.Equals(username));
    }
}