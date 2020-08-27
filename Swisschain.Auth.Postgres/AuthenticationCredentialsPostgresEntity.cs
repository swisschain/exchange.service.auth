using System;
using Swisschain.Auth.Domains;

namespace Swisschain.Auth.Postgres
{
    public class AuthenticationCredentialsPostgresEntity : IAuthenticationCredentials
    {
        public string Id { get; set; }
        
        public string Email { get; set; }
        
        public string Hash { get; set; }
        
        public string Salt { get; set; }
        
        public bool Authenticate(string password)
        {
            var hash = AuthHelper.GeneratePasswordHash(password, Salt);
            return Hash == hash;
        }
        
        internal void SetPassword(string password)
        {
            Salt = Guid.NewGuid().ToString("N");
            Hash = AuthHelper.GeneratePasswordHash(password, Salt);
        }

        public static string EncodeEmail(string email, byte[] key, byte[] initVector)
        {
            return email.ToLower().Encode(key,initVector);
        }

        public static AuthenticationCredentialsPostgresEntity Create(string email, string password, byte[] key,
            byte[] initVector)
        {
            var result = new AuthenticationCredentialsPostgresEntity
            {
                Id = Guid.NewGuid().ToString("N"),
                Email = EncodeEmail(email, key, initVector)
            };
            
            result.SetPassword(password);

            return result;
        }
    }
}