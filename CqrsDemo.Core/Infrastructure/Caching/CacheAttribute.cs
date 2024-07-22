namespace CqrsDemo.Infrastructure.Caching
{
    [AttributeUsage(AttributeTargets.Class, Inherited = false)]
    public sealed class CacheAttribute : Attribute
    { }
}