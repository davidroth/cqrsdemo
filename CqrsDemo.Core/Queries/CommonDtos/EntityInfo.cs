namespace CqrsDemo.Core.Queries
{
    public abstract class EntityInfo
    {
        public int Id { get; set; }
        public required string Name { get; set; }

        public override string ToString() => Name;
    }
}