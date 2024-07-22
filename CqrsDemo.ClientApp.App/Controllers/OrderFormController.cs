using System.ComponentModel;
using CqrsDemo.Core.Commands;
using CqrsDemo.Core.Queries;

namespace CqrsDemo.ClientApp.App.Controllers
{
    public class OrderFormController : Controller
    {
        public OrderFormController(IOrderFormView view, ControllerContext context)
            : base(view, context)
        { }

        public async Task InitEdit(int orderId)
        {
            var data = await this.QueryAsync(new GetOrderDetails(orderId));
            InitViewModel(data);
        }

        private void InitViewModel(GetOrderDetails.Result result)
        {
            var viewModel = new OrderFormViewModel()
            {
                Id = result.Id,
                Name = result.Name,
                Status = result.Status,
                Product = result.Product,
                Positions = new BindingList<OrderFormViewModel.Position>(result.Positions.Select(x => new OrderFormViewModel.Position
                {
                    Id = x.Id,
                    Name = x.Name,
                    Quantity = x.Quantity
                }).ToList())
            };
            viewModel.CreateOrUpdateCommand = new DelegateCommand(SaveAsync);
            ViewModel = viewModel;
        }

        private async Task SaveAsync(object? obj)
        {
            var command = new UpdateOrderDetails()
            {
                Id = ViewModel.Id,
                Name = ViewModel.Name,
                Positions = ViewModel.Positions.Select(x =>
                    new UpdateOrderDetails.Position()
                    {
                        Id = x.Id,
                        Name = x.Name,
                        Quantity = x.Quantity,
                        Delete = x.Delete,
                    }).ToList()
            };

            await this.ExecuteAsync(command);
            Dismiss();
        }

        public new OrderFormViewModel ViewModel
        {
            get { return (OrderFormViewModel)base.ViewModel; }
            set { base.ViewModel = value; }
        }
    }
}