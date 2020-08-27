using System;
using System.Threading.Tasks;
using Grpc.Core;
using Grpc.Core.Interceptors;
using Serilog.Core;

namespace Swisschain.Auth.Server
{
    public class ErrorLoggerInterceptor : Interceptor
    {
        private readonly Logger _logger;

        public ErrorLoggerInterceptor(Logger logger)
        {
            _logger = logger;
        }

        public override async Task<TResponse> UnaryServerHandler<TRequest, TResponse>(
            TRequest request,
            ServerCallContext context,
            UnaryServerMethod<TRequest, TResponse> continuation)
        {
            try
            {
                return await continuation(request, context);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, ex.Message);
                throw new RpcException(Status.DefaultCancelled, ex.Message);
            }
        }

    }
}