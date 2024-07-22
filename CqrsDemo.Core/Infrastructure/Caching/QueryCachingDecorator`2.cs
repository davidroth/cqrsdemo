using Newtonsoft.Json;

namespace CqrsDemo.Infrastructure.Caching
{
    public class QueryCachingDecorator<TQuery, TResult> : IRequestHandler<TQuery, TResult>
         where TQuery : IQuery<TResult>
    {
        private readonly IRequestHandler<TQuery, TResult> innerQueryHandler;
        private readonly ICache cache;

        public QueryCachingDecorator(IRequestHandler<TQuery, TResult> innerQueryHandler, ICache cache)
        {
            this.cache = cache;
            this.innerQueryHandler = innerQueryHandler;
        }

        public async Task<TResult> Handle(TQuery query, CancellationToken cancellationToken)
        {
            string? key = null;
            if (query is IProvideCachingKey cacheKeyProvider) // Note: IProvideCachingKey is the faster alternative to serializing a key based on serializing the query object.
            {
                key = cacheKeyProvider.GetCacheKey();
            }

            if (string.IsNullOrEmpty(key))
            {
                // This is only a fallback but is slower and consumes more memory than providing a custom GetCacheKey() implementation
                key = JsonConvert.SerializeObject(query);
            }

            var cached = await cache.GetAsync(key);
            if (cached == null)
            {
                var result = await innerQueryHandler.Handle(query, cancellationToken);
                await cache.PutAsync(key, result);
                return result;
            }
            return (TResult)cached;
        }
    }
}