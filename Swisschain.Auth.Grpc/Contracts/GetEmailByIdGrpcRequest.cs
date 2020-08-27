using System.Runtime.Serialization;

namespace Swisschain.Auth.Grpc.Contracts
{
    [DataContract]
    public class GetEmailByIdGrpcRequest
    {

        [DataMember(Order = 1)]
        public string TraderId { get; set; }
    }


    [DataContract]
    public class GetEmailByIdGrpcResponse
    {

        [DataMember(Order = 1)]
        public string Email { get; set; }
    }
}