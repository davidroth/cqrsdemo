namespace CqrsDemo.Core.Domain
{
    public class Product : Entity, IAggregateRoot
    {
        public Product(string name)
        {
            Name = name;
        }

        public string Name { get; private set; }

        public decimal AllowedDiscount { get; private set; }
    }
}