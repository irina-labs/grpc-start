using Basics;
using Grpc.Core;

namespace Basics.Services
{
    public class GreeterService : Greeter.GreeterBase
    {
        private readonly ILogger<GreeterService> _logger;
        public GreeterService(ILogger<GreeterService> logger)
        {
            _logger = logger;
        }

        public override Task<Response> SayHello(Request request, ServerCallContext context)
        {
            return Task.FromResult(new Response
            {
                Message = "Hello " + request.Name
            });
        }

        public override async Task ServerStream(Request request, IServerStreamWriter<Response> responseStream, ServerCallContext context)
        {
            for (int i = 0; i < 10000; i++)
            {
                var message = new Response
                {
                    Message = "Hello " + i
                };


                await responseStream.WriteAsync(message);
            }
        }

        public override async Task<Response> ClientStream(IAsyncStreamReader<Request> requestStream, ServerCallContext context)
        {
            var baseMessage = "I got ";
            Response reply = new Response { Message = baseMessage };

            while (await requestStream.MoveNext())
            {
                var currentItem = requestStream.Current;
                Console.WriteLine($" I got a request with:{currentItem}");

                reply.Message += currentItem.Name + ", ";
            }
            return reply;
        }

        public override  async Task Bidirectional(IAsyncStreamReader<Request> requestStream, IServerStreamWriter<Response> responseStream, ServerCallContext context)
        {
            var baseMessage = "";
            Response reply = new Response() { Message = baseMessage };

            while (await requestStream.MoveNext())
            {
                var payload = requestStream.Current;


                reply.Message = baseMessage + payload.Name.ToString();
                await responseStream.WriteAsync(reply);

            }
        }
    }
}