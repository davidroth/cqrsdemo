namespace CqrsDemo.Core.Domain
{
    public interface IOrderNumberGenerator
    {
        Task<string> GetNextOrderNumberAsync();
    }
}