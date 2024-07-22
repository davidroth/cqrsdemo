namespace CqrsDemo.Infrastructure
{
    /// <summary>
    /// Note: A query object may contain several members which must be included to form a valid caching identifier for this query.
    /// This interface shall be used to efficently build up such a caching key
    /// See sample implementations
    /// </summary>
    public interface IProvideCachingKey
    {
        string GetCacheKey();
    }
}