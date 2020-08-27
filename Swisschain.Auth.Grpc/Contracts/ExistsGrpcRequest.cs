using System.Runtime.Serialization;

namespace Swisschain.Auth.Grpc.Contracts
{
    [DataContract]
    public class ExistsGrpcRequest
    {
        [DataMember(Order = 1)]
        public string TraderId { get; set; }
    }

    [DataContract]
    public class ExistsGrpcResponse
    {
        [DataMember(Order = 1)]
        public bool Yes { get; set; }
    }

}