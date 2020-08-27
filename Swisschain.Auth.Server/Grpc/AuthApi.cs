
using System;
using System.Threading.Tasks;
using Npgsql;
using Swisschain.Auth.Grpc;
using Swisschain.Auth.Grpc.Contracts;

namespace Swisschain.Auth.Server.Grpc
{
    public class AuthApi : IAuthGrpcService
    {
        public async ValueTask<NewCredentialsGrpcResponse> RegisterAsync(NewCredentialsGrpcRequest request)
        {
            ServiceLocator.Logger.Information("Got register request new user by email.");
            try
            {
                var result = 
                    await ServiceLocator.AuthCredentialsRepository.RegisterAsync(request.Email, request.Password);


                return new NewCredentialsGrpcResponse
                {
                    TraderId = result.Id,
                    Status = ResponseStatuses.Success
                };
            }
            catch (Exception exception)
            {
                ServiceLocator.Logger.Error(exception, "Registration exception for email: {email}", request.Email);
                
                return new NewCredentialsGrpcResponse
                {
                    Status =  exception is PostgresException 
                        ? ResponseStatuses.ClientAlreadyExists 
                        : ResponseStatuses.SystemError,
                };
            }
        }

        public async ValueTask<AuthenticateGrpcResponse> AuthenticateAsync(AuthenticateGrpcRequest request)
        {
            var responseFromDb = await ServiceLocator.AuthCredentialsRepository.GetByEmailAsync(request.Email);
            var result = new AuthenticateGrpcResponse();

            if (responseFromDb == null || !responseFromDb.Authenticate(request.Password))
            {
                ServiceLocator.Logger.Information("User not found or password invalid");
                return result;
            }
            result.TraderId = responseFromDb.Id;

            return result;
        }

        public async ValueTask<ExistsGrpcResponse> ExistsAsync(ExistsGrpcRequest request)
        {

            var result = await ServiceLocator.AuthCredentialsRepository.GetByIdAsync(request.TraderId);

            var isExists = result != null;

            return new ExistsGrpcResponse
            {
                Yes = isExists
            };
        }

        public async ValueTask<GetIdGrpcResponse> GetIdByEmailAsync(GetIdGrpcRequest request)
        {
            var result = await ServiceLocator.AuthCredentialsRepository.GetByEmailAsync(request.Email);
            if (result == null) return new GetIdGrpcResponse {TraderId = null};
            
            return new GetIdGrpcResponse
            {
                TraderId = result.Id
            };
        }

        public async ValueTask<GetEmailByIdGrpcResponse> GetEmailByIdAsync(GetEmailByIdGrpcRequest request)
        {

            var result = await ServiceLocator.AuthCredentialsRepository.GetByIdAsync(request.TraderId);
            if (result == null) return new GetEmailByIdGrpcResponse {Email = null};

            return new GetEmailByIdGrpcResponse
            {
                Email = result.Email
            };
        }

        public async ValueTask ChangePasswordAsync(ChangePasswordGrpcContract request)
        {
           await ServiceLocator.AuthCredentialsRepository.ChangePasswordAsync(request.Email, request.Password);
        }

        public async ValueTask<ComparePasswordResponse> ComparePasswordAsync(ComparePasswordRequest request)
        {

            var responseFromDb = await ServiceLocator.AuthCredentialsRepository.GetByIdAsync(request.TraderId);

            if (responseFromDb == null || !responseFromDb.Authenticate(request.Password))
                return new ComparePasswordResponse {Ok = false};

            return new ComparePasswordResponse {Ok = true};
        }
    }
}