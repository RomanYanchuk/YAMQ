using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MusicalQuiz.Main.Exceptions;
using MusicalQuiz.Main.Modules.Users.Api;
using MusicalQuiz.Main.Modules.Users.Exceptions;

namespace MusicalQuiz.Main.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ProfileController : ControllerBase
    {
        private readonly IUsersApi _usersApi;
        public ProfileController(IUsersApi usersApi)
        {
            _usersApi = usersApi;
        }

        [HttpPost]
        [Route("login")]
        public async Task<TokenResponse> Login(LoginContract contract)
        {
            try
            {
                return await _usersApi.Login(contract);
            }
            catch (UserModelException exception)
            {
                throw exception switch
                {
                    ValueRequiredException ex => new ServiceException(ex, errorMessage: ex.Message),
                    WrongPasswordException ex => new WrongDataException(ex.Message),
                    UserNotFoundException ex => new ResourceNotFoundException(ex),
                    _ => new ServiceException()
                };
            }
        }

        [HttpPost]
        [Route("register")]
        public async Task<TokenResponse> Register(RegistrationContract contract)
        {
            try
            {
                return await _usersApi.Register(contract);
            }
            catch (UserModelException exception)
            {
                throw exception switch
                {
                    ValueRequiredException ex => new ServiceException(ex, errorMessage:ex.Message),
                    UserAlreadyExistsException ex => new ResourceAlreadyExistsException(ex),
                    _ => new ServiceException()
                };
            }
        }
    }
}
