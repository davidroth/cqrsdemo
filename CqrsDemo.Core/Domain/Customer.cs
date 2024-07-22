using System.ComponentModel.DataAnnotations;

namespace CqrsDemo.Core.Domain
{
    public class Customer : Entity, IAggregateRoot
    {
        public Customer(string name)
        {
            Name = name;
        }

        [Required]
        public string Name { get; set; }
    }
}