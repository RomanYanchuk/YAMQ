using System;
using System.Net.Http.Headers;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using MusicalQuiz.Main.Infrastructure.Database;
using MusicalQuiz.Main.Infrastructure.System;

namespace MusicalQuiz.Main.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class SystemController : Controller
    {
        private readonly IDatabaseUpdater _databaseUpdater;
        private readonly SystemUser _systemUser;

        public SystemController(IDatabaseUpdater databaseUpdater,
            SystemUser systemUser)
        {
            _databaseUpdater = databaseUpdater;
            _systemUser = systemUser;
        }

        [HttpPost]
        [Route("update-database")]
        public string UpdateDatabase()
        {
            try
            {
                var authHeader = AuthenticationHeaderValue.Parse(Request.Headers["Authorization"]);
                var credentialBytes = Convert.FromBase64String(authHeader.Parameter ?? string.Empty);
                var credentials = Encoding.UTF8.GetString(credentialBytes).Split(new[] {':'}, 2);
                var username = credentials[0];
                var password = credentials[1];

                if (username == _systemUser.Username && password == _systemUser.Password)
                {
                    _databaseUpdater.UpdateDatabase();
                    return "The database has been successfully updated.";
                }

                Response.StatusCode = 403;
                return "Forbidden";
            }
            catch (FormatException)
            {
                Response.StatusCode = 401;
                return "Error, authorization is needed.";
            }
        }
    }
}
