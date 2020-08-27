using System.Runtime.Serialization;

namespace Swisschain.Auth.Grpc.Contracts
{
    [DataContract]
    public class GetIdGrpcRequest
    {

        [DataMember(Order = 1)]
        public string Email { get; set; }
    }


    [DataContract]
    public class GetIdGrpcResponse
    {

        [DataMember(Order = 1)]
        public string TraderId { get; set; }
    }
}