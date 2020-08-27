using System.Runtime.Serialization;

namespace Swisschain.Auth.Grpc.Contracts
{
    [DataContract]
    public class NewCredentialsGrpcRequest
    {
        [DataMember(Order = 1)] 
        public string Email { get; set; }
        [DataMember(Order = 2)] 
        public string Password { get; set; }

    }

    [DataContract]
    public class NewCredentialsGrpcResponse
    {
        [DataMember(Order = 1)] 
        public string TraderId { get; set; }

        [DataMember(Order = 2)] 
        public ResponseStatuses Status { get; set; }

    }
}