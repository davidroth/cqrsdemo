using CqrsDemo.Core.Commands;
using CqrsDemo.Core.Queries;

namespace CqrsDemo.ClientApp.App.Controllers
{
    public class MainController : Controller<MainViewModel>
    {
        private readonly ControllerContext context;

        public MainController(IMainView view, ControllerContext context)
            : base(view, context)
        {
            this.context = context;
        }

        public async void Init()
        {
            ViewModel = new MainViewModel()
            {
                EditRecordCommand = new DelegateCommand(x => View.ShowMessageBox("Do you want to execute?")),

                CreateCommand = new DelegateCommand(x => CreateOrder()),
                EditCommand = new DelegateCommand(x => EditOrder(), x => Task.FromResult(ViewModel.SelectedRecord != null)),
                ReloadCommand = new DelegateCommand(x => RefreshGridData()),
                CancelOrderCommand = new DelegateCommand(CancelOrder, CanCancelOrderAsync) { Tag = "CancelOrder" },
            };

            ViewModel.BindCommand((ApplicationCommand)ViewModel.CancelOrderCommand, nameof(ViewModel.SelectedRecord));
            await RefreshGridData();
            await AppController.RunAsync<DispoDeviceListController>(x => x.Init());
        }

        private async Task CreateOrder()
        {
            await this.ExecuteAsync(new CreateOrder()
            {
                Name = $"Sample order {DateTime.Now.ToLongTimeString()}",
                ProductId = 1,
                CustomerId = 1
            });
            await RefreshGridData();
        }

        private async Task EditOrder()
        {
            var controller = await AppController.RunAsync<OrderFormController>(c => c.InitEdit(ViewModel.SelectedRecord!.Id));
            await controller.Dismissed;
            await RefreshGridData();
        }


        private async Task<bool> CanCancelOrderAsync(object? arg)
        {
            if (ViewModel.SelectedRecord != null)
            {
                return (await this.QueryAsync(new CanCancelOrder(ViewModel.SelectedRecord.Id))).CanExecute;
            }
            else return false;
        }

        private async Task CancelOrder(object? x)
        {
            await this.ExecuteAsync(
                new CancelOrder()
                {
                    OrderId = ViewModel.SelectedRecord!.Id,
                    Reason = "Cancel"
                });
            await RefreshGridData();
        }

        private async Task RefreshGridData()
        {
            ViewModel.AvailableOrders.Clear();
            foreach (var item in (await this.QueryAsync(new GetOrderList() { Take = 100 })).Orders)
            {
                ViewModel.AvailableOrders.Add(item);
            }
        }

        private new IMainView View => (IMainView)base.View;
    }
}