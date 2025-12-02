using Google.Protobuf.Collections;
using System.Collections.Generic;
using System.Linq;
using YGOClient.Models;

namespace YGOClient.Interfaces
{
    public interface ICacheService
    {
        Task<string> Generate(object obj);
    }
}
