using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using MusicalQuiz.Main.Modules.Users.Exceptions;

namespace MusicalQuiz.Main.Modules.Users.Model
{
    public class User
    {
        #region Properties

        public string Id { get; }
        public string Username { get; }
        public string Email { get; }
        public string Password { get; }
        
        #endregion

        #region Constructors

        public User(string username,
            string email,
            string password) : this(username,
            email,
            GetHashString(password),
            null)
        { }

        public User(string username, string email, string password, string id)
        {
            if (!IsUsernameValid(username))
            {
                throw new ValueRequiredException(nameof(Username));
            }
            if (!IsEmailValid(email))
            {
                throw new ValueRequiredException(nameof(Email));
            }
            if (!IsPasswordValid(password))
            {
                throw new ValueRequiredException(nameof(password));
            }

            Username = username;
            Email = email;
            Password = password;
            Id = id;
        }

        #endregion

        #region Password Hash

        public bool IsPasswordCorrect(string password) => GetHashString(password).Equals(Password);

        private static string GetHashString(string password)
        {
            if (!IsPasswordValid(password))
            {
                throw new ValueRequiredException(nameof(Password));
            }
            var bytes = Encoding.Unicode.GetBytes(password);
            var csp =
                new MD5CryptoServiceProvider();
            var byteHash = csp.ComputeHash(bytes);
            return byteHash.Aggregate(string.Empty, (current, b) => current + $"{b:x2}");
        }

        #endregion

        #region Validation

        private static bool IsEmailValid(string email)
        {
            try
            {
                var mailAddress = new System.Net.Mail.MailAddress(email);
                return mailAddress.Address == email;
            }
            catch
            {
                return false;
            }
        }

        private static bool IsUsernameValid(string username) => !string.IsNullOrWhiteSpace(username) && Regex.IsMatch(username, @"^[a-zA-Z0-9]{4,}$");

        private static bool IsPasswordValid(string password) => !string.IsNullOrWhiteSpace(password) && password.Length >= 8;

        #endregion

        #region Equals and GetHashCode

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }

            if (ReferenceEquals(this, obj))
            {
                return true;
            }

            if (obj.GetType() != this.GetType())
            {
                return false;
            }

            return Equals((User)obj);
        }

        private bool Equals(User other) =>
            Id.Equals(other.Id) &&
            Email.Equals(other.Email) &&
            Username.Equals(other.Username) &&
            Password.Equals(other.Password);


        public override int GetHashCode() =>
            HashCode.Combine(Id, Email, Username, Password);

        #endregion
    }
}