using CqrsDemo.Core.Queries;

namespace CqrsDemoWeb.Models
{
    public class OrderEditViewModel
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public OrderStatusInfo Status { get; set; }
        public required ProductInfo Product { get; set; }
        public List<Position> Positions { get; set; } = [];

        public class Position
        {
            public int Id { get; set; }

            public required string Name { get; set; }

            public int Quantity { get; set; }

            public bool Delete { get; set; }
        }
    }
}