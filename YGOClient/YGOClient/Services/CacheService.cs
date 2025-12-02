using Google.Protobuf.Collections;
using System.Reflection;
using YGOClient.Interfaces;
using YGOClient.Models;

namespace YGOClient.Services;

public class CacheService : ICacheService
{
    public async Task<string> Generate(object obj)
    {
        return await Task.Run(() =>
        {
            var type = obj.GetType().Name;
            var properties = obj.GetType()
            .GetProperties(BindingFlags.Public | BindingFlags.Instance)
            .OrderBy(p => p.Name);
            
            var propsString = properties
            .Select(p => $"{p.Name}:{p.GetValue(obj) ?? "null"}");
            
            return $"{type}:{string.Join(":", propsString)}".ToUpperInvariant();      
        });
    }
}
