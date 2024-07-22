namespace CqrsDemo.Infrastructure.Caching
{
    /// <summary>
    /// Note: Members are async so that a distributed cache provider can be implemented with async calls 
    /// (e.x caching values in an in memory datbase table, or external system like redis cache)
    /// </summary>
    public interface ICache
    {
        Task PutAsync(string key, object? value);
        Task<object?> GetAsync(string key);
    }
}