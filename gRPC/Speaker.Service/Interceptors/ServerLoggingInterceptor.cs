using Grpc.Core;
using Grpc.Core.Interceptors;
using System.Reflection.Metadata;

namespace Speaker.Service.Interceptors
{
    public class ServerLoggingInterceptor : Interceptor
    {
        private readonly ILogger _logger;

        public ServerLoggingInterceptor(ILogger<ServerLoggingInterceptor> logger)
        {
            _logger = logger;
        }

        public override async Task<TResponse> UnaryServerHandler<TRequest, TResponse>(
            TRequest request,
            ServerCallContext context,
            UnaryServerMethod<TRequest, TResponse> continuation)
        {
            _logger.LogTrace($"Starting receiving call. Type: {MethodType.Unary}. " + $"Method: {context.Method}.");
            Console.WriteLine("interceptor here:");

            try
            {
                ///add the trailers back? maybe?
                return await continuation(request, context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error thrown by {context.Method}.");
                throw;
            }
        }
    }
}
