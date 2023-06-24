using System.ComponentModel.DataAnnotations;

namespace MusicalQuiz.Main.Modules.Users.Api
{
    public class RegistrationContract
    {
        [Required] public string Email { get; set; }
        [Required] public string Username { get; set; }
        [Required] public string Password { get; set; }
    }
}