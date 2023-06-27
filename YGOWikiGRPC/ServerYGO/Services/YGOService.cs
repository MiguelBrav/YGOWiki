using Grpc.Core;
using ServerYGO;

namespace ServerYGO.Services
{
    public class YGOService : YGOWiki.YGOWikiBase
    {
        private readonly ILogger<YGOService> _logger;
        public YGOService(ILogger<YGOService> logger)
        {
            _logger = logger;
        }

        public override Task<HelloReply> SayHello(HelloRequest request, ServerCallContext context)
        {
            return Task.FromResult(new HelloReply
            {
                Message = "Hello " + request.Name
            });
        }
    }
}