namespace CqrsDemo.Infrastructure
{
    public sealed class EntityNotFoundException : Exception
    {
        public EntityNotFoundException() : this("Could not find entity") { }
        public EntityNotFoundException(string message) : base(message) { }
        public EntityNotFoundException(Type type, object id)
            : this($"Could not find {type.Name} with id {id}")
        { }
    }
}