using System.Threading.Tasks;
using MusicalQuiz.Main.Modules.Users.Exceptions;
using MusicalQuiz.Main.Modules.Users.Model.Repository;
using MusicalQuiz.Main.Modules.Users.Model;
using MusicalQuiz.Main.Modules.Infrastructure.Dependencies;

namespace MusicalQuiz.Main.Modules.Users.Api
{
    [DefaultTransientImplementation]
    public class UsersApi : IUsersApi
    {
        private readonly IUsersRepository _repository;
        private readonly ITokenGenerator _tokenGenerator;

        public UsersApi(IUsersRepository repository, ITokenGenerator tokenGenerator)
        {
            _repository = repository;
            _tokenGenerator = tokenGenerator;
        }

        public async Task<TokenResponse> Register(RegistrationContract contract)
        {
            if (await _repository.IsEmailUsed(contract.Email))
            {
                throw new UserAlreadyExistsException(nameof(contract.Email), contract.Email);
            }
            if (await _repository.IsUsernameUsed(contract.Username))
            {
                throw new UserAlreadyExistsException(nameof(contract.Username), contract.Username);
            }

            await _repository.Add(new User(contract.Username, contract.Email, contract.Password));

            var user = await _repository.Find(contract.Email);
            return _tokenGenerator.GenerateToken(user);
        }


        public async Task<TokenResponse> Login(LoginContract contract)
        {
            var user = await _repository.Find(contract.Email);
            if (!user.IsPasswordCorrect(contract.Password))
            {
                throw new WrongPasswordException();
            }

            return _tokenGenerator.GenerateToken(user);
        }
    }
}