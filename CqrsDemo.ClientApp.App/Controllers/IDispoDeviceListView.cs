using System.Collections.ObjectModel;
using System.Windows.Input;
using CqrsDemo.Core.Queries;

namespace CqrsDemo.ClientApp.App.Controllers
{
    public interface IDispoDeviceListView : IView
    { }

    public class DispoDeviceListViewModel : ViewModel
    {
        public ObservableCollection<GetDispoDeviceList.ListModel> Items { get; } = [];

        public required ICommand UpdateDeliveryDateCommand { get; set; }

        public GetDispoDeviceList.ListModel? SelectedRecord { get; set; }
    }
}