using System;
using System.Threading.Tasks;
using MyPostgreSQL;
using Swisschain.Auth.Domains;

namespace Swisschain.Auth.Postgres
{
    public class AuthenticationCredentialsPostgresRepository : IAuthCredentialsRepository
    {
        private readonly IPostgresConnection _postgresConnection;
        private readonly byte[] _initKey;
        private readonly byte[] _initVector;

        private const string TableName = "auth.authcreds";
        
        public AuthenticationCredentialsPostgresRepository(IPostgresConnection postgresConnection,  byte[] initKey, byte[] initVector)
        {
            _postgresConnection = postgresConnection ?? throw new ArgumentNullException(nameof(postgresConnection));
            _initKey = initKey;
            _initVector = initVector;
        }
        
        private async Task<AuthenticationCredentialsPostgresEntity> GetEntityAsync(string email)
        {
            var encodeEmail = AuthenticationCredentialsPostgresEntity.EncodeEmail(email, _initKey, _initVector);
            
            var sql = $@"SELECT * FROM {TableName} where email = @email";

            return await _postgresConnection
                .GetFirstRecordOrNullAsync<AuthenticationCredentialsPostgresEntity>(sql, new { email = encodeEmail });
        }
        
        public async Task<IAuthenticationCredentials> GetByEmailAsync(string email)
        {
            return await GetEntityAsync(email);
        }

        public async Task<IAuthenticationCredentials> GetByIdAsync(string id)
        {
            var sql = $@"SELECT * FROM {TableName} where id = @id";

            var result = await _postgresConnection
                .GetFirstRecordOrNullAsync<AuthenticationCredentialsPostgresEntity>(sql, new { id = id.ToLower() });

            if (result != null)
                result.Email = result.Email.Decode(_initKey, _initVector);

            return result;
        }

        public async Task<IAuthenticationCredentials> RegisterAsync(string email, string password)
        {
            var entity = AuthenticationCredentialsPostgresEntity.Create(email, password, _initKey, _initVector);

            await _postgresConnection.Insert(TableName).SetInsertModel(entity).ExecuteAsync();

            return entity;
        }

        public async Task<IAuthenticationCredentials> ChangePasswordAsync(string email, string password)
        {
            var entity = await GetEntityAsync(email);

            if (entity == null)
                return null;
            
            entity.SetPassword(password);
            
            await _postgresConnection.Update(TableName)
                .SetUpdateModel(entity)
                .SetWhereCondition("email = @email", new { email = entity.Email })
                .ExecuteAsync();

            return entity;
        }

    }
}
