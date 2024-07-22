using System.Collections.Concurrent;

namespace CqrsDemo.Infrastructure.Caching
{
    public class InMemoryCache : ICache
    {
        private readonly ConcurrentDictionary<string, object?> cacheStore = new();
        public Task<object?> GetAsync(string key)
        {
            if (cacheStore.TryGetValue(key, out var value))
                return Task.FromResult(value);
            else
                return Task.FromResult<object?>(null);
        }

        public Task PutAsync(string key, object? value)
        {
            cacheStore[key] = value;
            return Task.CompletedTask;
        }
    }
}