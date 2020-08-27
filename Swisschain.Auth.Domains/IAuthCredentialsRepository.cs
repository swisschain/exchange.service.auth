using System.Threading.Tasks;

namespace Swisschain.Auth.Domains
{
    public interface IAuthenticationCredentials
    {
        public string Id { get; set; }
        
        public string Email { get; set; }
        
        public string Hash { get; set; }
        
        public string Salt { get; set; }

        bool Authenticate(string password);
    }
    
    
    public interface IAuthCredentialsRepository
    {
        Task<IAuthenticationCredentials> GetByEmailAsync(string email);
        Task<IAuthenticationCredentials> GetByIdAsync(string id);
        Task<IAuthenticationCredentials> RegisterAsync(string email, string password);
        Task<IAuthenticationCredentials> ChangePasswordAsync(string email, string password);
    }
}