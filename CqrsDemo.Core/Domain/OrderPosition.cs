using System.ComponentModel.DataAnnotations;

namespace CqrsDemo.Core.Domain
{
    public class OrderPosition : Entity
    {
        public OrderPosition(string name)
        {
            Name = name;
        }

        [Required]
        public string Name { get; set; }

        [Range(0, int.MaxValue)]
        public int Quantity { get; set; } = 1;

        public Order? Order { get; set; }
    }
}