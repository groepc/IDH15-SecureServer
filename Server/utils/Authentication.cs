using System;
using System.Security.Cryptography;
using System.Text;

namespace Server.Entities
{
    public class Authentication
    {
        public static string[] Role { get; }
        private readonly Encoding hashEncoding;
        private readonly HashAlgorithm hashAlgorithm;
        private readonly UserHelper _userHelper;

        public Authentication(UserHelper userHelper)
        {
            hashEncoding = Encoding.UTF8;
            hashAlgorithm = HashAlgorithm.Create("SHA-512");
            _userHelper = userHelper;
        }

        static Authentication()
        {
            Role = new[]
            {
                "ondersteuners",
                "beheerders"
            };
        }

        // Authenticates a user and writes a response cookie containing the token of the login session.
        public bool AuthenticateUser(string username, string password)
        {

            // @TODO: Authenticatie controleren

            User user = _userHelper.GetByName(username);

            if (user != null)
            {
                string hash = CreatePasswordhash(username, password, user.Passwordsalt);

                if (hash == user.Passwordhash)
                {
                    //string sessionToken = Guid.NewGuid().ToString("N");
                    //_sessionCache.Set($"SessionUser_{sessionToken}", user, new CacheItemPolicy { SlidingExpiration = TimeSpan.FromMinutes(20D) });
                    //response.Headers["Set-Cookie"] = $"sessionToken={sessionToken}";

                    return true;
                }
            }

            return true;
        }

        // Creates a base64 password hash
        private string CreatePasswordhash(string username, string password, string salt)
        {
            string input = $"{username}|{password}|{salt}";
            byte[] inputBytes = hashEncoding.GetBytes(input);
            byte[] hashBytes = hashAlgorithm.ComputeHash(inputBytes);
            return Convert.ToBase64String(hashBytes);
        }
    }
}