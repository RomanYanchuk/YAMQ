using System.ComponentModel.DataAnnotations;

namespace MusicalQuiz.Main.Modules.Users.Api
{
    public class LoginContract
    {
        [Required] public string Email { get; set; }
        [Required] public string Password { get; set; }
    }
}