namespace CqrsDemo.Core.Domain
{
    public sealed class EntityValidationException : Exception
    {
        public EntityValidationException()
        { }

        public EntityValidationException(string message)
            : base(message)
        { }

        public EntityValidationException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}