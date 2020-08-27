using System.Runtime.Serialization;

namespace Swisschain.Auth.Grpc.Contracts
{
    [DataContract]
    public class AuthenticateGrpcRequest
    {
        [DataMember(Order = 1)]
        public string Email { get; set; }

        [DataMember(Order = 2)]
        public string Password { get; set; }
    }

    [DataContract]
    public class AuthenticateGrpcResponse
    {
        [DataMember(Order = 1)]
        public string TraderId { get; set; }
    }
}