namespace CqrsDemo.Infrastructure
{
    [AttributeUsage(AttributeTargets.Class, Inherited = false)]
    public sealed class RetryCommandAttribute : Attribute
    { }
}