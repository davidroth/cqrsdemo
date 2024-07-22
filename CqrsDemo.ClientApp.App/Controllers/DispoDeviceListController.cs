using CqrsDemo.Core.Commands.Dispos;
using CqrsDemo.Core.Queries;

namespace CqrsDemo.ClientApp.App.Controllers
{
    public class DispoDeviceListController : Controller<DispoDeviceListViewModel>
    {
        public DispoDeviceListController(IDispoDeviceListView view, ControllerContext context) 
            : base(view, context)
        { }

        public async Task Init()
        {
            ViewModel = new DispoDeviceListViewModel()
            {
                UpdateDeliveryDateCommand = new DelegateCommand(UpdateDeliveryDate, CanUpdateDeliveryDate),
            };
            ViewModel.BindCommand((ApplicationCommand)ViewModel.UpdateDeliveryDateCommand, nameof(ViewModel.SelectedRecord));
            await RefreshGridData();
        }

        private Task<bool> CanUpdateDeliveryDate(object? arg)
        {
            return Task.FromResult(ViewModel.SelectedRecord != null);
        }

        private async Task UpdateDeliveryDate(object? arg)
        {
            await this.ExecuteAsync(new ChangeAvailabilityDate()
            {
                DispoDeviceId = ViewModel.SelectedRecord?.Id ?? throw new InvalidOperationException("Selected record is null"),
                NewAvailability = DateTime.UtcNow.AddDays(20),
            });
            await RefreshGridData();
        }

        private async Task RefreshGridData()
        {
            ViewModel.Items.Clear();
            foreach (var item in (await this.QueryAsync(new GetDispoDeviceList() { Take = 100 })).Dispos)
            {
                ViewModel.Items.Add(item);
            }
        }
    }
}