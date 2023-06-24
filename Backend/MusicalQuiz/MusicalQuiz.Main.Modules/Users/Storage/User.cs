using System;
using System.ComponentModel.DataAnnotations;

namespace MusicalQuiz.Main.Modules.Users.Storage
{
    public class User
    {
        [Key] public string Id { get; set; }
        [Required] public string Username { get; set; }
        [Required] public string Email { get; set; }
        [Required] public string Password { get; set; }
        [Required] public DateTime DateCreated { get; set; }
    }
}