using System.Runtime.Serialization;

namespace Swisschain.Auth.Grpc.Contracts
{
    [DataContract]
    public class ComparePasswordRequest
    {
        [DataMember(Order = 1)]
        public string TraderId { get; set; }

        [DataMember(Order = 2)]
        public string Password { get; set; }
    }

    [DataContract]
    public class ComparePasswordResponse
    {
        [DataMember(Order = 1)]
        public bool Ok { get; set; }
    }
}