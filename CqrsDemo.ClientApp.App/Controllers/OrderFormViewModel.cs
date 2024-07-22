using System.ComponentModel;
using System.Windows.Input;
using CqrsDemo.Core.Queries;

namespace CqrsDemo.ClientApp.App.Controllers
{
    public class OrderFormViewModel
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public OrderStatusInfo Status { get; set; }
        public required ProductInfo Product { get; set; }
        public BindingList<Position> Positions { get; set; } = [];
        public ICommand? CreateOrUpdateCommand { get; set; }

        public class Position
        {
            public int Id { get; set; }

            public required string Name { get; set; }

            public int Quantity { get; set; }

            public bool Delete { get; set; }
        }
    }
}