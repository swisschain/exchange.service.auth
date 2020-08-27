using System.ServiceModel;
using System.Threading.Tasks;
using Swisschain.Auth.Grpc.Contracts;

namespace Swisschain.Auth.Grpc
{
    [ServiceContract(Name = "AuthService")]
    public interface IAuthGrpcService
    {
        [OperationContract(Action = "Register")]
        ValueTask<NewCredentialsGrpcResponse> RegisterAsync(NewCredentialsGrpcRequest request);

        [OperationContract(Action = "Authenticate")]
        ValueTask<AuthenticateGrpcResponse> AuthenticateAsync(AuthenticateGrpcRequest request);

        [OperationContract(Action = "Exists")]
        ValueTask<ExistsGrpcResponse> ExistsAsync(ExistsGrpcRequest request);

        [OperationContract(Action = "GetIdByEmail")]
        ValueTask<GetIdGrpcResponse> GetIdByEmailAsync(GetIdGrpcRequest request);

        [OperationContract(Action = "GetEmailById")]
        ValueTask<GetEmailByIdGrpcResponse> GetEmailByIdAsync(GetEmailByIdGrpcRequest request);

        [OperationContract(Action = "ChangePassword")]
        ValueTask ChangePasswordAsync(ChangePasswordGrpcContract request);

        [OperationContract(Action = "ComparePassword")]
        ValueTask<ComparePasswordResponse> ComparePasswordAsync(ComparePasswordRequest request);
    }
}